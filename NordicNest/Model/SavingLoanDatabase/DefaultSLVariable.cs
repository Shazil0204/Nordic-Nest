namespace NordicNest.Model.SavingLoanDatabase
{
	public class DefaultSLVariable
	{
		#region variables
		private string _saveName;
		private string _loanName;
		private decimal _saveTotalAmount;
		private decimal _loanTotalAmount;
		private decimal _saveCurrentAmount;
		private decimal _loancurrentAmount;
		#endregion
		
		#region Properties
		internal string SaveName { get { return _saveName; } set { _saveName = value; } }
		internal string LoanName { get { return _loanName; } set { _loanName = value; } }
		internal decimal SavingTotalAmount { get { return _saveTotalAmount; } set { _saveTotalAmount = value; } }
		internal decimal LoanTotalAmount { get { return _loanTotalAmount; } set { _loanTotalAmount = value; } }
		internal decimal SavingCurrentAmount { get { return _saveCurrentAmount; } set { _saveCurrentAmount = value; } }
		internal decimal LoanCurrentAmount { get { return _loancurrentAmount; } set { _loancurrentAmount = value; } }
		#endregion
	}
}
