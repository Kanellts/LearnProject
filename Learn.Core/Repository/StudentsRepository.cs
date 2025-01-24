using Learn.Core.DataAccess;
using Learn.Core.DataAccess.Models;
using Microsoft.EntityFrameworkCore;

namespace Learn.Core.Repository
{
    public class StudentRepository : IStudentsRepository, IDisposable
    {
        public async Task<List<Student>> GetAllAsync() {
            using (var context = new StudentsContext()) 
            {
                return await context.Students.AsNoTracking().ToListAsync();
            }
        }

        public async Task CreateAsync(Student student) {
            using (var context = new StudentsContext()) 
            {
                context.Students.Add(student);
                await context.SaveChangesAsync();
            }
        }
        public async Task UpdateAsync(Student student) {
            using (var context = new StudentsContext()) 
            {
                context.Students.Update(student);
                await context.SaveChangesAsync();
            }
        }
        public async Task DeleteAsync(int studentId) {
            using (var context = new StudentsContext()) 
            {
                await context.Students.Where(std => std.Id == studentId).ExecuteDeleteAsync(); 
            }
        }

        public void Dispose()
        {

        }

    }
}


