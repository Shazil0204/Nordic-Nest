namespace NordicNest.Model.NavBar
{
    public class NavBarProperties
    {
        #region Variables

        private string? _name;

        private string? _URL;

        #endregion

        #region Properties

        internal string? Name { get { return _name; } set { _name = value; } }
        internal string? URL { get { return _URL; } set { _URL = value; } }

        #endregion
    }
}
 