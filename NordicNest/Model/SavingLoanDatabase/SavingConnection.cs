using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace NordicNest.Model.SavingLoanDatabase
{
	public class SavingConnection
	{
		public List<SLVariable> GetAllSavingData()
		{
			var Savings = new List<SLVariable>();

			// Connection string
			var config = new ConfigurationBuilder()
				   .AddJsonFile("appsettings.json")
				   .Build();

			var connectionString = config.GetConnectionString("Saving_Account_Data");

			using (SqlConnection connection = new SqlConnection(connectionString))
			{
				SqlCommand command = new SqlCommand("GetSavingsInfo", connection)
				{
					CommandType = CommandType.StoredProcedure
				};

				// Add the clientID as a parameter
				command.Parameters.AddWithValue("@ClientID", BasicProperties.ClientID);

				Console.WriteLine(BasicProperties.ClientID);

				try
				{
					connection.Open();
					SqlDataReader reader = command.ExecuteReader();

					if (reader.HasRows)
					{
						while (reader.Read())
						{
							var saving = new SLVariable
							{
								ID = Convert.ToInt32(reader["SavingID"]),
								Name = reader["Name"].ToString(),
								TotalAmount = Convert.ToDecimal(reader["TotalAmount"]),
								AmountBalance = Convert.ToDecimal(reader["AmountBalance"])
							};

							Savings.Add(saving);
						}
					}
					else
					{
						// Handle case where no savings record is found for the client
					}

					reader.Close();
				}
				catch (Exception ex)
				{
					// Handle the exception (log, throw, etc.)
					Console.WriteLine(ex.Message);
				}
				finally
				{
					// Ensure the connection is closed
					if (connection.State == ConnectionState.Open)
					{
						connection.Close();
					}
				}
			}

			return Savings;
		}
	}
}
