using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Proyecto.Models;

[Table("Etapa")]
[Index("Nombre", Name = "nombre", IsUnique = true)]
public partial class Etapa
{
    [Key]
    [Column("id", TypeName = "int(11)")]
    public int Id { get; set; }

    [Column("nombre")]
    [StringLength(50)]
    public string? Nombre { get; set; }

    [InverseProperty("IdEtapaNavigation")]
    public virtual ICollection<Prediccion> Prediccions { get; set; } = new List<Prediccion>();

    [InverseProperty("IdEtapaNavigation")]
    public virtual ICollection<Tratamiento> Tratamientos { get; set; } = new List<Tratamiento>();
}
