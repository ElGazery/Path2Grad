using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Path2Grad.Domain.Entities;

[Table("ProjectsAdmin")]
public partial class ProjectsAdmin
{
    [Key]
    [Column("AdminID")]
    public int AdminId { get; set; }

    [StringLength(255)]
    public string AdminName { get; set; } = null!;

    [StringLength(255)]
    public string AdminEmail { get; set; } = null!;

    [StringLength(255)]
    public string AdminPassword { get; set; } = null!;

    public byte[]? Pic { get; set; }
}
