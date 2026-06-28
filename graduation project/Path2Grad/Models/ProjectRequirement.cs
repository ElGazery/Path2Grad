using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Path2Grad.Models
{
    public class ProjectRequirement
    {
        [Key]
       public int RequirementId { get; set; }   
       public string RequirementName { get; set; }
       public byte[] PdfContent { get; set; } // PDF file
        public int ProjectId { get; set; }  // Just use this

        // 
        [ForeignKey("ProjectId")]  // Explicitly linking the foreign key
        [InverseProperty("Requirements")]  // Points back to the Requirements collection in Project
        public Project Project { get; set; }
    }
}
