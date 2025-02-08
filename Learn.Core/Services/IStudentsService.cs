using Learn.Core.DataAccess.Models;

namespace Learn.Core.Services {
    public interface IStudentsService {
        Task<Student> CreateStudentAsync(Student student);
        Task<List<Student>> GetAllStudentsAsync();
        Task<Student> GetStudentByIdAsync(int studentId);

        Task UpdateStudentAsync(Student student);
        Task DeleteStudentAsync(int studentId);

    }
}