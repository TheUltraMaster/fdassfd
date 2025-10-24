using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Proyecto.Models;

[Index("IdEnfermedad", Name = "id_enfermedad")]
[Index("IdEtapa", Name = "id_etapa")]
[Index("Nombre", Name = "nombre", IsUnique = true)]
public partial class Tratamiento
{
    [Key]
    [Column("id", TypeName = "int(11)")]
    public int Id { get; set; }

    [Column("cantidad_por_planta")]
    [Precision(10, 0)]
    public decimal? CantidadPorPlanta { get; set; }

    [Column("nombre")]
    [StringLength(50)]
    public string Nombre { get; set; } = null!;

    [Column("descripcion", TypeName = "text")]
    public string? Descripcion { get; set; }

    [Column("id_etapa", TypeName = "int(11)")]
    public int IdEtapa { get; set; }

    [Column("id_enfermedad", TypeName = "int(11)")]
    public int IdEnfermedad { get; set; }

    [ForeignKey("IdEnfermedad")]
    [InverseProperty("Tratamientos")]
    public virtual Enfermedad IdEnfermedadNavigation { get; set; } = null!;

    [ForeignKey("IdEtapa")]
    [InverseProperty("Tratamientos")]
    public virtual Etapa IdEtapaNavigation { get; set; } = null!;

    [InverseProperty("IdTratamientoCultivoNavigation")]
    public virtual ICollection<Reporte> Reportes { get; set; } = new List<Reporte>();

    [InverseProperty("IdTratamientoNavigation")]
    public virtual ICollection<TratamientosCultivo> TratamientosCultivos { get; set; } = new List<TratamientosCultivo>();
}
