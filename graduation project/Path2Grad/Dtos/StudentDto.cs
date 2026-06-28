using System.ComponentModel.DataAnnotations;

namespace Path2Grad.Dtos
{
    public class StudentDto
    {
        public int StudentId { get; set; }

        public string StudentName { get; set; }

        public byte[]? Pic { get; set; }
    }
}
