using System.Data.SqlClient;
using System.Data;

namespace NordicNest.Model.DbUserEntry
{
	public class CheckUserName
	{
		public int CheckUserNameIfExist(string username)
		{
			var config = new ConfigurationBuilder()
				   .AddJsonFile("appsettings.json")
				   .Build();

			var connectionString = config.GetConnectionString("Check_Email");

			int result = 0;

			using (SqlConnection conn = new SqlConnection(connectionString))
			{
				SqlCommand cmd = new SqlCommand("CheckEmailExist", conn);
				cmd.CommandType = CommandType.StoredProcedure;

				// Set up the parameters
				cmd.Parameters.Add("@Email", SqlDbType.VarChar, 30).Value = username;

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
