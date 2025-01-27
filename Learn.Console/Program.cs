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
                services.AddHostedService<ExecuteStudentServicesAsync>();
            });

        IHost host = builder.Build();
        await host.RunAsync();

    }

}

public sealed class ExecuteStudentServicesAsync(string[] args, IStudentLogger studentLogger, IStudentsService studentsService, StudentsContext studentsContext, IStudentsRepository studentsRepository) : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        System.Console.WriteLine("Hello World");
        await studentsContext.Database.MigrateAsync();

        await Parser.Default.ParseArguments<StudentOperations>(args)
            .WithParsedAsync(async options => {
             if(options.addStudent){
                    await studentsService.CreateStudentAsync(new Student { Id = 0, Name = options.newStudentName, Age = options.newStudentAge });
                }
                else if(options.updateStudent){
                    await studentsService.UpdateStudentAsync(new Student { Id = options.newStudentId, Name = options.newStudentName, Age = options.newStudentAge });
                }
                else if(options.getAllStudents) {
                    studentLogger.LogStudents(await studentsService.GetAllStudentsAsync());
                }
                else {
                    Worker(studentLogger,studentsRepository, studentsContext);
                }
            });
    }

public async void Worker(IStudentLogger studentLogger, IStudentsRepository studentsRepository, StudentsContext studentsContext)
{
    // protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    // {
        for(int i = 4; i <= 10; i++) {
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
}
// }
