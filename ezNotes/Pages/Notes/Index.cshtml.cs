using ezNotes.Data;
using ezNotes.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc; // where used
using Markdig; // only in Edit.cshtml.cs

namespace ezNotes.Pages.Notes
{
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _db;
        public IndexModel(ApplicationDbContext db) => _db = db;

        public IList<Note> Items { get; set; } = new List<Note>();
        public string? Q { get; set; }

        public async Task OnGetAsync(string? q)
        {
            Q = q;
            IQueryable<Note> query = _db.Notes;

            if (!string.IsNullOrWhiteSpace(q))
            {
                query = query.Where(n => n.Title.Contains(q) || n.ContentMd.Contains(q));
            }

            Items = await query
                .OrderByDescending(n => n.UpdatedUtc)
                .Take(500) // sane cap
                .ToListAsync();
        }
    }
}
