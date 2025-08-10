using Microsoft.AspNetCore.Mvc.RazorPages;

public class IndexModel : PageModel
{
    public List<Note> Notes { get; set; } = new();

    public void OnGet()
    {
        // Example: Replace with your data source
        Notes = new List<Note>
        {
            new Note { Id = 1, Title = "Welcome to Notion Notes", Content = "This is a sample note. Click + New Note to add more!" },
            new Note { Id = 2, Title = "Second Note", Content = "You can customize this app as you like." }
        };
    }
}

public class Note
{
    public int Id { get; set; }
    public string Title { get; set; } = "";
    public string Content { get; set; } = "";
}