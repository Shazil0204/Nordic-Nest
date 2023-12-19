using System.Data;
using System.Data.SqlClient;

namespace NordicNest.Model.NavBar
{
    public class NavBarConnection
    {
        public List<NavBarProperties> GetNavBars()
        {
            var navBars = new List<NavBarProperties>();

            // Connection string
            string connectionString = "Server=SHIZ;Database=NordicNestDB;User ID=Navbar;Password=Navbar;Integrated Security=True;TrustServerCertificate=true;";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand("CHECKNAVBAR", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };

                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        var navBar = new NavBarProperties
                        {
                            Name = reader["Name"].ToString(),
                            URL = reader["URL"].ToString()
                        };

                        navBars.Add(navBar);
                    }

                    reader.Close();
                }
                catch (Exception ex)
                {
                    // Handle exceptions (log or throw)
                    Console.WriteLine(ex.Message);
                }
            }

            return navBars;
        }
    }
}
