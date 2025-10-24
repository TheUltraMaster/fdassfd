using System.ComponentModel.DataAnnotations;

namespace Proyecto.ApiModels;

public class LoginResponse
{
    public bool Success { get; set; }
    public string Message { get; set; } = string.Empty;
    public UserData? User { get; set; }
    public string? Token { get; set; }
}

public class UserData
{
    public int Id { get; set; }
    public string Username { get; set; } = string.Empty;
    public bool IsAdmin { get; set; }
}

public class PredictionRequest
{
    [Required(ErrorMessage = "El token es requerido")]
    public string Token { get; set; } = string.Empty;

    [Required(ErrorMessage = "El classname de la enfermedad es requerido")]
    public string EnfermedadClassname { get; set; } = string.Empty;

    [Required(ErrorMessage = "El ID del cultivo es requerido")]
    public int CultivoId { get; set; }

    [Range(0.0, 1.0, ErrorMessage = "La confianza debe estar entre 0 y 1")]
    public double Confianza { get; set; } = 0.0;
}

public class PredictionResponse
{
    public bool Success { get; set; }
    public string Message { get; set; } = string.Empty;
    public int? PrediccionId { get; set; }
}

public class LeafRequest
{
    [Required(ErrorMessage = "El ID de la predicción es requerido")]
    public int PrediccionId { get; set; }

    [Range(0, 255, ErrorMessage = "El valor R debe estar entre 0 y 255")]
    public int Rojo { get; set; }

    [Range(0, 255, ErrorMessage = "El valor G debe estar entre 0 y 255")]
    public int Verde { get; set; }

    [Range(0, 255, ErrorMessage = "El valor B debe estar entre 0 y 255")]
    public int Azul { get; set; }

    [Range(0.0, double.MaxValue, ErrorMessage = "El brillo debe ser un valor positivo")]
    public double Luminosidad { get; set; }

    [Required(ErrorMessage = "Los datos de la imagen son requeridos")]
    public byte[] ImageData { get; set; } = [];

    [Range(0.0, 1.0, ErrorMessage = "La confianza debe estar entre 0 y 1")]
    public double Confianza { get; set; }

    [Required(ErrorMessage = "El classname de la enfermedad es requerido")]
    public string EnfermedadClassname { get; set; } = string.Empty;

    [Range(1, int.MaxValue, ErrorMessage = "El ancho debe ser mayor a 0")]
    public int Ancho { get; set; }

    [Range(1, int.MaxValue, ErrorMessage = "El alto debe ser mayor a 0")]
    public int Alto { get; set; }
}

public class LeafResponse
{
    public bool Success { get; set; }
    public string Message { get; set; } = string.Empty;
    public int? HojaId { get; set; }
    public string? ImageUrl { get; set; }
}