using Learn.Core.DataAccess;
using Learn.Core.DataAccess.Models;
using Learn.Core.Repository;
using Learn.Core.Logger;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Learn.Console;
class Program
{
    static async Task Main(string[] args)
    {
        var builder = Host.CreateApplicationBuilder(args);

        builder.Services.AddDbContext<StudentsContext>();
        builder.Services.AddSingleton<IStudentsRepository, StudentRepository>();
        builder.Services.AddSingleton<IStudentLogger, StudentLogger>();
        builder.Services.AddHostedService<Worker>();

        using IHost host = builder.Build();
        
        await host.RunAsync();
    }
}

public sealed class Worker(IStudentLogger studentLogger, IStudentsRepository studentsRepository, StudentsContext studentsContext) : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {   
        System.Console.WriteLine("Hello World");
        // using (var db = new StudentsContext())
        // {
        await studentsContext.Database.MigrateAsync();
        // }
        
        System.Console.WriteLine("\nBefore the add");
            
        studentLogger.LogStudents(await studentsRepository.GetAllAsync());

        //Let's add a new student to the database:
        await studentsRepository.CreateAsync(new Student{ Id = 4, Name = "Kanellos Tsitouras", Age = 24});

        //Check if we succeded
        System.Console.WriteLine("\nAfter the Create");
        studentLogger.LogStudents(await studentsRepository.GetAllAsync());

        //Let's update the new entry:
        await studentsRepository.UpdateAsync(new Student{ Id = 4, Name = "Kanellos Tsitouras", Age = 25});

        //Check if we succeded
        System.Console.WriteLine("\nAfter the Update");
        studentLogger.LogStudents(await studentsRepository.GetAllAsync());

        //Let's delete the new student from the database:
        await studentsRepository.DeleteAsync(4);
            
        //Check if we succeded
        System.Console.WriteLine("\nAfter the Delete");
        studentLogger.LogStudents(await studentsRepository.GetAllAsync());

        // System.Console.WriteLine("Press any key to continue");
        // System.Console.ReadKey();
        }
}