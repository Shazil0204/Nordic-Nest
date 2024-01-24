namespace NordicNest.Model.Subscriptions
{
	public class SubVariables
	{
		#region Variables
		private string _name;
		private decimal _amount;
		private int _daysBeforeRenewal;
		#endregion

		#region Variables
		internal string Name { get { return _name; } set { _name = value; } }
		internal decimal Amount { get { return _amount; } set { _amount = value; } }
		internal int DaysBeforeRenewal { get {  return _daysBeforeRenewal; } set { _daysBeforeRenewal = value; } }
		#endregion
	}
}
