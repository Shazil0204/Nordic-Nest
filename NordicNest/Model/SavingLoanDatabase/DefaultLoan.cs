using System.Data.SqlClient;
using System.Data;

namespace NordicNest.Model.SavingLoanDatabase
{
	public class DefaultLoan
	{
		public void DefaultLoanAccount(DefaultSLVariable DSLV)
		{
			try
			{
				var config = new ConfigurationBuilder()
						.AddJsonFile("appsettings.json")
						.Build();

				var connectionString = config.GetConnectionString("Default_Loan");

				using (SqlConnection connection = new SqlConnection(connectionString))
				{
					connection.Open();

					// Assuming GetDefaultSavingsInfo is the name of your stored procedure
					using (SqlCommand command = new SqlCommand("GetDefaultLoanInfo", connection))
					{
						command.CommandType = CommandType.StoredProcedure;
						command.Parameters.AddWithValue("@ClientID", BasicProperties.ClientID);

						using (SqlDataReader reader = command.ExecuteReader())
						{
							if (reader.HasRows)
							{
								while (reader.Read())
								{
									// Assuming column names in the result set match properties in DefaultSLVariable
									DSLV.SaveName = reader["Name"].ToString();
									DSLV.SavingTotalAmount = Convert.ToDecimal(reader["TotalAmount"]);
									DSLV.SavingCurrentAmount = Convert.ToDecimal(reader["AmountBalance"]);
								}
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
