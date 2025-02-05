using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;
using CommandLine;
using Learn.Core.DataAccess;
using Learn.Core.DataAccess.Models;
// using Learn.Core.Repository;
using Learn.Core.Logger;
using Learn.Service;


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
                // services.AddSingleton<IStudentsRepository, StudentRepository>();
                services.AddSingleton<IStudentsService, StudentsService>();
                services.AddSingleton<IStudentLogger, StudentLogger>();
                services.AddSingleton(args);
                services.AddHostedService<ExecuteStudentServicesAsync>();
            });

        IHost host = builder.Build();
        await host.RunAsync();

    }

}

public sealed class ExecuteStudentServicesAsync(string[] args, IStudentLogger studentLogger, IStudentsService studentsService, StudentsContext studentsContext) : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        System.Console.WriteLine("Parser initialized\n");
        await studentsContext.Database.MigrateAsync();

        await Parser.Default.ParseArguments<StudentOperations>(args)
            .WithParsedAsync(async options =>
            {
                if (options.addStudent)
                {
                    await studentsService.CreateStudentAsync(new Student { Name = options.newStudentName, Age = options.newStudentAge });
                }
                else if (options.updateStudent)
                {
                    await studentsService.UpdateStudentAsync(new Student { Id = options.newStudentId, Name = options.newStudentName, Age = options.newStudentAge });
                }
                else if (options.getAllStudents)
                {
                    studentLogger.LogStudents(await studentsService.GetAllStudentsAsync());
                }
                else //Default mode option
                {
                    System.Console.Write("\nThis option is not valid.\n---- HELP ----\n" +
                                          "Use --create or --update or --get as follows:\n" +
                                          "Example 1: Add a new entry \"John Doe, 20 years old\" to the database with: " +
                                          "--create --name \"John Doe\" -- age 20\n" +
                                          "Example 2: Update a student in the database with: " +
                                          "--update --id studentId --name \"newStudentName\" --age newStudentAge\n" +
                                          "Example 3: Get all students in the database with: --get\n");
                    /*
                    //Provide maxId to Worker
                    System.Console.Write($"\nThe option {options} is not valid for CRU operation." +
                                            "\nDefault behavior of the app will be initialized, if so," +
                                            "every student with Id greater than or equal to 4, is going to be removed from the database.\n" +
                                            "Would you like to reset the database and apply the default app behavior? y/n: ");
                    char ch = System.Console.ReadKey().KeyChar;
                    if(ch == 'y') {
                        System.Console.WriteLine("\n\nDefault application initialized, " +
                                                 "database is now reseted.\n");
                        int maxId = studentsContext.Students.Max(s => s.Id);
                        Worker(maxId, studentLogger, studentsRepository, studentsContext);
                    }
                    else {
                        System.Console.WriteLine("\nFeel free to try again. \nGoodbye");
                    }
                    */
                }
            });
    }
/*
    public async void Worker(int maxId, IStudentLogger studentLogger, IStudentsRepository studentsRepository, StudentsContext studentsContext)
    {
        //Deletes every change previously done by the command line parser
        for (int i = 4; i <= maxId; i++)
        {
            await studentsRepository.DeleteAsync(i);
        }
        System.Console.WriteLine("Hello World");
        await studentsContext.Database.MigrateAsync();

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
    }
    */
}
