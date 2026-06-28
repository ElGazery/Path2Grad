using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Path2Grad.Models
{
    public class SupervisorProject
    {
        [Key]
        [Column("ID")]
        public int Id { get; set; }

        public int SupervisorId { get; set; }
        public int ProjectId { get; set; }

        [ForeignKey("SupervisorId")]
        public virtual Supervisor Supervisor { get; set; } = null!;

        [ForeignKey("ProjectId")]
        public virtual Project Project { get; set; } = null!;
    }

}
