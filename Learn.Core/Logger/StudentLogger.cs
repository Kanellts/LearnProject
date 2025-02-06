using Learn.Core.DataAccess.Models;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Security.Cryptography;

namespace Learn.Core.Logger 
{
    public class StudentLogger : IStudentLogger
    {
        private readonly ILogger<StudentLogger> _logger;
        public StudentLogger(ILogger<StudentLogger> logger) {
            _logger = logger;
        }
        private string FormatStudents(List<Student> studentsList) {
            string str = "Displaying Student Database\n";
            foreach(var s in studentsList)
            {
                str += $"{s.Id}{s.Name}{s.Age}\n";
            }
            return str;
        }
        public void LogStudents(List<Student> studentsList) 
        {
            //Our logger will diplay the data in the console
            _logger.LogInformation("Displaying Student Database\n");
            foreach(var s in studentsList)
            {
                _logger.LogInformation("{Id} {Name} {Age}",s.Id,s.Name,s.Age);
            }
        }
    }
}
