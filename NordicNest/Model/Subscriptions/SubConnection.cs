using System.Data.SqlClient;
using System.Data;

namespace NordicNest.Model.Subscriptions
{
	public class SubConnection
	{

		public List<SubVariables> GetSubscriptionInfo()
		{
			var subinfos = new List<SubVariables>();

			var config = new ConfigurationBuilder()
						.AddJsonFile("appsettings.json")
						.Build();

			var connectionString = config.GetConnectionString("ReadOnlyLogin");

			int resultCode = 0;  // Initialize the result code variable

			try
			{
				using (SqlConnection connection = new SqlConnection(connectionString))
				{
					connection.Open();

					using (SqlCommand command = new SqlCommand("GetSubscriptionInfo", connection))
					{
						command.CommandType = CommandType.StoredProcedure;
						command.Parameters.AddWithValue("@ClientID", Model.BasicProperties.ClientID);

						// Add output parameter for result code
						SqlParameter resultCodeParam = new SqlParameter("@ResultCode", SqlDbType.Int);
						resultCodeParam.Direction = ParameterDirection.Output;
						command.Parameters.Add(resultCodeParam);

						using (SqlDataReader reader = command.ExecuteReader())
						{
							if (reader.HasRows)
							{
								while (reader.Read())
								{
									var subinfo = new SubVariables
									{
										Name = reader["SubscriptionName"].ToString(),
										Amount = Convert.ToDecimal(reader["Amount"]),
										DaysBeforeRenewal = Convert.ToInt32(reader["DaysBeforeRenewal"])
									};

									subinfos.Add(subinfo);
								}
							}
						}

						// Retrieve the result code after executing the stored procedure
						resultCode = (int)command.Parameters["@ResultCode"].Value;
						Console.WriteLine(resultCode);
					}
				}

				// Check the result code and handle it as needed
				if (resultCode == 1)
				{
					Console.WriteLine("Subscription information retrieved successfully.");
				}
				else
				{
					Console.WriteLine($"An error occurred while fetching subscription information. Result Code: {resultCode}");
					// Handle the error based on the result code
				}
			}
			catch (Exception ex)
			{
				Console.WriteLine($"An error occurred: {ex.Message}");
				// Handle the error as needed
			}

			return subinfos;
		}
	}
}
