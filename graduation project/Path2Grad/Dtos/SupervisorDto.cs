using System.ComponentModel.DataAnnotations;

namespace Path2Grad.Dtos
{
    public class SupervisorDto
    {
        public int SupervisorId { get; set; }
        public string SupervisorName { get; set; } 
        public byte[]? Pic { get; set; }
    }
}
