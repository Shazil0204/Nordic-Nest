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

		[BindProperty]
		public bool ShowMessage { get; set; }

		[BindProperty]
		public string FirstName { get; set; }

		[BindProperty]
		public int ClientNumber { get; set; }

		[BindProperty]
		public string NewUserFirstName { get; set; }

		[BindProperty]
		public string NewUserLastName { get; set; }

		[BindProperty]
		public string userMessage { get; set; }

		[BindProperty]
		public string userEmail { get; set; }

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
            Console.WriteLine("Everything is working on OnPostIsClient");

			return RedirectToPage();
		}

		public IActionResult OnPostIsNotClient()
		{
			Console.WriteLine("Everything is working on OnPostIsNotClient");

			return RedirectToPage();
		}
	}
}
