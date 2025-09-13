using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Proyecto.Models;

[Index("UsuarioId", Name = "usuario_id")]
public partial class Agronomo
{
    [Key]
    [Column("id", TypeName = "int(11)")]
    public int Id { get; set; }

    [Column("primer_nombre")]
    [StringLength(50)]
    public string PrimerNombre { get; set; } = null!;

    [Column("segundo_nombre")]
    [StringLength(50)]
    public string? SegundoNombre { get; set; }

    [Column("primer_apellido")]
    [StringLength(50)]
    public string PrimerApellido { get; set; } = null!;

    [Column("segundo_apellido")]
    [StringLength(50)]
    public string? SegundoApellido { get; set; }

    [Column("usuario_id", TypeName = "int(11)")]
    public int UsuarioId { get; set; }

    [InverseProperty("IdAgronomoNavigation")]
    public virtual ICollection<Cultivo> Cultivos { get; set; } = new List<Cultivo>();

    [ForeignKey("UsuarioId")]
    [InverseProperty("Agronomos")]
    public virtual Usuario Usuario { get; set; } = null!;
}
