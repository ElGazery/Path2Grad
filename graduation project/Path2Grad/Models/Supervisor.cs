using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Path2Grad.Models;

[Index("SupervisorEmail", Name = "UQ__Supervis__FAB2DC03A7DD1066", IsUnique = true)]
public partial class Supervisor
{
    [Key]
    [Column("SupervisorID")]
    public int SupervisorId { get; set; }

    [StringLength(255)]
    public string SupervisorName { get; set; } = null!;

    [StringLength(255)]
    public string SupervisorEmail { get; set; } = null!;

    [StringLength(255)]
    public string SupervisorPassword { get; set; } = null!;

    [StringLength(20)]
    public string? Phone { get; set; }

    [StringLength(255)]
    public string Position { get; set; } = null!;

    [StringLength(255)]
    public string Specialization { get; set; } = null!;

    public byte[]? Pic { get; set; }

   

    [ForeignKey("SupervisorId")]
    [InverseProperty("Supervisors")]
    public virtual ICollection<Student> Students { get; set; } = new List<Student>();
    public virtual ICollection<SupervisorProjectJoinRequest> ProjectJoinRequests { get; set; } = new List<SupervisorProjectJoinRequest>();
    [InverseProperty("Supervisor")]
    public virtual ICollection<SupervisorProject> SupervisorProjects { get; set; } = new List<SupervisorProject>();
}
