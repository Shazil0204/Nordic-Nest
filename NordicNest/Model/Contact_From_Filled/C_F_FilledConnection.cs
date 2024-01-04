using System.Data.SqlClient;
using System.Data;

namespace NordicNest.Model.Contact_From_Filled
{
	public class C_F_FilledConnection
	{
		public int SubmitContactForm(string firstName, string lastName, bool isExistingClient, string messageContent, string userEmail)
		{
			var config = new ConfigurationBuilder()
				   .AddJsonFile("appsettings.json")
				   .Build();

			var connectionString = config.GetConnectionString("Contact_Form_Filled");

			int result = 0;

			using (SqlConnection conn = new SqlConnection(connectionString))
			{
				SqlCommand cmd = new SqlCommand("USERCONTACTFORM", conn);
				cmd.CommandType = CommandType.StoredProcedure;

				// Set up the parameters
				cmd.Parameters.Add("@FirstName", SqlDbType.VarChar, 30).Value = firstName;
				cmd.Parameters.Add("@LastName", SqlDbType.VarChar, 30).Value = lastName;
				cmd.Parameters.Add("@IsExistingClient", SqlDbType.Bit).Value = isExistingClient;
				cmd.Parameters.Add("@MessageContent", SqlDbType.VarChar, 255).Value = messageContent;
				cmd.Parameters.Add("@UserEmail", SqlDbType.VarChar, 75).Value = userEmail;

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
