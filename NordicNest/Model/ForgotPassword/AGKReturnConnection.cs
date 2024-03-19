﻿using System.Data.SqlClient;
using System.Data;

namespace NordicNest.Model.ForgotPassword
{
    public class AGKReturnConnection
    {
        internal void AutoGeneratedKeyReturnConnection(string username)
        {
            int result = -9994;

            // Connection string from appsettings.json
            var config = new ConfigurationBuilder()
                        .AddJsonFile("appsettings.json")
                        .Build();

            var connectionString = config.GetConnectionString("TReadOnly");

            using (var connection = new SqlConnection(connectionString))
            {
                using (var command = new SqlCommand("AGKRETURN", connection))
                {
                    // Specify that the command is a stored procedure
                    command.CommandType = CommandType.StoredProcedure;

                    // Add parameters
                    command.Parameters.AddWithValue("@Username", username);

                    // Add output parameter for @Result
                    SqlParameter resultParam = command.Parameters.Add("@Result", SqlDbType.VarChar, 10); // VARCHAR(10)
                    resultParam.Direction = ParameterDirection.Output;

                    // Open the connection
                    connection.Open();

                    try
                    {
                        // Execute the stored procedure
                        command.ExecuteNonQuery();

                        // Retrieve the value of the output parameter
                        string sresult = resultParam.Value.ToString(); // Treat as string
                        result = Convert.ToInt32(sresult);

                        // Assign the result to Model.BasicProperties.ForgotPasswordcurrentForm
                        BasicProperties.ForgotPasswordcurrentForm = result;
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
