using Learn.Core.DataAccess.Models;

namespace Learn.Core.Services {
    public interface IStudentsService {
        Task<Student> CreateStudentAsync(Student student);
        Task<List<Student>> GetAllStudentsAsync();
        Task UpdateStudentAsync(Student student);
    }
}