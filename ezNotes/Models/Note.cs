// File: Models/Note.cs
namespace ezNotes.Models
{
    public class Note
    {
        public int Id { get; set; }
        public string OwnerId { get; set; } = default!;   // <-- required
        public string Title { get; set; } = "";
        public string ContentMd { get; set; } = "";
        public DateTime CreatedUtc { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedUtc { get; set; } = DateTime.UtcNow; // <-- required
        public bool IsArchived { get; set; } = false;
    }
}
