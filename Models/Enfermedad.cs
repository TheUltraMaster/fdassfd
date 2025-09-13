using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Proyecto.Models;

[Table("Enfermedad")]
[Index("Classname", Name = "classname", IsUnique = true)]
[Index("Nombre", Name = "nombre", IsUnique = true)]
public partial class Enfermedad
{
    [Key]
    [Column("id", TypeName = "int(11)")]
    public int Id { get; set; }

    [Column("classname")]
    [StringLength(50)]
    public string? Classname { get; set; }

    [Column("nombre")]
    [StringLength(50)]
    public string? Nombre { get; set; }

    [InverseProperty("IdEnfermedadNavigation")]
    public virtual ICollection<Hoja> Hojas { get; set; } = new List<Hoja>();

    [InverseProperty("IdEnfermedadNavigation")]
    public virtual ICollection<Prediccion> Prediccions { get; set; } = new List<Prediccion>();

    [InverseProperty("IdEnfermedadNavigation")]
    public virtual ICollection<Tratamiento> Tratamientos { get; set; } = new List<Tratamiento>();
}
