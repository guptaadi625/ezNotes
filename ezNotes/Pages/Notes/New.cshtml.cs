using ezNotes.Data;
using ezNotes.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore; // where used
using Markdig; // only in Edit.cshtml.cs

namespace ezNotes.Pages.Notes
{
    public class NewModel : PageModel
    {
        private readonly ApplicationDbContext _db;
        public NewModel(ApplicationDbContext db) => _db = db;
        [BindProperty]
        public ezNotes.Models.Note Input { get; set; } = new ezNotes.Models.Note();


        public void OnGet() { }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid) return Page();

            Input.CreatedUtc = DateTime.UtcNow;
            Input.UpdatedUtc = DateTime.UtcNow;

            _db.Notes.Add(Input);
            await _db.SaveChangesAsync();

            return RedirectToPage("./Edit", new { id = Input.Id });
        }
    }
}
