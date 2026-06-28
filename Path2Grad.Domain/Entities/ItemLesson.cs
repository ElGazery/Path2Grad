namespace Path2Grad.Domain.Entities
{
    public class ItemLesson
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool? IsComplet { get; set; }

        public TrackItem TrackItem { get; set; }
    }
}
