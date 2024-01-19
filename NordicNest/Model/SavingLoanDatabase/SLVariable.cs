using System.Security.Cryptography;

namespace NordicNest.Model.SavingLoanDatabase
{
	public class SLVariable
	{
		#region Variables
		private int _id;
		private string _name;
		private decimal _totalAmount;
		private decimal _amountBalance;

		#endregion
		#region Properties
		internal int ID { get { return _id; } set { _id = value; } }
		internal string Name { get { return _name; } set { _name = value; } }
		internal decimal TotalAmount { get { return _totalAmount; } set { _totalAmount = value; } }
		internal decimal AmountBalance { get { return _amountBalance; } set { _amountBalance = value; } }
		#endregion
	}
}
