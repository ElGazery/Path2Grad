using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Path2Grad.Models;

[Index("InternshipId", Name = "IX_InternshipCertificates_InternshipID")]
public partial class InternshipCertificate
{
    [Key]
    [Column("InternshipCertificatesID")]
    public int InternshipCertificatesId { get; set; }

    public byte[] Certificate { get; set; } = null!;

    [Column("InternshipID")]
    public int? InternshipId { get; set; }
    public int StudentId { get; set; }

    [ForeignKey("StudentId")]
    public virtual Student Student { get; set; }

    [ForeignKey("InternshipId")]
    [InverseProperty("InternshipCertificates")]
    public virtual Internship Internship { get; set; } = null!;
}
