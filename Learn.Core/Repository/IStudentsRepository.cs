using Learn.Core.DataAccess.Models;

namespace Learn.Core.Repository
{
    public interface IStudentsRepository
    {
        Task<List<Student>> GetAllAsync();
        Task<Student> CreateAsync(Student student);
        Task UpdateAsync(Student student);
        Task DeleteAsync(int studentId);
    }
}
