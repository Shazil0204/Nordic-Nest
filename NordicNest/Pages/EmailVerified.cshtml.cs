using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NordicNest.Controller.EmailVerficationClasses;

namespace NordicNest.Pages
{
    public class EmailVerifiedModel : PageModel
    {
		public bool IsSuccess { get; set; }
		public List<User> Users { get; set; }

		/// <summary>
		/// with this i check if i already have a user and if he is verified
		/// </summary>
		/// <param name="token"></param>
		public IActionResult OnGet(string token)
		{
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
				// This way i remove all the data that is temperarory stored in C#
				UserData.Users.Remove(user);

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
	}
}
