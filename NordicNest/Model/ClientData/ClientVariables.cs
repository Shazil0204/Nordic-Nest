namespace NordicNest.Model.ClientData
{
	public class ClientVariables
	{
		#region Variables
		private string _userName = null;
		private int _totalMonthlyAmount = 0;
		private int _usableAmount = 0;
		private int _userReserved = 0;
		private int _systemReserved = 0;
		#endregion
		
		#region Properties
		internal string UserName { get { return _userName; } set {  _userName = value; } }
		internal int TotalMonthlyAmount { get {  return _totalMonthlyAmount; } set { _totalMonthlyAmount = value; } }
		internal int UsableAmount { get { return _usableAmount; } set { _usableAmount = value; } }
		internal int UserReserved { get { return _userReserved; } set { _userReserved = value; } }
		internal int SystemReserved { get { return _systemReserved;} set { _systemReserved = value; } }
		#endregion
	}
}
