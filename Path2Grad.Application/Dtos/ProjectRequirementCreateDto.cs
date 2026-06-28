using Microsoft.AspNetCore.Http;

namespace Path2Grad.Application.Dtos
{
    public class ProjectRequirementCreateDto
    {
        public string RequirementName { get; set; }
        public IFormFile PdfFile { get; set; }
        public int ProjectId { get; set; }
    }
}
