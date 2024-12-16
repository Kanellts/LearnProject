using Npgsql;
using System;
using System.Linq;

namespace Learn.Console;
class Program
{
    static void Main(string[] args)
    {
        System.Console.WriteLine("Hello World");
        
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