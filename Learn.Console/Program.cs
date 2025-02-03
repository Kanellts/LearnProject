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
                services.AddSingleton(args);
            });

        using IHost host = builder.Build();
        using var scope = host.Services.CreateScope();
        var services = scope.ServiceProvider;

        // Ανάκτηση εξαρτήσεων
        var studentLogger = services.GetRequiredService<IStudentLogger>();
        var studentsService = services.GetRequiredService<IStudentsService>();
        var studentsRepository = services.GetRequiredService<IStudentsRepository>();
        var studentsContext = services.GetRequiredService<StudentsContext>();

        // Εκτέλεση χωρίς background service
        await RunAppAsync(args, studentsContext, studentsService, studentLogger, studentsRepository);
    }

    static async Task RunAppAsync(string[] args, StudentsContext studentsContext, IStudentsService studentsService, IStudentLogger studentLogger, IStudentsRepository studentsRepository)
    {
        System.Console.WriteLine("Parser initialized\n");
        await studentsContext.Database.MigrateAsync();

        await Parser.Default.ParseArguments<StudentOperations>(args)
            .WithParsedAsync(async options =>
            {
                if (options.addStudent)
                {
                    await studentsService.CreateStudentAsync(new Student { Id = 0, Name = options.newStudentName, Age = options.newStudentAge });
                }
                else if (options.updateStudent)
                {
                    await studentsService.UpdateStudentAsync(new Student { Id = options.newStudentId, Name = options.newStudentName, Age = options.newStudentAge });
                }
                else if (options.getAllStudents)
                {
                    studentLogger.LogStudents(await studentsService.GetAllStudentsAsync());
                }
                else
                {
                    System.Console.Write($"\nThe option {options} is not valid for CRU operation." +
                                            "\nDefault behavior of the app will be initialized, if so," +
                                            "every student with Id greater than or equal to 4, is going to be removed from the database.\n" +
                                            "Would you like to reset the database and apply the default app behavior? y/n: ");
                    char ch = System.Console.ReadKey().KeyChar;
                    if (ch == 'y')
                    {
                        System.Console.WriteLine("\n\nDefault application initialized, " +
                                                 "database is now reset.\n");
                        int maxId = studentsContext.Students.Max(s => s.Id);
                        await ExecuteStudentWorkflow(maxId, studentLogger, studentsRepository, studentsContext);
                    }
                    else
                    {
                        System.Console.WriteLine("\nFeel free to try again. \nGoodbye");
                    }
                }
            });
    }

    static async Task ExecuteStudentWorkflow(int maxId, IStudentLogger studentLogger, IStudentsRepository studentsRepository, StudentsContext studentsContext)
    {
        for (int i = 4; i <= maxId; i++)
        {
            await studentsRepository.DeleteAsync(i);
        }
        System.Console.WriteLine("Hello World");
        await studentsContext.Database.MigrateAsync();

        System.Console.WriteLine("\nBefore the add");
        studentLogger.LogStudents(await studentsRepository.GetAllAsync());

        // Let's add a new student to the database:
        await studentsRepository.CreateAsync(new Student { Id = 4, Name = "Kanellos Tsitouras", Age = 24 });

        // Check if we succeeded
        System.Console.WriteLine("\nAfter the Create");
        studentLogger.LogStudents(await studentsRepository.GetAllAsync());

        // Let's update the new entry:
        await studentsRepository.UpdateAsync(new Student { Id = 4, Name = "Kanellos Tsitouras", Age = 25 });

        // Check if we succeeded
        System.Console.WriteLine("\nAfter the Update");
        studentLogger.LogStudents(await studentsRepository.GetAllAsync());

        // Let's delete the new student from the database:
        await studentsRepository.DeleteAsync(4);

        // Check if we succeeded
        System.Console.WriteLine("\nAfter the Delete");
        studentLogger.LogStudents(await studentsRepository.GetAllAsync());
    }
}
