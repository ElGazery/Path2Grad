using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Path2Grad.Models;

//[Index("StudentId", Name = "IX_Tracks_StudentID", IsUnique = true)]
public partial class Track
{
    [Key]
    [Column("TrackID")]
    public int TrackId { get; set; }

    [StringLength(255)]
    public string TrackName { get; set; } = null!;

    [InverseProperty("Track")]
    public virtual ICollection<Student> Students { get; set; } = new List<Student>();

    public ICollection<TrackItem> Items { get; set; }

}