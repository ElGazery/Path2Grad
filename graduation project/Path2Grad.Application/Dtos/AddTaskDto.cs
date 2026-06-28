using System;

namespace Path2Grad.Application.Dtos
{
    public class AddTaskDto
    {
        public string TaskName { get; set; }
        public int StudentId { get; set; }
        public int ProjectId { get; set; }
        public DateTime Deadline { get; set; }
    }
}
