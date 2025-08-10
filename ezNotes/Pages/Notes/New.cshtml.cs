using ezNotes.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ezNotes.Data;
using ezNotes.Models;
using System.Security.Claims;

namespace Noted.Pages.Notes
{
    [Authorize]
    public class NewModel : PageModel
    {
        private readonly ApplicationDbContext _db;
        public NewModel(ApplicationDbContext db) => _db = db;

        [BindProperty] public Note Input { get; set; } = new();

        public void OnGet() { }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid) return Page();

            var uid = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
            Input.OwnerId = uid;
            Input.CreatedUtc = Input.UpdatedUtc = DateTime.UtcNow;
            if (string.IsNullOrWhiteSpace(Input.Title)) Input.Title = "Untitled";

            _db.Notes.Add(Input);
            await _db.SaveChangesAsync();
            return RedirectToPage("Edit", new { id = Input.Id });
        }
    }
}
