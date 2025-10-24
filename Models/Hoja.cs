using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Proyecto.Models;

[Index("IdEnfermedad", Name = "id_enfermedad")]
public partial class Hoja
{
    [Key]
    [Column("id", TypeName = "int(11)")]
    public int Id { get; set; }

    [Column("confianza")]
    public double Confianza { get; set; }

    [Column("luminosidad")]
    public double Luminosidad { get; set; }

    [Column("rojo", TypeName = "int(11)")]
    public int Rojo { get; set; }

    [Column("verde", TypeName = "int(11)")]
    public int Verde { get; set; }

    [Column("azul", TypeName = "int(11)")]
    public int Azul { get; set; }

    [Column("id_enfermedad", TypeName = "int(11)")]
    public int IdEnfermedad { get; set; }

    [Column("url", TypeName = "text")]
    public string? Url { get; set; }

    [Column("anchopx", TypeName = "int(11)")]
    public int Anchopx { get; set; }

    [Column("altopx", TypeName = "int(11)")]
    public int Altopx { get; set; }

    [ForeignKey("IdEnfermedad")]
    [InverseProperty("Hojas")]
    public virtual Enfermedad IdEnfermedadNavigation { get; set; } = null!;
}
