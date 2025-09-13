using Proyecto.Models;

namespace Proyecto.Services;

public interface ITreatmentService
{
    Task<IEnumerable<Tratamiento>> GetAllTreatmentsAsync();
    Task<Tratamiento?> GetTreatmentByIdAsync(int id);
    Task<Tratamiento> CreateTreatmentAsync(Tratamiento tratamiento);
    Task<Tratamiento> UpdateTreatmentAsync(Tratamiento tratamiento);
    Task<bool> DeleteTreatmentAsync(int id);
    Task<bool> TreatmentNameExistsAsync(string nombre, int? excludeId = null);
    Task<IEnumerable<Etapa>> GetAllEtapasAsync();
    Task<IEnumerable<Enfermedad>> GetAllEnfermedadesAsync();
}