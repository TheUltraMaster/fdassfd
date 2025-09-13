using Microsoft.EntityFrameworkCore;
using Proyecto.Data;
using Proyecto.Models;

namespace Proyecto.Services;

public class TreatmentService : ITreatmentService
{
    private readonly ProyectoContext _context;

    public TreatmentService(ProyectoContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Tratamiento>> GetAllTreatmentsAsync()
    {
        return await _context.Tratamientos
            .Include(t => t.IdEtapaNavigation)
            .Include(t => t.IdEnfermedadNavigation)
            .OrderBy(t => t.Nombre)
            .ToListAsync();
    }

    public async Task<Tratamiento?> GetTreatmentByIdAsync(int id)
    {
        return await _context.Tratamientos
            .Include(t => t.IdEtapaNavigation)
            .Include(t => t.IdEnfermedadNavigation)
            .FirstOrDefaultAsync(t => t.Id == id);
    }

    public async Task<Tratamiento> CreateTreatmentAsync(Tratamiento tratamiento)
    {
        _context.Tratamientos.Add(tratamiento);
        await _context.SaveChangesAsync();
        return await GetTreatmentByIdAsync(tratamiento.Id) ?? tratamiento;
    }

    public async Task<Tratamiento> UpdateTreatmentAsync(Tratamiento tratamiento)
    {
        var existingTreatment = await _context.Tratamientos.FindAsync(tratamiento.Id);
        if (existingTreatment == null)
            throw new ArgumentException("Tratamiento no encontrado");

        existingTreatment.Nombre = tratamiento.Nombre;
        existingTreatment.Descripcion = tratamiento.Descripcion;
        existingTreatment.CantidadPorPlanta = tratamiento.CantidadPorPlanta;
        existingTreatment.IdEtapa = tratamiento.IdEtapa;
        existingTreatment.IdEnfermedad = tratamiento.IdEnfermedad;

        await _context.SaveChangesAsync();
        return await GetTreatmentByIdAsync(tratamiento.Id) ?? existingTreatment;
    }

    public async Task<bool> DeleteTreatmentAsync(int id)
    {
        var treatment = await _context.Tratamientos.FindAsync(id);
        if (treatment == null)
            return false;

        _context.Tratamientos.Remove(treatment);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> TreatmentNameExistsAsync(string nombre, int? excludeId = null)
    {
        var query = _context.Tratamientos.Where(t => t.Nombre == nombre);

        if (excludeId.HasValue)
        {
            query = query.Where(t => t.Id != excludeId.Value);
        }

        return await query.AnyAsync();
    }

    public async Task<IEnumerable<Etapa>> GetAllEtapasAsync()
    {
        return await _context.Etapas
            .OrderBy(e => e.Nombre)
            .ToListAsync();
    }

    public async Task<IEnumerable<Enfermedad>> GetAllEnfermedadesAsync()
    {
        return await _context.Enfermedads
            .OrderBy(e => e.Nombre)
            .ToListAsync();
    }

    public async Task<IEnumerable<Tratamiento>> GetTreatmentsByDiseaseAndCropAsync(string diseaseClassname, int cultivoId)
    {
        // Verify that the crop exists
        var cultivo = await _context.Cultivos.FindAsync(cultivoId);
        if (cultivo == null)
            return new List<Tratamiento>();

        // Find the disease by classname
        var enfermedad = await _context.Enfermedads
            .FirstOrDefaultAsync(e => e.Classname == diseaseClassname);

        if (enfermedad == null)
            return new List<Tratamiento>();

        // Get treatments for the disease and the crop's current stage
        return await _context.Tratamientos
            .Include(t => t.IdEtapaNavigation)
            .Include(t => t.IdEnfermedadNavigation)
            .Where(t => t.IdEnfermedad == enfermedad.Id && t.IdEtapa == cultivo.IdEtapa)
            .OrderBy(t => t.Nombre)
            .ToListAsync();
    }
}