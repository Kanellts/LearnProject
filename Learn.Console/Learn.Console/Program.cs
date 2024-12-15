using Npgsql;
using System;
using System.Linq;

// namespace Learn.Console;
class Program
{
    static void Main()
    {
        string connectionString = "Host=localhost;Port=user_port;Database=database_name;User Id=username;Password=password;";

        using NpgsqlConnection connection = new NpgsqlConnection(connectionString);
        connection.Open();

        using (var db = new StudentsContext())
        {
            Console.Write("Enter credentials for a new student:");
            var id = Convert.ToInt32(Console.ReadLine());
            var name = Console.ReadLine();
            var age = Convert.ToInt32(Console.ReadLine());

            var student = new Student(id,name,age);
        }
        // using NpgsqlCommand cmd = new NpgsqlCommand("SELECT * FROM students", connection);

        // using NpgsqlDataReader reader = cmd.ExecuteReader();

        // while (reader.Read())
        // {
        //     Console.WriteLine(reader["column_name"]);
        //     // Use the fetched results
        // }
    }
}