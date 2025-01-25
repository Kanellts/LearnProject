using Learn.Core.DataAccess.Models;
using Microsoft.Extensions.Logging;


namespace Learn.Core.Logger 
{
    public class StudentLogger : IStudentLogger
    {
        //This function is used only for output manipulation. The original LogStudents 
        //is commented out below, and it was abandonded in order to not have
        //info: Students DB[0] be displayed, next to every db output
        private string forLoop(List<Student> studentsList)
        {
            string str = "Displaying Student Database\n";
            foreach(var s in studentsList) {
                str = str + s.Id + " " + s.Name + " " + s.Age + "\n";
            }
            return str;
        }
        public void LogStudents(List<Student> studentsList) 
        {
            //Our logger will diplay the data in the console
            ILoggerFactory factory = LoggerFactory.Create(builder => builder.AddConsole());
            ILogger logger = factory.CreateLogger("Students DB");
            logger.LogInformation(forLoop(studentsList));
        }

      
        //If you use this, comment out the aforementioned methods
        // 
        // public void LogStudents(List<Student> studentsList) 
        // {
        //     //Our logger will diplay the data in the console
        //     ILoggerFactory factory = LoggerFactory.Create(builder => builder.AddConsole());
        //     ILogger logger = factory.CreateLogger("Students DB");
        //     logger.LogInformation("Displaying Student Database");
        //     foreach(var s in studentsList) {
        //         logger.LogInformation("{0} {1} {2}",s.Id,s.Name,s.Age);
        //     }
        // }        
    }
}