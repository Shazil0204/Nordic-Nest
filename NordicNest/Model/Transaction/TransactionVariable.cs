namespace NordicNest.Model.Transaction
{
	public class TransactionVariable
	{
		#region Variables
		private string _transactionTo;
		private string _transactionFrom;
		private decimal _amount;
		private string _time;
		private string _category;
		#endregion
		#region Properties
		internal string TransactionTo { get { return _transactionTo; } set { _transactionTo = value; } }
		internal string TransactionFrom { get { return _transactionFrom; } set { _transactionFrom = value; } }
		internal decimal Amount { get { return _amount; } set { _amount = value; } }
		internal string Time { get { return _time; } set { _time = value; } }
		internal string Category { get { return _category; } set { _category = value; } }
		#endregion
	}
}
