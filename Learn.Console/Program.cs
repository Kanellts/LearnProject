using Learn.Core.DataAccess;
using Learn.Core.DataAccess.Models;
using Learn.Core.Repository;
using Learn.Core.Logger;
using Microsoft.EntityFrameworkCore;

namespace Learn.Console;
class Program
{
    static async Task Main(string[] args)
    {
        System.Console.WriteLine("Hello World");
        using (var db = new StudentsContext())
        {
            await db.Database.MigrateAsync();
        }

        using (StudentRepository std = new StudentRepository())
        {
            System.Console.WriteLine("\nBefore the add");
            
            StudentLogger stdlog = new StudentLogger();
            stdlog.LogStudents(await std.GetAllAsync());

            //Let's add a new student to the database:
            await std.CreateAsync(new Student{ Id = 4, Name = "Kanellos Tsitouras", Age = 24});

            //Check if we succeded
            System.Console.WriteLine("\nAfter the Create");
            stdlog.LogStudents(await std.GetAllAsync());

            //Let's update the new entry:
            await std.UpdateAsync(new Student{ Id = 4, Name = "Kanellos Tsitouras", Age = 25});

            //Check if we succeded
            System.Console.WriteLine("\nAfter the Update");
            stdlog.LogStudents(await std.GetAllAsync());

            //Let's delete the new student from the database:
            await std.DeleteAsync(4);
            
            //Check if we succeded
            System.Console.WriteLine("\nAfter the Delete");
            stdlog.LogStudents(await std.GetAllAsync());

        }

        System.Console.WriteLine("Press any key to continue");
        System.Console.ReadKey();
    }


}