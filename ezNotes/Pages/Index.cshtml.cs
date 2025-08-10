using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ezNotes.Pages
{
    public class IndexModel : PageModel
    {
        public IActionResult OnGet()
        {
            return RedirectToPage("/Notes/Index");
        }
    }
}
