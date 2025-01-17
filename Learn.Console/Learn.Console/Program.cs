using Learn.Console.DataAccess;
using Learn.Console.DataAccess.Models;
using Learn.Console.Repository;
using Learn.Console.Logger;
using Microsoft.EntityFrameworkCore;
using Npgsql;
using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Learn.Console;
class Program
{
    static async Task Main(string[] args)
    {
        System.Console.WriteLine("Hello World");
        using (var db = new StudentsContext())
        {
            db.Database.Migrate();
        }

        using (StudentRepository std = new StudentRepository())
        {
            List<Student> stds = await std.GetAllAsync();
            System.Console.WriteLine("\nBefore the add");
            
            System.Console.WriteLine("\nCHECK LOGGER #1");
            StudentLogger stdlog = new StudentLogger();
            stdlog.LogStudents(stds);

            //Let's add a new student to the database:
            await std.CreateAsync(new Student{ Id = 4, Name = "Kanellos Tsitouras", Age = 24});

            //Check if we succeded
            System.Console.WriteLine("\nAfter the add");
            System.Console.WriteLine("\nCHECK LOGGER #1");
            stdlog.LogStudents(stds);

            //Let's update the new entry:
            await std.UpdateAsync(new Student{ Id = 4, Name = "Kanellos Tsitouras", Age = 25});

            //Check if we succeded
            System.Console.WriteLine("\nAfter the update");
            System.Console.WriteLine("\nCHECK LOGGER #1");
            stdlog.LogStudents(stds);

            //Let's delete the new student from the database:
            await std.DeleteAsync(4);
            
            //Check if we succeded
            System.Console.WriteLine("\nAfter delete");
            System.Console.WriteLine("\nCHECK LOGGER #1");
            stdlog.LogStudents(stds);

        }

        System.Console.WriteLine("Press any key to continue");
        System.Console.ReadKey();
    }


}