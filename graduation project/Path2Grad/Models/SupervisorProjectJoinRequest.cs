using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Path2Grad.Models
{
    public class SupervisorProjectJoinRequest
    {
        [Key]
        [Column("RequestId")]
        public int RequestId { get; set; }
        public int StudentId { get; set; }
        public int SupervisorId { get; set; } // Supervisor receiving the request
        public int? ProjectId { get; set; }

        [StringLength(50)]
        public string Status { get; set; } = "Pending"; // "Pending", "Approved", "Rejected"
        

        [ForeignKey("SupervisorId")]
        public virtual Supervisor Supervisor { get; set; } = null!;
        [ForeignKey("ProjectId")]
        public virtual Project Project { get; set; } = null!;
    }

}

