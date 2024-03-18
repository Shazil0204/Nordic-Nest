using System.Data;
using System.Data.SqlClient;

namespace NordicNest.Model.ForgotPassword
{
    public class CheckUserEmailConnection
    {
        internal void CUEConnection()
        {
            string username = "";
            string email = "abcd";

            int result = -9991;

            // Connection string from appsettings.json
            var config = new ConfigurationBuilder()
                        .AddJsonFile("appsettings.json")
                        .Build();

            var connectionString = config.GetConnectionString("TReadOnly");

            using (var connection = new SqlConnection(connectionString))
            {
                using (var command = new SqlCommand("CHECKUSERNAMEANDEMAIL", connection))
                {
                    // Specify that the command is a stored procedure
                    command.CommandType = CommandType.StoredProcedure;

                    // Add parameters
                    command.Parameters.AddWithValue("@Username", username);
                    command.Parameters.AddWithValue("@Email", email);

                    // Add output parameter for @Result
                    SqlParameter resultParam = command.Parameters.Add("@Result", SqlDbType.Int);
                    resultParam.Direction = ParameterDirection.Output;

                    // Open the connection
                    connection.Open();

                    try
                    {
                        // Execute the stored procedure
                        command.ExecuteNonQuery();

                        // Retrieve the value of the output parameter
                        result = (int)resultParam.Value;

                        // Assign the result to Model.BasicProperties.ForgotPasswordcurrentForm
                        Console.WriteLine("before " + BasicProperties.ForgotPasswordcurrentForm);
                        BasicProperties.ForgotPasswordcurrentForm = result;
                        Console.WriteLine("After " + BasicProperties.ForgotPasswordcurrentForm);
                    }
                    catch (Exception ex)
                    {
                        // Assign the result to Model.BasicProperties.ForgotPasswordcurrentForm
                        BasicProperties.ForgotPasswordcurrentForm = result;
                        Console.WriteLine(ex.Message);
                    }
                    finally
                    {
                        // Ensure connection is closed
                        if (connection != null && connection.State != ConnectionState.Closed)
                        {
                            connection.Close();
                        }
                    }
                }
            }
        }
    }
}
