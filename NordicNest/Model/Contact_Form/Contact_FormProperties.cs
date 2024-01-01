namespace NordicNest.Model.Contact_Form
{
    public class Contact_FormProperties
    {
        #region Variables

        private string _firstName;

        private string _lastName;

        private int _clientNumber;

        private int _returnValue;

        #endregion

        #region Properties

        internal string FirstName { get { return _firstName; } set { _firstName = value; } }
        internal string LastName { get { return _lastName; } set { _lastName = value; } }
        internal int ClientNumber { get { return _clientNumber; } set { _clientNumber = value; } }
        internal int Result { get { return _returnValue; } set { _returnValue = value; } }

        #endregion
    }
}
