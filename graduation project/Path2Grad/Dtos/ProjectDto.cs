using System.ComponentModel.DataAnnotations;
using Path2Grad.Models;
using Path2Grad.Dtos;
namespace Path2Grad.Dtos
{
    public class ProjectDto
    {
        public string ProjectName { get; set; }
        public string? Description { get; set; }
        public List<FieldDto> Fields { get; set; }
        public int NumberOfTeam { get;set; }
       
    }
}
