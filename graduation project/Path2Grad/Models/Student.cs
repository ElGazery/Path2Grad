using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace Path2Grad.Models;

[Index("ProjectId", Name = "IX_Students_ProjectID")]
[Index("StudentEmail", Name = "UQ__Students__3569CFDBFD684B18", IsUnique = true)]
public partial class Student
{
    [Key]
    [Column("StudentID")]
    public int StudentId { get; set; }

    [StringLength(255)]
    public string StudentName { get; set; } = null!;

    [StringLength(255)]
    public string StudentEmail { get; set; } = null!;

    [StringLength(255)]
    public string StudentPassword { get; set; } = null!;

    [StringLength(20)]
    public string? Phone { get; set; }

    public byte[]? Pic { get; set; }

    public int AcademicYear { get; set; }

    [Column("ProjectID")]
    public int? ProjectId { get; set; }

    [InverseProperty("Student")]
    public virtual ICollection<ChatBotConversation> ChatBotConversations { get; set; } = new List<ChatBotConversation>();

    [InverseProperty("Student")]
    public virtual Cv? Cv { get; set; }

    [InverseProperty("Student")]
    public virtual ICollection<Internship> Internships { get; set; } = new List<Internship>();

    [ForeignKey("ProjectId")]
    [InverseProperty("Students")]
    public virtual Project? Project { get; set; }

    [InverseProperty("Student")]
    public virtual ICollection<ProjectTask> Tasks { get; set; } = new List<ProjectTask>();

    public int? TrackId { get; set; }

    [ForeignKey("TrackId")]
    [InverseProperty("Students")]
    public virtual Track? Track { get; set; }


    [InverseProperty("Student")]
    public virtual TrackTest? TrackTest { get; set; }

    [ForeignKey("StudentId")]
    [InverseProperty("Students")]
    public virtual ICollection<Supervisor> Supervisors { get; set; } = new List<Supervisor>();
    public virtual ICollection<InternshipWorkFile> WorkFiles { get; set; }
    public virtual ICollection<InternshipCertificate> Certificates { get; set; }
    [InverseProperty("Student")]
    public virtual ICollection<StudentProjectJoinRequest> ProjectJoinRequests { get; set; } = new List<StudentProjectJoinRequest>();



}
