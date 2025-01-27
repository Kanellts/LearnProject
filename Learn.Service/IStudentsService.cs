using Learn.Core.DataAccess.Models;

namespace Learn.Service {
    public interface IStudentsService {
        Task CreateStudentAsync(Student student);
        Task<List<Student>> GetAllStudentsAsync();
        Task UpdateStudentAsync(Student student);
    }
}