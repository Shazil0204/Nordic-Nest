using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Runtime.CompilerServices;
using System.Text.Json;
using System.Xml;

namespace NordicNest.Pages
{
	public class Contact_FormModel : PageModel
	{
		Model.Contact_From_Filled.C_F_FilledConnection CFFC = new Model.Contact_From_Filled.C_F_FilledConnection();

		Model.Contact_Form.Contact_FormProperties CFP = new Model.Contact_Form.Contact_FormProperties();

		Controller.Contact_FormController CFConn = new Controller.Contact_FormController();

		internal string _firstname;
		internal string _lastname;
		internal int _clientNumber;
		internal int _result;

		/// <summary>
		/// This is used to show different divs in my cshtml
		/// </summary>
		[BindProperty]
		public bool ShowMessage { get; set; }

		/// <summary>
		/// This is what i get from the frontend to check if user is a client or not
		/// </summary>
		[BindProperty]
		public string FirstName { get; set; }

		/// <summary>
		/// This is what i get from the frontend to check if user is a client or not
		/// </summary>
		[BindProperty]
		public int ClientNumber { get; set; }

		/// <summary>
		/// This is for a new user
		/// </summary>
		[BindProperty]
		public string NewUserFirstName { get; set; }

		/// <summary>
		/// This is for a new user
		/// </summary>
		[BindProperty]
		public string NewUserLastName { get; set; }

		/// <summary>
		/// This is for find out where to reply
		/// </summary>
		[BindProperty]
		public string userEmail { get; set; }

		/// <summary>
		/// This is for the message i get from the user
		/// </summary>
		[BindProperty]
		public string userMessage { get; set; }

		public void OnGet()
		{

		}

		public void OnPost()
		{
			Console.WriteLine();

			CFP = CFConn.InputData(FirstName, ClientNumber);

			if (CFP == null)
			{
				Console.WriteLine("Fucking not working");
			}
			else
			{
				Console.WriteLine(CFP.FirstName + " / " + CFP.LastName + " / " + CFP.ClientNumber + " / " + CFP.Result);

				_firstname = CFP.FirstName;
				_lastname = CFP.LastName;
				_clientNumber = CFP.ClientNumber;
				_result = CFP.Result;

				TempData["FirstName"] = _firstname;
				TempData["LastName"] = _lastname;
			}

			ShowMessage = true;
		}

		public IActionResult OnPostCancel()
		{
			ShowMessage = false;
			Console.WriteLine("Back button method is working");
			return RedirectToPage(); // Redirects to the same page with a GET request
		}


		public IActionResult OnPostIsClient()
		{
			// Retrieve values from TempData
			var firstName = TempData["FirstName"] as string;
			var lastName = TempData["LastName"] as string;
			// ... similarly for other variables ...

			Console.WriteLine("Everything is working on OnPostIsClient");
			Console.WriteLine("_firstname " + firstName + " _lastname " + lastName + " userEmail " + userEmail + " userMessage " + userMessage);

			CFFC.SubmitContactForm(firstName, lastName, true, userMessage, userEmail);

			return RedirectToPage();
		}


		public IActionResult OnPostIsNotClient()
		{
			Console.WriteLine("Everything is working on OnPostIsNotClient");

            Console.WriteLine("NewUserFirstName " + NewUserFirstName + " NewUserLastName " + NewUserLastName + " userEmail " + userEmail + " userMessage " + userMessage );

			CFFC.SubmitContactForm(NewUserFirstName, NewUserLastName, false, userMessage, userEmail);

			return RedirectToPage();
		}
	}
}
