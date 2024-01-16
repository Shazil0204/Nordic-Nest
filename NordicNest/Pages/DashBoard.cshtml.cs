using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NordicNest.Model;

namespace NordicNest.Pages
{
	public class DashBoardModel : PageModel
	{

		////////////////////////////////////////////////////////////////////////////////////////////////// temp commented so that i can design my User Dashboard //

		//public IActionResult OnGet()
		//{
		//	// Check if the user is logged in
		//	if (HttpContext.Session.GetBool("IsLoggedIn") == true)
		//	{
		//		// If the user is logged in, return the Page
		//		return Page();
		//	}

		//	// If the user is not logged in, redirect to the login page or error page
		//	return RedirectToPage("/ShowErrorMessage");
		//}

		////////////////////////////////////////////////////////////////////////////////////////////////// temp commented so that i can design my User Dashboard //

		public string[] labels = { "1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12", "13", "14", "15", "16", "17", "18", "19", "20", "21", "22", "23", "24", "25", "26", "27", "28", "29", "30", "31" };
		public int[] data = { 0, 55, 5, 119, 202, 30, 45, 5, 2, 20, 30, 850, 0, 55, 5, 119, 202, 30, 45, 5, 2, 20, 30, 850, 1, 100, 100, 100, 100, 100, 100 };

		public void OnGet()
		{

		}
	}
}

// Extension methods to set and get Boolean values in session
public static class SessionExtensions
{
	// Extension method to set a boolean value in session
	public static void SetBool(this ISession session, string key, bool value)
	{
		session.SetInt32(key, value ? 1 : 0);
	}

	// Extension method to get a boolean value from session
	public static bool? GetBool(this ISession session, string key)
	{
		var value = session.GetInt32(key);
		return value.HasValue ? value == 1 : (bool?)null;
	}
}

