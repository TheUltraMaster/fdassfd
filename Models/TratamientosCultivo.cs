using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Proyecto.Models;

[Table("Tratamientos_cultivo")]
[Index("IdCultivo", Name = "id_cultivo")]
[Index("IdTratamiento", Name = "id_tratamiento")]
public partial class TratamientosCultivo
{
    [Key]
    [Column("id", TypeName = "int(11)")]
    public int Id { get; set; }

    [Column("id_tratamiento", TypeName = "int(11)")]
    public int IdTratamiento { get; set; }

    [Column("id_cultivo", TypeName = "int(11)")]
    public int IdCultivo { get; set; }

    [Column("fecha_commit", TypeName = "datetime")]
    public DateTime FechaCommit { get; set; }

    [ForeignKey("IdCultivo")]
    [InverseProperty("TratamientosCultivos")]
    public virtual Cultivo IdCultivoNavigation { get; set; } = null!;

    [ForeignKey("IdTratamiento")]
    [InverseProperty("TratamientosCultivos")]
    public virtual Tratamiento IdTratamientoNavigation { get; set; } = null!;
}
