using System;
using System.Data;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace NordicNest.Model.DbUserEntry
{
	public class CheckEmail
	{
		/// <summary>
		/// This will check if an email already exists or not
		/// </summary>
		/// <param name="email"></param>
		/// <returns></returns>
		public int CheckEmailIfExist(string email)
		{
			Console.WriteLine(email);

			var config = new ConfigurationBuilder()
				.AddJsonFile("appsettings.json")
				.Build();

			var connectionString = config.GetConnectionString("Check_Email");

			int result = 0;

			using (SqlConnection conn = new SqlConnection(connectionString))
			{
				SqlCommand cmd = new SqlCommand("CheckEmailExist", conn);
				cmd.CommandType = CommandType.StoredProcedure;

				// Set up the @Email parameter
				cmd.Parameters.Add("@Email", SqlDbType.VarChar, 255).Value = email;

				try
				{
					conn.Open();
					// Execute the stored procedure
					result = (int)cmd.ExecuteScalar();
				}
				catch (SqlException ex)
				{
					// Handle exceptions
					Console.WriteLine("SQL Error: " + ex.Message);
					result = -99;
				}
				finally
				{
					conn.Close();
				}
			}

			return result;
		}

	}
}
