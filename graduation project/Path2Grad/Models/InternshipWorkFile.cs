using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Path2Grad.Models;

[Index("InternshipId", Name = "IX_InternshipWorkFiles_InternshipID")]
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
