using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Path2Grad.Domain.Entities;

[Table("TeamMember")]
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
