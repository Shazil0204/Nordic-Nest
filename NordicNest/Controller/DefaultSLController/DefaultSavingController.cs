using NordicNest.Model.SavingLoanDatabase;
namespace NordicNest.Controller.DefaultSLController
{
	public class DefaultSavingController
	{
		DefaultSaving DS = new DefaultSaving();
		DefaultSLVariable DSLV = new DefaultSLVariable();
		internal (string, decimal, decimal) GetData()
		{
            DS.DefaultSavingAccount(DSLV);
			return (DSLV.SaveName, DSLV.SavingCurrentAmount, DSLV.SavingTotalAmount);
		}
	}
}
