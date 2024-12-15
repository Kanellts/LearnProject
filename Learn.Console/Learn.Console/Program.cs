// See https://aka.ms/new-console-template for more information

using System.ComponentModel;
using System.Reflection;
using System.Reflection.Metadata;
using Npgsql;
using System;

class Program 
{
    static void Main()
    {
        string connectionString = "Host=my_host; Port = port_number; Database = database_name; User Id = username; Password = PasswordPropertyTextAttribute";
        
        using NpgsqlConnection connection = new NpgsqlConnection(connectionString);
        connection.Open();

        // using NpgsqlCommand cmd = new NpgsqlCommand("SELECT * FROM students")
    }
}