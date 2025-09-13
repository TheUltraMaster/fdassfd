using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Proyecto.Models;

[Table("Prediccion")]
[Index("IdCultvio", Name = "id_cultvio")]
[Index("IdEnfermedad", Name = "id_enfermedad")]
[Index("IdEtapa", Name = "id_etapa")]
public partial class Prediccion
{
    [Key]
    [Column("id", TypeName = "int(11)")]
    public int Id { get; set; }

    [Column("confianza")]
    public double Confianza { get; set; }

    [Column("id_cultvio", TypeName = "int(11)")]
    public int IdCultvio { get; set; }

    [Column("id_enfermedad", TypeName = "int(11)")]
    public int IdEnfermedad { get; set; }

    [Column("id_etapa", TypeName = "int(11)")]
    public int IdEtapa { get; set; }

    [ForeignKey("IdCultvio")]
    [InverseProperty("Prediccions")]
    public virtual Cultivo IdCultvioNavigation { get; set; } = null!;

    [ForeignKey("IdEnfermedad")]
    [InverseProperty("Prediccions")]
    public virtual Enfermedad IdEnfermedadNavigation { get; set; } = null!;

    [ForeignKey("IdEtapa")]
    [InverseProperty("Prediccions")]
    public virtual Etapa IdEtapaNavigation { get; set; } = null!;
}
