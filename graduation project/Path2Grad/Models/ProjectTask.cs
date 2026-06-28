using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Path2Grad.Models;

[Index("ProjectId", Name = "IX_Tasks_ProjectID")]
[Index("StudentId", Name = "IX_Tasks_StudentID")]
public partial class ProjectTask
{
    [Key]
    [Column("TaskID")]
    public int TaskId { get; set; }

    [StringLength(255)]
    public string TaskName { get; set; } = null!;

    public string? Description { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime Deadline { get; set; }

    [StringLength(50)]
    public string Status { get; set; } = null!;

    [Column("ProjectID")]
    public int ProjectId { get; set; }

    [Column("StudentID")]
    public int StudentId { get; set; }

    [ForeignKey("ProjectId")]
    [InverseProperty("Tasks")]
    public virtual Project Project { get; set; } = null!;

    [ForeignKey("StudentId")]
    [InverseProperty("Tasks")]
    public virtual Student Student { get; set; } = null!;
}
