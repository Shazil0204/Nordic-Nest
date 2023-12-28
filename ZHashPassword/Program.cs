// dotnet add package BCrypt.Net-Next

using System;
using BCrypt.Net;

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

        // Verifying the password
        Console.Write("Enter the password again to verify: ");
        string passwordToVerify = Console.ReadLine();
        bool isVerified = VerifyPassword(passwordToVerify, hashedPassword);

        if (isVerified)
        {
            Console.WriteLine("Password verified successfully!");
        }
        else
        {
            Console.WriteLine("Password verification failed!");
        }
    }

    private static string HashPassword(string password)
    {
        return BCrypt.Net.BCrypt.HashPassword(password);
    }

    private static bool VerifyPassword(string password, string hash)
    {
        return BCrypt.Net.BCrypt.Verify(password, hash);
    }
}
