namespace NordicNest.Model.SavingLoanDatabase
{
	public class testing
	{

		public void testingwl()
		{
			SavingConnection savingConnection = new SavingConnection();
			List<SLVariable> sLVariables = savingConnection.GetSavingData();

			foreach (SLVariable v in sLVariables)
			{
				Console.WriteLine($"ClientID: {v.ID}, SavingName: {v.Name}, TotalAmount: {v.TotalAmount}, CurrentBalance: {v.AmountBalance}");
            }
		}
	}
}
