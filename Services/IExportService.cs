using Proyecto.Models;

namespace Proyecto.Services;

public interface IExportService
{
    Task<byte[]> ExportHojasToExcelAsync();
    Task<byte[]> ExportHojasToCsvAsync();
    Task<List<HojaExportDto>> GetHojasForExportAsync();
}