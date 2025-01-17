using Learn.Console.DataAccess.Models;

namespace Learn.Console.Repository
{
    public interface IStudentsRepository
    {
        Task<List<Student>> GetAllAsync();
        Task CreateAsync(Student student);
        Task UpdateAsync(Student student);
        Task DeleteAsync(int studentId);
    }
}
