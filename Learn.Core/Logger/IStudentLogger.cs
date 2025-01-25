using Learn.Core.DataAccess.Models;

namespace Learn.Core.Logger {
    public interface IStudentLogger
    {
        void LogStudents(List<Student> studentsList);
    }
}
