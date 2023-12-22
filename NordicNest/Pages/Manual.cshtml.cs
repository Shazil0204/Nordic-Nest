using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace NordicNest.Pages
{
    public class ManualModel : PageModel
    {
        private readonly ILogger<ManualModel> _logger;

        public ManualModel(ILogger<ManualModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
        }
    }

}
