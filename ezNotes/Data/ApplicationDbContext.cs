using ezNotes.Models;            // <-- REQUIRED
using Microsoft.EntityFrameworkCore;

namespace ezNotes.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Note> Notes => Set<Note>();

        protected override void OnModelCreating(ModelBuilder b)
        {
            base.OnModelCreating(b);
            b.Entity<Note>().HasIndex(n => n.UpdatedUtc);
        }
    }
}
