using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace NordicNest.Pages
{
    public class Contact_FormModel : PageModel
    {
        Model.Contact_Form.Contact_FormConnection CFC = new Model.Contact_Form.Contact_FormConnection();
        Model.Contact_Form.Contact_FormProperties CFP = new Model.Contact_Form.Contact_FormProperties();

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

            int a = CFC.GetUserExistence(FirstName, ClientNumber);

            Console.WriteLine(a);

            Console.WriteLine(CFP.FirstName + " / " + CFP.LastName + " / " + CFP.ClientNumber + " / " + CFP.Result);

            ShowMessage = true;
            //return RedirectToPage();
        }
    }
}
