// File: Data/ApplicationDbContext.cs
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ezNotes.Models; // <-- must match the namespace above

namespace ezNotes.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        // If VS keeps picking the wrong Note type, fully-qualify it:
        // public DbSet<ezNotes.Models.Note> Notes => Set<ezNotes.Models.Note>();
        public DbSet<Note> Notes => Set<Note>();

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<Note>()
                   .HasIndex(n => new { n.OwnerId, n.UpdatedUtc });
        }
    }
}
