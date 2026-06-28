namespace Path2Grad.Models
{
    public class ItemLesson
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool? IsComplet { get; set; }
       

        // Foreign key to Item
        public TrackItem TrackItem { get; set; }
    }
}