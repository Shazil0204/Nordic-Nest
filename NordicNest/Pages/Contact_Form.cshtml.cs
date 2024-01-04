using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace NordicNest.Pages
{
    public class Contact_FormModel : PageModel
    {
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

        public void OnGet()
        {

        }

        public void OnPost()
        {
            Console.WriteLine();

            CFP = CFConn.InputData(FirstName, ClientNumber);

            if(CFP == null)
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
            //return RedirectToPage();
        }

        public void OnCancel()
        {
            ShowMessage = false;
        }
    }
}
