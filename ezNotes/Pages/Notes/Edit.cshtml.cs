using ezNotes.Data;
using Markdig;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ezNotes.Data;
using ezNotes.Models;
using System.Security.Claims;

namespace Noted.Pages.Notes
{
    [Authorize]
    public class EditModel : PageModel
    {
        private readonly ApplicationDbContext _db;
        public EditModel(ApplicationDbContext db) => _db = db;

        [BindProperty] public Note Input { get; set; } = default!;
        public string HtmlPreview { get; set; } = "";

        public async Task<IActionResult> OnGetAsync(int id)
        {
            var uid = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
            var note = await _db.Notes.FirstOrDefaultAsync(n => n.Id == id && n.OwnerId == uid);
            if (note is null) return NotFound();

            Input = note;
            HtmlPreview = Markdown.ToHtml(Input.ContentMd ?? "");
            return Page();
        }

        public async Task<IActionResult> OnPostSaveAsync()
        {
            if (!ModelState.IsValid) return Page();
            var uid = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
            var note = await _db.Notes.FirstOrDefaultAsync(n => n.Id == Input.Id && n.OwnerId == uid);
            if (note is null) return NotFound();

            note.Title = Input.Title ?? "Untitled";
            note.ContentMd = Input.ContentMd ?? "";
            note.UpdatedUtc = DateTime.UtcNow;
            await _db.SaveChangesAsync();

            TempData["Saved"] = "Saved.";
            return RedirectToPage(new { id = note.Id });
        }

        public async Task<IActionResult> OnPostDeleteAsync(int id)
        {
            var uid = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
            var note = await _db.Notes.FirstOrDefaultAsync(n => n.Id == id && n.OwnerId == uid);
            if (note is null) return NotFound();

            _db.Notes.Remove(note);
            await _db.SaveChangesAsync();
            return RedirectToPage("Index");
        }
    }
}
