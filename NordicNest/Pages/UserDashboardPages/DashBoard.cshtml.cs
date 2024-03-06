using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NordicNest.Controller.DefaultSLController;
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
		public int[] data = { 13, 15, 5, 19, 20, 30, 15, 5, 2, 20, 30, 11, 11, 15, 5, 11, 20, 30, 15, 5, 2, 20, 30, 11, 11, 10, 10, 10, 10, 10, 10 };
		public int firstProgressBar = 59;
		public int secondProgressBar = 70;
        public int SavingProgressBar = 59;
        public int LoanProgressBar = 70;
        public void OnGet()
        {
        }

        public decimal Savingpercentage()
        {
            DefaultSavingController DSC = new DefaultSavingController();
            var Sresult = DSC.GetData();

            decimal percentage = (Sresult.Item2 / Sresult.Item3) * 100;

            return percentage;
        }

        public decimal Loanpercentage()
        {
            DefaultLoanController DLC = new DefaultLoanController();
            var Lresult = DLC.GetData();

            decimal percentage = (Lresult.Item2 / Lresult.Item3) * 100;

            return percentage;
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

