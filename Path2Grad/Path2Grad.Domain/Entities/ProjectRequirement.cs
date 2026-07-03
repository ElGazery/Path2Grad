using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Path2Grad.Domain.Entities
{
    public class ProjectRequirement
    {
        [Key]
        public int RequirementId { get; set; }
        public string RequirementName { get; set; }
        public byte[] PdfContent { get; set; }
        public int ProjectId { get; set; }

        [ForeignKey("ProjectId")]
        [InverseProperty("Requirements")]
        public Project Project { get; set; }
    }
}
