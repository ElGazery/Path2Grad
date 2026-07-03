using System.ComponentModel.DataAnnotations.Schema;

namespace Path2Grad.Domain.Entities
{
    public class ProjectFile
    {
        public int ProjectFileId { get; set; }

        public string FileName { get; set; }

        public byte[] FileContent { get; set; }

        public int ProjectId { get; set; }

        [ForeignKey("ProjectId")]
        [InverseProperty("projectFiles")]
        public Project Project { get; set; }
    }
}
