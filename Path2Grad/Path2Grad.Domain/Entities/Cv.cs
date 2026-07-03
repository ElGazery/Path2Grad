using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Path2Grad.Domain.Entities;

[Table("CV")]
public partial class Cv
{
    [Key]
    [Column("CVID")]
    public int Cvid { get; set; }
    public string CVName { get; set; }

    [StringLength(255)]
    public string? Link { get; set; }

    [Column("CVFile")]
    public byte[]? Cvfile { get; set; }
    public string Type { get; set; }

    [Column("StudentID")]
    public int? StudentId { get; set; }

    [ForeignKey("StudentId")]
    [InverseProperty("Cv")]
    public virtual Student Student { get; set; } = null!;
}
