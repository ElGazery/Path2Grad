using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Path2Grad.Models;

[Table("TeamMember")]
[Index("ProjectId", Name = "IX_TeamMember_ProjectID")]
public partial class TeamMember
{
    [Key]
    [Column("TeamMemberID")]
    public int TeamMemberId { get; set; }

    [Column("TeamMember")]
    [StringLength(255)]
    public string TeamMember1 { get; set; } = null!;

    [Column("ProjectID")]
    public int ProjectId { get; set; }

    [ForeignKey("ProjectId")]
    [InverseProperty("TeamMembers")]
    public virtual Project Project { get; set; } = null!;
}
