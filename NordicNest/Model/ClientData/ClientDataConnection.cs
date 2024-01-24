using System;
using System.Data;
using System.Data.SqlClient;
namespace NordicNest.Model.ClientData
{
	public class ClientDataConnection
	{
		public void ClientInfo(ClientVariables CV)
		{
			try
			{
				var config = new ConfigurationBuilder()
					.AddJsonFile("appsettings.json")
					.Build();

				var connectionString = config.GetConnectionString("Default_Saving");

				using (SqlConnection connection = new SqlConnection(connectionString))
				{
					connection.Open();

					using (SqlCommand command = new SqlCommand("GetClientInfo", connection))
					{
						command.CommandType = CommandType.StoredProcedure;
						command.Parameters.AddWithValue("@InputClientID", BasicProperties.ClientID); // Assuming ClientID is a property

						using (SqlDataReader reader = command.ExecuteReader())
						{
							if (reader.HasRows)
							{
								while (reader.Read())
								{
									// Assuming the column names match the variable names
									CV.UserName = reader["UserName"].ToString();
									CV.TotalMonthlyAmount = Convert.ToInt32(reader["TotalMonthlyAmount"]);
									CV.UsableAmount = Convert.ToInt32(reader["UsableAmount"]);
									CV.UserReserved = Convert.ToInt32(reader["UserReserved"]);
									CV.SystemReserved = Convert.ToInt32(reader["SystemReserved"]);
								}
							}
							else
							{
								Console.WriteLine("No rows returned from the database.");
							}
						}
					}
				}
			}
			catch (Exception ex)
			{
				Console.WriteLine($"An error occurred: {ex.Message}");
				// Handle the error as needed
			}
		}
	}
}
