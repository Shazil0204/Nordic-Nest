using System.Xml.Linq;

namespace NordicNest.Model
{
    public class BasicProperties
    {
        #region Variables
        internal static string CurrentPage = "Default Value Null"; 

        internal bool EmailExist = false;
        internal static string Email {  get; set; } 
        internal static bool IsNew { get; set; }
        internal static bool IsLogged { get; set; } = false;
        internal static int ClientID { get; set; } = 2;
        internal static int DefaultSavingAccount { get; set; } = 1;
        internal static int DefaultLoanAccount { get; set; } = 1;
		internal static int UserSavingAccount { get; set; } = 1;
		internal static int UserLoanAccount { get; set; } = 1;
		#endregion
	}
}
