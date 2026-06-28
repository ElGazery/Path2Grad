namespace Path2Grad.Models
{
    public class Field
    {
        public int FieldId { get; set; }
        public string FieldName { get; set; }
        public virtual ICollection<ProjectField> ProjectFields { get; set; } = new List<ProjectField>();
    }
}
