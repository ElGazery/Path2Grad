using Microsoft.AspNetCore.Http;

namespace Path2Grad.Application.Dtos
{
    public class ProjectFileDto
    {
        public IFormFile File { get; set; }
        public int ProjectId { get; set; }
    }
}
