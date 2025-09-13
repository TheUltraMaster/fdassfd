using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Proyecto.Models;

[Table("Reporte")]
[Index("IdTratamientoCultivo", Name = "id_tratamiento_cultivo")]
public partial class Reporte
{
    [Key]
    [Column("id", TypeName = "int(11)")]
    public int Id { get; set; }

    [Column("positivo")]
    public bool Positivo { get; set; }

    [Column("observacion")]
    [StringLength(300)]
    public string? Observacion { get; set; }

    [Column("id_tratamiento_cultivo", TypeName = "int(11)")]
    public int IdTratamientoCultivo { get; set; }

    [Column("fecha_commit", TypeName = "datetime")]
    public DateTime FechaCommit { get; set; }

    [ForeignKey("IdTratamientoCultivo")]
    [InverseProperty("Reportes")]
    public virtual Tratamiento IdTratamientoCultivoNavigation { get; set; } = null!;
}
