using System;
using System.Data;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using NordicNest.Model.NavBar;

namespace NordicNest.Model.Contact_Form
{
    public class Contact_FormConnection
    {
        public Contact_FormProperties GetUserExistence(string firstName, int clientNumber)
        {
            Contact_FormProperties properties = new Contact_FormProperties();

            // Connection string
            var config = new ConfigurationBuilder()
                   .AddJsonFile("appsettings.json")
                   .Build();

            var connectionString = config.GetConnectionString("Contact_form_check");

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand("CHECKIFCLIENTEXISTS", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                Console.WriteLine(BasicProperties.CurrentPage);

                // Add the page name as a parameter
                command.Parameters.AddWithValue("@FirstName", firstName);
                command.Parameters.AddWithValue("@ClientNumber", clientNumber);

                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.Read())
                    {
                        properties.FirstName = reader["FirstName"].ToString();
                        properties.LastName = reader["LastName"].ToString();
                        properties.ClientNumber = (int)reader["ClientNumber"];
                        properties.Result = (int)reader["Result"];
                        Console.WriteLine("From database " + (int)reader["Result"]);
                        Console.WriteLine(properties.Result);
                    }
                    else
                    {
                        properties.Result = -2; // Indicating no data found
                        return properties;
                    }

                    reader.Close();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    properties.Result = -99; // Indicating an error
                    return null;
                }

                return properties;
            }

        }
    }
}
