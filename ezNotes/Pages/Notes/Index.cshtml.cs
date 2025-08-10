using ezNotes.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ezNotes.Data;
using ezNotes.Models;
using System.Security.Claims;

namespace Noted.Pages.Notes
{
    [Authorize]
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _db;
        public IndexModel(ApplicationDbContext db) => _db = db;

        public IList<Note> Notes { get; set; } = new List<Note>();
        public string? q { get; set; }

        public async Task OnGetAsync(string? q)
        {
            this.q = q;
            var uid = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
            var query = _db.Notes.Where(n => n.OwnerId == uid && !n.IsArchived);

            if (!string.IsNullOrWhiteSpace(q))
            {
                q = q.Trim();
                query = query.Where(n => n.Title.Contains(q) || n.ContentMd.Contains(q));
            }

            Notes = await query.OrderByDescending(n => n.UpdatedUtc)
                               .Take(200).ToListAsync();
        }
    }
}
