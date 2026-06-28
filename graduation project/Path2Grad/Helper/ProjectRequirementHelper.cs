using Path2Grad.Dtos;
using Path2Grad.Models;

namespace Path2Grad.Helper
{
    public static class ProjectRequirementHelper
    {
        public static ProjectRequirement ToEntity(ProjectRequirementCreateDto dto)
        {
            using var memoryStream = new MemoryStream();
            dto.PdfFile.CopyTo(memoryStream);

            return new ProjectRequirement
            {
                RequirementName = dto.RequirementName,
                PdfContent = memoryStream.ToArray(),
                ProjectId = dto.ProjectId
            };
        }
    }
}
