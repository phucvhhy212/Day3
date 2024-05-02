using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Day3.Pages
{
    [IgnoreAntiforgeryToken]
    public class IndexModel : PageModel
    {
        public IndexModel()
        {
            
        }
        public void OnGet()
        {

        }
        public IActionResult OnPost([FromBody] int id)
        {
            var x = id;
            return Page();
        }

    }
}
