using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Path2Grad.Models;

[Index("StudentId", Name = "IX_Internships_StudentID")]
public partial class Internship
{
    [Key]
    [Column("InternshipID")]
    public int InternshipId { get; set; }

    [StringLength(255)]
    public string InternshipName { get; set; } = null!;

    [StringLength(255)]
    public string? InternshipLink { get; set; }

    [Column("StudentID")]
    public int? StudentId { get; set; }

    [InverseProperty("Internship")]
    public virtual ICollection<InternshipCertificate> InternshipCertificates { get; set; } = new List<InternshipCertificate>();

    [InverseProperty("Internship")]
    public virtual ICollection<InternshipWorkFile> InternshipWorkFiles { get; set; } = new List<InternshipWorkFile>();

    [ForeignKey("StudentId")]
    [InverseProperty("Internships")]
    public virtual Student Student { get; set; } = null!;
}
