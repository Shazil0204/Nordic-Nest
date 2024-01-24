using NordicNest.Model.SavingLoanDatabase;
namespace NordicNest.Controller.DefaultSLController
{
	public class DefaultLoanController
	{
		DefaultLoan DL = new DefaultLoan();
		DefaultSLVariable DSLV = new DefaultSLVariable();
		internal (string, decimal, decimal) GetData()
		{
			DL.DefaultLoanAccount(DSLV);
			return (DSLV.SaveName, DSLV.SavingCurrentAmount, DSLV.SavingTotalAmount);
		}
	}
}
