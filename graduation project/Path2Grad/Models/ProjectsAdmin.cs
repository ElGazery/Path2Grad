using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Path2Grad.Models;

[Table("ProjectsAdmin")]
[Index("AdminEmail", Name = "UQ__Projects__F2AA7AD96AD4F7D5", IsUnique = true)]
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
