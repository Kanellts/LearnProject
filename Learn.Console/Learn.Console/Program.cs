using Learn.Console.DataAccess;
using Learn.Console.DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using Npgsql;
using System;
using System.Linq;

namespace Learn.Console;
class Program
{
    static void Main(string[] args)
    {
        System.Console.WriteLine("Hello World");
        using (var db = new StudentsContext())
        {
            db.Database.Migrate();
        }
        readStudent();

        System.Console.WriteLine("Press any key to continue");
        System.Console.ReadKey();
    }

    static void readStudent()
    {
        using (var db = new StudentsContext()) 
        {
            IQueryable<Student> studQuery = 
                from stud in db.Students 
                select stud;

            foreach(Student s in studQuery)
            {
                System.Console.WriteLine("{0} {1} {2}",s.Id,s.Name,s.Age);
            }
        }
        return;
    }
}