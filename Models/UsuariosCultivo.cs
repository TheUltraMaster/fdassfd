using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Proyecto.Models;

[Table("usuarios_cultivos")]
[Index("IdCultivo", Name = "id_cultivo")]
[Index("IdUsuario", Name = "id_usuario")]
public partial class UsuariosCultivo
{
    [Key]
    [Column("id", TypeName = "int(11)")]
    public int Id { get; set; }

    [Column("id_usuario", TypeName = "int(11)")]
    public int IdUsuario { get; set; }

    [Column("id_cultivo", TypeName = "int(11)")]
    public int IdCultivo { get; set; }

    [ForeignKey("IdCultivo")]
    [InverseProperty("UsuariosCultivos")]
    public virtual Cultivo IdCultivoNavigation { get; set; } = null!;

    [ForeignKey("IdUsuario")]
    [InverseProperty("UsuariosCultivos")]
    public virtual Usuario IdUsuarioNavigation { get; set; } = null!;
}
