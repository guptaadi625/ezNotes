using ezNotes.Data;
using Microsoft.EntityFrameworkCore;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ApplicationDbContext>(o =>
    o.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddRazorPages();

var app = builder.Build();

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

app.MapRazorPages();

app.Run();
