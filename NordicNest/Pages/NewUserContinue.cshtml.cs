using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace NordicNest.Pages
{
	public class NewUserContinueModel : PageModel
	{
		public IActionResult OnGet()
		{
			if (Model.BasicProperties.IsNew == true)
			{
				return Page();
			}
			else
			{
				return RedirectToPage("/ShowErrorMessage");
			}
		}
	}
}
