using Learn.Core.DataAccess;
using Learn.Core.DataAccess.Models;
using Microsoft.EntityFrameworkCore;

namespace Learn.Core.Repository
{
    public class StudentRepository : IStudentsRepository
    {
        private readonly StudentsContext _context;
        public StudentRepository(StudentsContext context) {
            _context = context;
        }
        public async Task<List<Student>> GetAllAsync() {
            return await _context.Students.AsNoTracking().OrderBy(s => s.Id).ToListAsync();
        }
        public async Task<Student> GetByIdAsync(int studentId)
        {
            var student = await _context.Students.FindAsync(studentId);
            return student!; // ! added due to Possible null reference return warning.
        }
        public async Task<Student> CreateAsync(Student student) {
            _context.Students.Add(student);
            await _context.SaveChangesAsync();
            return student;
        }
        public async Task UpdateAsync(Student student) {
            _context.Students.Update(student);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int studentId) {
            var student = await _context.Students.FindAsync(studentId);
            _context.Students.Remove(student!);
            await _context.SaveChangesAsync();
        }
    }
}


