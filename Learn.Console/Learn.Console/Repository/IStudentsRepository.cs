using Learn.Console.DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

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
