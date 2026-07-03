using System.Collections.Generic;

namespace Path2Grad.Application.Dtos
{
    public class ProjectDto
    {
        public string ProjectName { get; set; }
        public string? Description { get; set; }
        public List<FieldDto> Fields { get; set; }
        public int NumberOfTeam { get; set; }
    }
}
