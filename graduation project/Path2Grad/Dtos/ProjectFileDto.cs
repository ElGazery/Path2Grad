using System.ComponentModel.DataAnnotations;

namespace Path2Grad.Dtos
{
    public class ProjectFileDto
    {
        [Required]
        public IFormFile File {  get; set; }
        public int ProjectId { get; set; }

    }
}
