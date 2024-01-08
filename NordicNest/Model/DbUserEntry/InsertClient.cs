using System.Data.SqlClient;
using System.Data;

namespace NordicNest.Model.DbUserEntry
{
    public class InsertClient
    {
        internal int AddNewClient(string firstName, string lastName, string email, string userName, string password, bool gender, int age)
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

                cmd.Parameters.AddWithValue("@FirstName", firstName);
                cmd.Parameters.AddWithValue("@LastName", lastName);
                cmd.Parameters.AddWithValue("@Email", email);
                cmd.Parameters.AddWithValue("@UserName", userName);
                cmd.Parameters.AddWithValue("@Password", password);
                cmd.Parameters.AddWithValue("@Gender", gender);
                cmd.Parameters.AddWithValue("@Age", age);

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
                return result;
            }
        }
    }
}
