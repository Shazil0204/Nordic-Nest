using System.Data;
using System.Data.SqlClient;

namespace NordicNest.Model.Login
{
	public class UserLogin
	{
		public (string Password, int ResultCode) CheckUser(string userName)
		{
			var config = new ConfigurationBuilder()
						.AddJsonFile("appsettings.json")
						.Build();

			var connectionString = config.GetConnectionString("Create_Account"); 

			string password = null;
			int resultCode = 0;

			using (var connection = new SqlConnection(connectionString))
			{
				using (var command = new SqlCommand("CHECKUSER", connection))
				{
					command.CommandType = CommandType.StoredProcedure;

					// Pass the username exactly as it is entered
					command.Parameters.AddWithValue("@UserName", userName);
					command.Parameters.Add("@Password", SqlDbType.VarChar, 255).Direction = ParameterDirection.Output;
					command.Parameters.Add("@Result", SqlDbType.Int).Direction = ParameterDirection.Output;
					command.Parameters.Add("@ClientID", SqlDbType.Int).Direction = ParameterDirection.Output; // Add output parameter for ClientID

					connection.Open();

					try
					{
						command.ExecuteNonQuery();

						// Retrieve the output parameters
						password = command.Parameters["@Password"].Value as string;
						resultCode = (int)command.Parameters["@Result"].Value;

						// Check if ClientID parameter is not DBNull before assigning
						if (command.Parameters["@ClientID"].Value != DBNull.Value)
						{
							BasicProperties.ClientID = (int)command.Parameters["@ClientID"].Value;
						}
					}
					catch (Exception ex)
					{
						// Handle any exceptions here
						Console.WriteLine("An error occurred: " + ex.Message);
						resultCode = -99; // Or handle the error in another way
					}
				}
			}

			Console.WriteLine(BasicProperties.ClientID);

            // Now, ClientID is set within the CheckUser method

            return (password, resultCode);
		}
	}
}

