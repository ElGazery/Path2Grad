using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Path2Grad.Domain.Entities;

public partial class InternshipWorkFile
{
    [Key]
    [Column("InternshipWorkFilesID")]
    public int InternshipWorkFilesId { get; set; }

    public byte[] WorkFile { get; set; } = null!;

    [Column("InternshipID")]
    public int? InternshipId { get; set; }
    public int StudentId { get; set; }

    [ForeignKey("StudentId")]
    public virtual Student Student { get; set; }
    [ForeignKey("InternshipId")]
    [InverseProperty("InternshipWorkFiles")]
    public virtual Internship Internship { get; set; } = null!;
}
