using System.Data.SqlClient;
using System.Data;

namespace NordicNest.Model.Transaction
{
	public class TransactionConnection
	{
		public List<TransactionVariable> GetTransactionInfo()
		{
			var transinfos = new List<TransactionVariable>();

			var config = new ConfigurationBuilder()
						.AddJsonFile("appsettings.json")
						.Build();

			var connectionString = config.GetConnectionString("ReadOnlyLogin");

			try
			{
				using (SqlConnection connection = new SqlConnection(connectionString))
				{
					connection.Open();

					using (SqlCommand command = new SqlCommand("GetTransactionInfoForClient", connection))
					{
						command.CommandType = CommandType.StoredProcedure;
						command.Parameters.AddWithValue("@ClientID", Model.BasicProperties.ClientID);

						using (SqlDataReader reader = command.ExecuteReader())
						{
							if (reader.HasRows)
							{
								while (reader.Read())
								{
									var transinfo = new TransactionVariable
									{
										TransactionTo = reader["TransactionTo"].ToString(),
										TransactionFrom = reader["TransactionFrom"].ToString(),
										Amount = Convert.ToDecimal(reader["Amount"]),
										Time = reader["Time"].ToString(),
										Category = reader["category"].ToString()
									};

									transinfos.Add(transinfo);
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

			return transinfos;
		}
	}
}
