using System.Collections.Generic;

namespace Path2Grad.Domain.Entities
{
    public class TrackItem
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public int TrackId { get; set; }

        public Track Track { get; set; }

        public ICollection<ItemLesson> ItemLessons { get; set; }
    }
}
