using System.ComponentModel.DataAnnotations;

namespace Path2Grad.Dtos
{
    public class ProjectRequirementCreateDto
    {
        [Required]
        public string RequirementName { get; set; }

        [Required]
        public IFormFile PdfFile { get; set; } 

        [Required]
        public int ProjectId { get; set; }
    }
}
