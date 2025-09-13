using System.ComponentModel.DataAnnotations;

namespace Proyecto.Models;

public class HojaExportDto
{
    [Display(Name = "ID")]
    public int Id { get; set; }

    [Display(Name = "Confianza")]
    public double Confianza { get; set; }

    [Display(Name = "Luminosidad")]
    public double Luminosidad { get; set; }

    [Display(Name = "Rojo")]
    public int Rojo { get; set; }

    [Display(Name = "Verde")]
    public int Verde { get; set; }

    [Display(Name = "Azul")]
    public int Azul { get; set; }

    [Display(Name = "ID Enfermedad")]
    public int IdEnfermedad { get; set; }

    [Display(Name = "Nombre Enfermedad")]
    public string? NombreEnfermedad { get; set; }

    [Display(Name = "Classname Enfermedad")]
    public string? ClassnameEnfermedad { get; set; }
}