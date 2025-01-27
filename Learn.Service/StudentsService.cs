using Learn.Core.DataAccess.Models;
using Learn.Core.DataAccess;
using Microsoft.EntityFrameworkCore;

namespace Learn.Service;

public class StudentsService : IStudentsService
{
    private readonly StudentsContext _context;
    public StudentsService(StudentsContext context) {
            _context = context;
    }
    public async Task CreateStudentAsync(Student student)
    {
        int max = _context.Students.Max(u => u.Id);
        student.Id = max+1;
        _context.Students.Add(student);
        await _context.SaveChangesAsync();
    }
    public async Task<List<Student>> GetAllStudentsAsync()
    {
        return await _context.Students.AsNoTracking().ToListAsync();
    }
    public async Task UpdateStudentAsync(Student student)
    {
        var trackedEntity = _context.Students.FirstOrDefault(s => s.Id == student.Id);
        if (trackedEntity != null)
        {
            _context.Entry(trackedEntity).State = EntityState.Detached;
        }
        _context.Students.Update(student);
        await _context.SaveChangesAsync();
    }
}
