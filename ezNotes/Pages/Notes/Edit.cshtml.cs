using ezNotes.Data;
using ezNotes.Models;
using Markdig;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
// only in Edit.cshtml.cs


namespace ezNotes.Pages.Notes
{
    public class EditModel : PageModel
    {
        private readonly ApplicationDbContext _db;
        public EditModel(ApplicationDbContext db) => _db = db;

        [BindProperty]
        public Note Input { get; set; } = default!;

        public string HtmlPreview { get; set; } = "";

        public async Task<IActionResult> OnGetAsync(int id)
        {
            var note = await _db.Notes.FirstOrDefaultAsync(n => n.Id == id);
            if (note == null) return RedirectToPage("./Index");

            Input = note;
            HtmlPreview = Markdown.ToHtml(Input.ContentMd ?? "");
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid) return Page();

            var existing = await _db.Notes.FirstOrDefaultAsync(n => n.Id == Input.Id);

            if (existing == null) return RedirectToPage("./Index");

            existing.Title = Input.Title ?? "";
            existing.ContentMd = Input.ContentMd ?? "";
            existing.IsArchived = Input.IsArchived;
            existing.UpdatedUtc = DateTime.UtcNow;

            await _db.SaveChangesAsync();
            return RedirectToPage("./Edit", new { id = existing.Id });
        }

        public async Task<IActionResult> OnPostDeleteAsync(int id)
        {
            var note = await _db.Notes.FindAsync(id);
            if (note != null)
            {
                _db.Notes.Remove(note);
                await _db.SaveChangesAsync();
            }
            return RedirectToPage("./Index");
        }
    }
}
