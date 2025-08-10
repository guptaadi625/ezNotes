namespace ezNotes.Models
{
    public class Note
    {
        public int Id { get; set; }
        public string Title { get; set; } = "";
        public string ContentMd { get; set; } = "";
        public DateTime CreatedUtc { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedUtc { get; set; } = DateTime.UtcNow;
        public bool IsArchived { get; set; } = false;
    }
}
