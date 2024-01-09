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

        Model.BasicProperties BP = new Model.BasicProperties();

		public RegisterModel(EmailSender emailSender)
		{
            _emailSender = emailSender;
        }

        public void OnGet()
        {

        }

        public async Task<IActionResult> OnPostAsync(string email)
        {
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
				var verificationLink = Url.PageLink(pageName: "/EmailVerified", values: new { token = user.VerificationToken });

				// This is the message that user will see in it's email
				await _emailSender.SendEmailAsync(email, "Verify your email", $"Please verify your email by clicking <a href='{verificationLink}'>here</a>.");

                Console.WriteLine("return value in CheckEmailIfExist is: " + i);

				return Page();
			}
            else
            {
                BP.EmailExist = true;
				TempData["storeData"] = BP.EmailExist;

				Console.WriteLine("return value in CheckEmailIfExist is: " + i);

				return Page();
            }
        }

		public IActionResult OnPostEmailAlreadyExist()
		{
			ModelState.Clear(); // Clear ModelState to remove validation errors
			BP.EmailExist = false;
			TempData["storeData"] = BP.EmailExist;

			// Redirect to a different page after handling the "Retry" action
			return RedirectToPage("/SomeOtherPage"); // Change "/SomeOtherPage" to your desired URL
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
    }
}
