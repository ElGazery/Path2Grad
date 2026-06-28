using System.ComponentModel.DataAnnotations.Schema;

namespace Path2Grad.Models
{
    public class ProjectFile
    {
        public int ProjectFileId { get; set; }

        public string FileName { get; set; }

        public byte[] FileContent { get; set; }  // The actual file content (binary data)

        // Foreign key for the Project this file belongs to
        public int ProjectId { get; set; }

        // Navigation property to the Project
        [ForeignKey("ProjectId")]
        [InverseProperty("projectFiles")]
        public Project Project { get; set; }
    }
}
