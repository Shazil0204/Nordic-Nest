// dotnet add package BCrypt.Net-Next

using BCrypt.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Cryptography;
using NordicNest.Controller.EmailVerficationClasses;
using System.Runtime.CompilerServices;

namespace NordicNest.Pages
{
    public class RegisterModel : PageModel
    {
        private readonly EmailSender _emailSender;
        public bool IsSuccess { get; set; }
        public List<User> Users { get; set; }

        internal bool isVarified;

        internal bool allComplete;

        internal bool emailExist;
        
        internal bool usernameExist;

        private string _username;

		private string _password;


		public RegisterModel(EmailSender emailSender)
		{
            _emailSender = emailSender;
        }

        /// <summary>
        /// with this i check if i already have a user and if he is verified
        /// </summary>
        /// <param name="token"></param>
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

            // Token validity duration (e.g., 5 min)
            TimeSpan tokenValidityDuration = TimeSpan.FromHours(0.05);

            if (user == null)
            {
                Console.WriteLine("working");
            }

            // Check if a user was found and if the token is still within the validity period
            else if (user != null && (DateTime.UtcNow - user.TokenCreated) <= tokenValidityDuration)
            {
                Users = UserData.Users;
                // If a user was found and the token is valid, set their status to verified
                user.IsVerified = true;
                // Indicate success in the response
                IsSuccess = true;

                isVarified = true;

                Console.WriteLine("well done");
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
            }

            else
            {
                // If no user was found or the token has expired, set the response to indicate failure
                IsSuccess = false;
                // Removing user so that i don't have so many data in my list
                UserData.Users.Remove(user);
            }
        }

        public async Task<IActionResult> OnPostAsync(string email)
        {
            Console.WriteLine(email);

            Model.DbUserEntry.CheckEmail CE = new Model.DbUserEntry.CheckEmail();
            int i = CE.CheckEmailIfExist(email);

            if (i == 0)
            {
				// creating an instance of user.cs here and adding the basic information that user.cs needs
				var user = new User
				{
					Email = email,
					VerificationToken = GenerateToken(),
					TokenCreated = DateTime.UtcNow, // Set the token creation time
					IsVerified = false
				};

				// over here i added the instance into the list that i have in UserData.cs 
				UserData.Users.Add(user);

				// This is how i create a verification link
				var verificationLink = Url.PageLink(pageName: "/Register", values: new { token = user.VerificationToken });

				// This is the message that user will see in it's email
				await _emailSender.SendEmailAsync(email, "Verify your email", $"Please verify your email by clicking <a href='{verificationLink}'>here</a>.");

                Console.WriteLine("return value in CheckEmailIfExist is: " + i);

				return Page();
			}
            else
            {
				TempData["emailExist"] = true; // Set emailExist in TempData

                Console.WriteLine("return value in CheckEmailIfExist is: " + i);

				return Page();
            }
        }

        public IActionResult OnPostAllDone(string firstname, string lastname, int userage, string username, string password)
        {
			Model.DbUserEntry.CheckUserName CUN = new Model.DbUserEntry.CheckUserName();
			int i = CUN.CheckUserNameIfExist(username);

            if (i == 0)
            {
				_username = HashData(username);
				_password = HashData(password);

				Console.WriteLine(_username + _password);

				TempData["allComplete"] = true;

                Console.WriteLine("return value in CheckUserNameIfExist is: " + i);

				return Page();
			}

            else
            {
                TempData["usernameExist"] = true;
                
                Console.WriteLine("return value in CheckUserNameIfExist is: " + i);

				return Page();
			}
        }

        /// <summary>
        /// This is how i generate a Token
        /// </summary>
        /// <returns></returns>
        private string GenerateToken()
        {            
            using (var rng = new RNGCryptoServiceProvider())
            {
                byte[] randomBytes = new byte[32];
                rng.GetBytes(randomBytes);
                return Convert.ToBase64String(randomBytes);
            }
        }

        /// <summary>
        /// This is how i hash Username and Password
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        private static string HashData(string data)
        {
            return BCrypt.Net.BCrypt.HashPassword(data);
        }
    }
}
