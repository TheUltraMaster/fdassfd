using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Proyecto.Models;

public partial class Usuario
{
    [Key]
    [Column("id", TypeName = "int(11)")]
    public int Id { get; set; }

    [Column("username")]
    [StringLength(50)]
    public string Username { get; set; } = null!;

    [Column("password")]
    [StringLength(300)]
    public string Password { get; set; } = null!;

    [Column("isadmin")]
    public bool? Isadmin { get; set; }

    [InverseProperty("Usuario")]
    public virtual ICollection<Agronomo> Agronomos { get; set; } = new List<Agronomo>();

    [InverseProperty("IdUsuarioNavigation")]
    public virtual ICollection<UsuariosCultivo> UsuariosCultivos { get; set; } = new List<UsuariosCultivo>();
}
