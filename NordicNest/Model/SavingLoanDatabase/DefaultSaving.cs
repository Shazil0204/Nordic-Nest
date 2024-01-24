﻿using System.Data.SqlClient;
using System.Data;

namespace NordicNest.Model.SavingLoanDatabase
{
	public class DefaultSaving
	{
		public void DefaultSavingAccount(DefaultSLVariable DSLV)
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

					// Assuming GetDefaultSavingsInfo is the name of your stored procedure
					using (SqlCommand command = new SqlCommand("GetDefaultSavingsInfo", connection))
					{
						command.CommandType = CommandType.StoredProcedure;
						command.Parameters.AddWithValue("@ClientID", Model.BasicProperties.ClientID);

						using (SqlDataReader reader = command.ExecuteReader())
						{
							if (reader.HasRows)
							{
								while (reader.Read())
								{
									// Assuming column names in the result set match properties in DefaultSLVariable
									DSLV.SaveName= reader["Name"].ToString();
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
