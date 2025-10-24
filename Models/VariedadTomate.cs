using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Proyecto.Models;

[Table("variedad_Tomate")]
[Index("Nombre", Name = "nombre", IsUnique = true)]
public partial class VariedadTomate
{
    [Key]
    [Column("id", TypeName = "int(11)")]
    public int Id { get; set; }

    [Column("nombre")]
    [StringLength(50)]
    public string Nombre { get; set; } = null!;

    [InverseProperty("IdVariedadTomateNavigation")]
    public virtual ICollection<Cultivo> Cultivos { get; set; } = new List<Cultivo>();
}
