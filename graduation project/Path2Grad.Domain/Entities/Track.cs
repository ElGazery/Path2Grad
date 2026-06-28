using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Path2Grad.Domain.Entities;

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
