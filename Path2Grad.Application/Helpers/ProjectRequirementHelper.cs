using System.IO;
using Path2Grad.Application.Dtos;
using Path2Grad.Domain.Entities;

namespace Path2Grad.Application.Helpers
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
