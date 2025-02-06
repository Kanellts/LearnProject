using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using CommandLine;
using Learn.Core.DataAccess;
using Learn.Core.DataAccess.Models;
using Learn.Core.Repository;
using Learn.Core.Logger;
using Learn.Core.Services;

namespace Learn.Console;

class Program
{
    static async Task Main(string[] args)
    {
        var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())  // Ορίζει το path όπου βρίσκεται το appsettings.json
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)  // Φορτώνει το αρχείο
            .Build();

        // var services = new ServiceCollection();
        // .AddSingleton<IStudentsRepository, StudentRepository>();
        // .AddSingleton<IStudentsService, StudentsService>();
        // .AddSingleton<IStudentLogger, StudentLogger>();


        var serviceProvider = new ServiceCollection()
            .AddSingleton<IConfiguration>(configuration)  // Προσθήκη IConfiguration
            .AddDbContext<StudentsContext>(options =>
                options.UseNpgsql(configuration.GetConnectionString("DefaultConnection"))) // Ρύθμιση DbContext
            .AddSingleton<IStudentsService, StudentsService>()
            .AddSingleton<IStudentsRepository, StudentRepository>()
            .AddSingleton<IStudentsService, StudentsService>()
            .AddSingleton<IStudentLogger, StudentLogger>()
            .BuildServiceProvider();

        var studentsService = serviceProvider.GetRequiredService<IStudentsService>();
        var studentLogger = serviceProvider.GetRequiredService<IStudentLogger>();
        var studentsContext = serviceProvider.GetRequiredService<StudentsContext>();

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
                }
            });
    }
}