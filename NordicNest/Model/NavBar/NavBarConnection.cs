using System.Data;
using System.Data.SqlClient;

namespace NordicNest.Model.NavBar
{
    public class NavBarConnection
    {
        // Modified method to accept a page name and return NavBarProperties
        public List<NavBarProperties> GetNavBars()
        {
            var navBars = new List<NavBarProperties>();

            // Connection string
            var config = new ConfigurationBuilder()
                   .AddJsonFile("appsettings.json")
                   .Build();

            var connectionString = config.GetConnectionString("NavBarConnection");

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand("CHECKNAVBAR", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                // Add the page name as a parameter
                command.Parameters.AddWithValue("@PageName", Model.BasicProperties.CurrentPage);

                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            var navBar = new NavBarProperties
                            {
                                Name = reader["Name"].ToString(),
                                URL = reader["URL"].ToString(),
                                IsAuthBtn = (bool)reader["IsAuthBtn"]
                            };

                            navBars.Add(navBar);
                        }
                    }
                    else
                    {
                    }

                    reader.Close();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }
            }

            return navBars;
        }
    }

}
