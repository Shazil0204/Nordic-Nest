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

        #endregion
    }
}
