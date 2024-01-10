using System;
using System.Data.SqlClient;
using System.Data;
using Microsoft.Extensions.Configuration;
using NordicNest.Controller;

namespace NordicNest.Model.DbUserEntry
{
	public class InsertClient
	{
		internal int AddNewClient(string firstName, string lastName, string userName, string password, bool gender, int age)
		{
            var config = new ConfigurationBuilder()
				.AddJsonFile("appsettings.json")
				.Build();

			var connectionString = config.GetConnectionString("Create_Account"); // Replace with the correct connection string name

			int result = 0;

			using (SqlConnection conn = new SqlConnection(connectionString))
			{
				SqlCommand cmd = new SqlCommand("AddNewClient", conn);
				cmd.CommandType = CommandType.StoredProcedure;

				cmd.Parameters.AddWithValue("@FirstName", firstName);
				cmd.Parameters.AddWithValue("@LastName", lastName);
				cmd.Parameters.AddWithValue("@Email", BasicProperties.Email);
				cmd.Parameters.AddWithValue("@UserName", userName);
				cmd.Parameters.AddWithValue("@Password", password);
				cmd.Parameters.AddWithValue("@Gender", gender);
				cmd.Parameters.AddWithValue("@Age", age);

				try
				{
					conn.Open();
					object resultObject = cmd.ExecuteScalar();
					if (resultObject != null)
					{
						result = Convert.ToInt32(resultObject);
					}
					else
					{
						// Handle the scenario where result is null
						Console.WriteLine("No result returned from stored procedure.");
						result = -1; // Or any other appropriate value
					}
				}
				catch (SqlException ex)
				{
					Console.WriteLine("SQL Error: " + ex.Message);
					result = -99;
				}
				finally
				{
					conn.Close();
				}

				return result;
			}
		}
	}
}
