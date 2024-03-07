using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NordicNest.Model;
using BCrypt.Net;

namespace NordicNest.Pages
{
    public class LoginModel : PageModel
    {
        Model.Login.UserLogin UL = new Model.Login.UserLogin();
        public void OnGet()
        {
            TempData["LoginReturnData"] = 0;
        }

        public IActionResult OnPost(string username, string password)
        {

            var userLogin = new Model.Login.UserLogin();
            var (returnpassword, returnvalue) = userLogin.CheckUser(username);

            if (returnvalue == 1)
            {

                bool verified = BCrypt.Net.BCrypt.Verify(password, returnpassword);

				if (verified)
				{
					TempData["LoginReturnData"] = returnvalue;
					// Set session variable to indicate the user is logged in
					HttpContext.Session.SetBool("IsLoggedIn", true);

					return RedirectToPage("/UserDashboardPages/DashBoard"); // This will send him to the dashboard
				}

				// If password is incorrect
				else
				{
                    TempData["LoginReturnData"] = -2;
                    return Page();
                }
            }

            // if an error occou in database procedure
            else if (returnvalue == -99)
            {
                TempData["LoginReturnData"] = returnvalue;
                return Page();
            }

            // if an error occur in C# code
            else if (returnvalue == -101)
            {
                TempData["LoginReturnData"] = returnvalue;
                return Page();
            }

            // if something else happened
            // i believe it won't be necessary but this is just for safety
            else
            {
                TempData["LoginReturnData"] = returnvalue;
                return Page();
            }
        }

        public IActionResult OnPostRetry()
        {
            return RedirectToPage("/Login");
        }
    }
}
