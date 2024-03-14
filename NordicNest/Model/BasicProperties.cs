using NordicNest.Pages;
using System.Xml.Linq;

namespace NordicNest.Model
{
    internal class BasicProperties
    {
        #region Variables
        internal static string CurrentPage { get; set; } = null; 
        internal bool EmailExist { get; set; } = false;
        internal static string Email {  get; set; } 
        internal static bool IsNew { get; set; }
        internal static bool IsLogged { get; set; } = false;
        internal static int ClientID { get; set; } = 1;
        internal static int ForgotPasswordcurrentForm { get; set; } = 4;
		#endregion
	}
}
