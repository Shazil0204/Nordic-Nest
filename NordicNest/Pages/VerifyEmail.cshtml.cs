using EmailVerification;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace NordicNest.Pages
{
    public class VerifyEmailModel : PageModel
    {
        public bool IsSuccess { get; set; }

        public void OnGet(string token)
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

            // Token validity duration (e.g., 24 hours)
            TimeSpan tokenValidityDuration = TimeSpan.FromHours(24);

            // Check if a user was found and if the token is still within the validity period
            if (user != null && (DateTime.UtcNow - user.TokenCreated) <= tokenValidityDuration)
            {
                // If a user was found and the token is valid, set their status to verified
                user.IsVerified = true;
                // Indicate success in the response
                IsSuccess = true;
            }
            else
            {
                // If no user was found or the token has expired, set the response to indicate failure
                IsSuccess = false;
            }
        }
    }
}
