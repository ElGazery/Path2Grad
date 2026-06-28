namespace Path2Grad.Models
{
    public class TrackItem
    {
        public int Id { get; set; }
        public string Name { get; set; }

        // Foreign key to Track
        public int TrackId { get; set; }

        public Track Track { get; set; }

        // Navigation property
        public ICollection<ItemLesson> ItemLessons { get; set; }
    }
}