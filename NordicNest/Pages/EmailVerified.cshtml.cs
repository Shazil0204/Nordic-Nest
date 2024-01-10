using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NordicNest.Controller;
using NordicNest.Controller.EmailVerficationClasses;
using NordicNest.Model;

namespace NordicNest.Pages
{
	public class EmailVerifiedModel : PageModel
	{
		public bool IsSuccess { get; set; }
		public List<User> Users { get; set; }

		internal string hashpassword;

		internal string email = "NULL";

		internal bool gender;

		/// <summary>
		/// with this i check if i already have a user and if he is verified
		/// </summary>
		/// <param name="token"></param>
		public IActionResult OnGet(string token)
		{
			TempData["result"] = 0;

			// Initialize a variable to hold the user, starting as null
			User user = null;

			// Iterate through the list of users
			foreach (var u in UserData.Users)
			{
				// Check if the current user has the matching verification token
				if (u.VerificationToken == token)
				{
					// If a match is found, assign this user to the 'user' variable
					user = u;
					// Exit the loop as we found the user we were looking for
					break;
				}

			}

			// Token validity duration (e.g., 5 min)
			TimeSpan tokenValidityDuration = TimeSpan.FromHours(0.05);

			// Check if a user was found and if the token is still within the validity period
			if (user != null && (DateTime.UtcNow - user.TokenCreated) <= tokenValidityDuration)
			{
				Users = UserData.Users;
				// If a user was found and the token is valid, set their status to verified
				user.IsVerified = true;
				// Temparory storing the data
				TempData["UserEmail"] = user.Email;

				BasicProperties.Email = user.Email;

				return Page();
			}

			else if (user != null && (DateTime.UtcNow - user.TokenCreated) > tokenValidityDuration)
			{
				Users = UserData.Users;
				// If a user was found but the token is invalid, set their status to not verified yet.
				user.IsVerified = false;
				// Indicate unsuccess in the response
				IsSuccess = false;
				// Removing user so that i don't have so many data in my list
				UserData.Users.Remove(user);

				return Page();
			}

			else
			{
				// If no user was found or the token has expired, set the response to indicate failure
				IsSuccess = false;
				// Removing user so that i don't have so many data in my list
				UserData.Users.Remove(user);

				return RedirectToPage("/Register");
			}
		}

		public IActionResult OnPost(string Firstname, string Lastname, int Userage, string UserGender, string Username, string Password)
		{

			TempData["fn"] = Firstname;
			TempData["ln"] = Lastname;
			TempData["ua"] = Userage;
			TempData["un"] = Username;

			gender = false;
			hashpassword = HashData(Password);

			if (UserGender == "Male")
			{
				gender = true;
			}
			else if (UserGender == "Female")
			{
				gender = false;
			}

			Model.DbUserEntry.InsertClient IC = new Model.DbUserEntry.InsertClient();

			int i = IC.AddNewClient(Firstname, Lastname, Username, hashpassword, gender, Userage);

			if (i == 2)
			{
				TempData["result"] = 2;
			}
			else if (i == 1)
			{
				User user = null;
				TempData["result"] = 1;
				UserData.Users.Remove(user);
			}
			else if (i == -99)
			{
				TempData["result"] = -99;
			}

			return Page();
		}

		private static string HashData(string data)
		{
			return BCrypt.Net.BCrypt.HashPassword(data);
		}

		public IActionResult OnPostError()
		{
			ModelState.Clear(); // Clear ModelState to remove validation errors
			TempData["result"] = 0;
			// Redirect to a different page after handling the "Retry" action
			return RedirectToPage("/EmailVerified");
		}

        public bool IsUserTrue()
        {
            BasicProperties.IsNew = true;
            return BasicProperties.IsNew; // Return a boolean value
        }

    }
}
