namespace Proyecto.ApiModels;

public class CultivosResponse
{
    public bool Success { get; set; }
    public string Message { get; set; } = string.Empty;
    public List<CultivoData>? Cultivos { get; set; }
}

public class CultivoData
{
    public int Id { get; set; }
    public int IdAgronomo { get; set; }
    public DateTime? FechaCommit { get; set; }
    public int CantidadPlantas { get; set; }
    public double Latitud { get; set; }
    public double Longitud { get; set; }
    public int IdVariedadTomate { get; set; }
    public int IdEtapa { get; set; }
    public string? VariedadTomate { get; set; }
    public string? Etapa { get; set; }
}