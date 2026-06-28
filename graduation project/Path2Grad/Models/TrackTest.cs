using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Path2Grad.Models;

[Table("TrackTest")]
[Index("StudentId", Name = "IX_TrackTest_StudentID", IsUnique = true)]
public partial class TrackTest
{
    [Key]
    [Column("TrackTestID")]
    public int TrackTestId { get; set; }

    public string Question { get; set; } = null!;

    public string Answer { get; set; } = null!;

    [Column("StudentID")]
    public int StudentId { get; set; }

    [ForeignKey("StudentId")]
    [InverseProperty("TrackTest")]
    public virtual Student Student { get; set; } = null!;
}
