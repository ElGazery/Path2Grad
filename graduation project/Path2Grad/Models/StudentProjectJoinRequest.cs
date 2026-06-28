using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Path2Grad.Models;

public class StudentProjectJoinRequest
{
    [Key]
    public int RequestId { get; set; }
    public int SenderId { get; set; }
    public int StudentId { get; set; }
    public int? ProjectId { get; set; }

    [StringLength(50)]
    public string Status { get; set; } = "Pending"; // "Pending", "Approved", "Rejected"
    [ForeignKey("SenderId")]
    public virtual Student Sender { get; set; } = null!;

    [ForeignKey("StudentId")]
    public virtual Student Student { get; set; } = null!;

    [ForeignKey("ProjectId")]
    public virtual Project Project { get; set; } = null!;
}
