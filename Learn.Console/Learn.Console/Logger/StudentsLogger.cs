using Learn.Console.DataAccess.Models;
using Learn.Console.DataAccess;
using Learn.Console.Repository;
using Microsoft.Extensions.Logging;


namespace Learn.Console.Logger 
{
    class StudentLogger 
    {
        public void LogStudents(List<Student> studentsList) 
        {
            ILoggerFactory factory = LoggerFactory.Create(builder => builder.AddConsole());
            ILogger logger = factory.CreateLogger("Students DB");
            logger.LogInformation("Displaying Student Database");
            foreach(var s in studentsList) {
                System.Console.WriteLine("{0} {1} {2}",s.Id,s.Name,s.Age);
            }
        }
        
    }
    
}