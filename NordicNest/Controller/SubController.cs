using NordicNest.Model.Subscriptions;
namespace NordicNest.Controller
{
	public class SubController
	{
		public void subControlIO()
		{
			List<SubVariables> SV = new SubConnection().GetSubscriptionInfo();

			int i = 0;

			foreach (SubVariables v in SV)
			{
                Console.WriteLine(i + "\t");
                Console.Write("Name");
                Console.Write(v.Name);
                Console.Write("Amount");
                Console.Write(v.Amount);
                Console.Write("Renewel");
                Console.Write(v.DaysBeforeRenewal);
                Console.WriteLine("\n\n");
				i++;
            }

		}
	}
}
