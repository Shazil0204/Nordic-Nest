using NordicNest.Model.ClientData;
namespace NordicNest.Controller
{
	public class ClientDataController
	{
		ClientDataConnection CDC = new ClientDataConnection();
		ClientVariables CV = new ClientVariables();
		internal (string, int, int, int, int) GetClientData()
		{
			CDC.ClientInfo(CV);
			return (CV.UserName, CV.TotalMonthlyAmount, CV.UsableAmount, CV.UserReserved, CV.SystemReserved);
		}
	}
}
