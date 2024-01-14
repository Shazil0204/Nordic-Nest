using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace C__To_JS_Through_JSON.Pages
{
    public class IndexModel : PageModel
    {
        public string[] labels = { "11", "22", "33", "44", "55", "66", "77", "88", "99", "1010", "1111", "1212" };
        public int[] data = { 0, 10, 5, 2, 20, 30, 45, 5, 2, 20, 30, 100 };

        public void OnGet()
        {
        }
    }
}
