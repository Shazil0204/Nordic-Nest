using NordicNest.Model.Contact_Form;

namespace NordicNest.Controller
{
    public class Contact_FormController
    {
        Model.Contact_Form.Contact_FormProperties CFP = new Model.Contact_Form.Contact_FormProperties();
        Model.Contact_Form.Contact_FormConnection CFC = new Model.Contact_Form.Contact_FormConnection();
        
        internal Contact_FormProperties InputData(string firstName, int clientNumber)
        {
            CFP = CFC.GetUserExistence(firstName, clientNumber);

            Console.WriteLine(CFP.FirstName + " " + CFP.ClientNumber);

            return CFP;
        }
    }
}
