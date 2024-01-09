using System;
using BCrypt.Net;
using System.Data.SqlClient;
using Microsoft.Data.SqlClient;

class Program
{
	static void Main(string[] args)
	{
		Console.WriteLine("Password Hashing in C# Console Application");

		// Getting the user's password
		Console.Write("Enter a password to hash: ");
		string password = Console.ReadLine();

		// Hashing the password
		string hashedPassword = HashPassword(password);
		Console.WriteLine($"Hashed Password: {hashedPassword}");

		// Store the hashed password in the database
		StoreHashedPasswordInDatabase(hashedPassword);
	}

	private static string HashPassword(string password)
	{
		return BCrypt.Net.BCrypt.HashPassword(password);
	}

	private static void StoreHashedPasswordInDatabase(string hashedPassword)
	{
		string connectionString = "Server=localhost;Database=hashpasswordverify;User ID=testing;Password=testing;Integrated Security=True;TrustServerCertificate=true;"; // Replace with your actual connection string

		using (SqlConnection connection = new SqlConnection(connectionString))
		{
			connection.Open();
			string insertQuery = "INSERT INTO hashpassword (password) VALUES (@hashedPassword)";

			using (SqlCommand cmd = new SqlCommand(insertQuery, connection))
			{
				cmd.Parameters.AddWithValue("@hashedPassword", hashedPassword);
				cmd.ExecuteNonQuery();
			}
		}
	}
}

