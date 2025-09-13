using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Proyecto.Models;

[Index("IdAgronomo", Name = "id_agronomo")]
[Index("IdVariedadTomate", Name = "id_variedad_Tomate")]
public partial class Cultivo
{
    [Key]
    [Column("id", TypeName = "int(11)")]
    public int Id { get; set; }

    [Column("id_agronomo", TypeName = "int(11)")]
    public int IdAgronomo { get; set; }

    [Column("fecha_commit", TypeName = "datetime")]
    public DateTime? FechaCommit { get; set; }

    [Column("cantidad_plantas", TypeName = "int(11)")]
    public int CantidadPlantas { get; set; }

    [Column("id_variedad_Tomate", TypeName = "int(11)")]
    public int IdVariedadTomate { get; set; }

    [Column("latitud")]
    public double Latitud { get; set; }

    [Column("longitud")]
    public double Longitud { get; set; }

    [Column("id_etapa", TypeName = "int(11)")]
    public int IdEtapa { get; set; }

    [ForeignKey("IdAgronomo")]
    [InverseProperty("Cultivos")]
    public virtual Agronomo IdAgronomoNavigation { get; set; } = null!;

    [ForeignKey("IdVariedadTomate")]
    [InverseProperty("Cultivos")]
    public virtual VariedadTomate IdVariedadTomateNavigation { get; set; } = null!;

    [InverseProperty("IdCultvioNavigation")]
    public virtual ICollection<Prediccion> Prediccions { get; set; } = new List<Prediccion>();

    [InverseProperty("IdCultivoNavigation")]
    public virtual ICollection<TratamientosCultivo> TratamientosCultivos { get; set; } = new List<TratamientosCultivo>();

    [InverseProperty("IdCultivoNavigation")]
    public virtual ICollection<UsuariosCultivo> UsuariosCultivos { get; set; } = new List<UsuariosCultivo>();
}
