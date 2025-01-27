using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;
using CommandLine;
using Learn.Core.DataAccess;
using Learn.Core.DataAccess.Models;
using Learn.Core.Repository;
using Learn.Core.Logger;
using Learn.Service;
using System.Reflection.Metadata;
using System.Diagnostics.Metrics;
using System.Security.Cryptography;

namespace Learn.Console;
class Program
{
    static async Task Main(string[] args)
    {
        var builder = Host.CreateDefaultBuilder(args)
            .ConfigureAppConfiguration((context, config) =>
            {
                config.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
            })
            .ConfigureServices((context, services) =>
            {
                services.AddDbContext<StudentsContext>(ServiceLifetime.Scoped);
                services.AddSingleton<IStudentsRepository, StudentRepository>();
                services.AddSingleton<IStudentsService, StudentsService>();
                services.AddSingleton<IStudentLogger, StudentLogger>();
                if(args == null)
                {
                    services.AddHostedService<Worker>();
                }
                else {
                    services.AddSingleton(args);
                    services.AddHostedService<ExecuteStudentServicesAsync>();
                }
            });

        // using (var db = new StudentsContext())
        // {
        // {
        //     if(!string.IsNullOrWhiteSpace(options.outputPath)) {
        //         var filePath = Path.Combine(options.outputPath, "test.txt");
        //         await File.WriteAllTextAsync(filePath, "This is just a test");
        //         System.Console.WriteLine($"The file {filePath} has been successfully written.");
        //     }
        //     else if(!string.IsNullOrWhiteSpace(options.newStudentInfo)) {
        //         System.Console.WriteLine("Student create was given with values: " + options.newStudentInfo);
        //     }
        //     else {
        //         System.Console.WriteLine("No argument parsed");
        //     }

        // });

        IHost host = builder.Build();
        await host.RunAsync();

    }

}

public sealed class ExecuteStudentServicesAsync(string[] args, IStudentLogger studentLogger, IStudentsService studentsService, StudentsContext studentsContext) : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        System.Console.WriteLine("Hello World");
        await studentsContext.Database.MigrateAsync();

        await Parser.Default.ParseArguments<StudentOperations>(args)
            .WithParsedAsync(async options => {
            // {
            //     switch (options.dbOperation.ToLower())
            //     {
            //         case "create":
            //             await studentsService.CreateStudentAsync((new Student { Id = 0, Name = options.newStudentName, Age = options.newStudentAge }));
            //             break;
            //         case "update":
            //             await studentsService.UpdateStudentAsync((new Student { Id = options.newStudentId, Name = options.newStudentName, Age = options.newStudentAge }));
            //             break;
            //         case "get":
            //             studentLogger.LogStudents(await studentsService.GetAllStudentsAsync());
            //             break;
            //         default:
            //             break;
            //     }
            // });
             if(options.addStudent){
                    await studentsService.CreateStudentAsync((new Student { Id = 0, Name = options.newStudentName, Age = options.newStudentAge }));
                }
                else if(options.updateStudent){
                    await studentsService.UpdateStudentAsync((new Student { Id = options.newStudentId, Name = options.newStudentName, Age = options.newStudentAge }));
                }
                else if(options.getAllStudents) {
                    studentLogger.LogStudents(await studentsService.GetAllStudentsAsync());
                }
                else {
                    System.Console.WriteLine($"Wrong option {options} provided");
                }
            });
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
        await studentsRepository.CreateAsync(new Student { Id = 4, Name = "Kanellos Tsitouras", Age = 24 });

        //Check if we succeded
        System.Console.WriteLine("\nAfter the Create");
        studentLogger.LogStudents(await studentsRepository.GetAllAsync());

        //Let's update the new entry:
        await studentsRepository.UpdateAsync(new Student { Id = 4, Name = "Kanellos Tsitouras", Age = 25 });

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


/*
 {
                if(options.addStudent){
                    await studentsService.CreateStudentAsync((new Student { Id = 0, Name = options.newStudentName, Age = options.newStudentAge }));
                }
                else if(options.updateStudent){
                    await studentsService.UpdateStudentAsync((new Student { Id = options.newStudentId, Name = options.newStudentName, Age = options.newStudentAge }));
                }
                else if(options.getAllStudents) {
                    studentLogger.LogStudents(await studentsService.GetAllStudentsAsync());
                }
                else {
                    System.Console.WriteLine($"Wrong option {options} provided");
                }
            });
*/