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
    //CreateStudentAsync gets called with student.Id = 0
    public async Task CreateStudentAsync(Student student)
    {
        //We find the maximum id that exists in the database
        int max = _context.Students.Max(s => s.Id);
        //The new entry will have an id equal to max+1
        student.Id = max+1;
        _context.Students.Add(student);
        await _context.SaveChangesAsync();
    }
    public async Task<List<Student>> GetAllStudentsAsync()
    {
        return await _context.Students.AsNoTracking().OrderBy(s => s.Id).ToListAsync();
    }
    public async Task UpdateStudentAsync(Student student)
    {
        var trackedEntity = _context.Students.FirstOrDefault(s => s.Id == student.Id);
        if (trackedEntity != null)
        {
            _context.Entry(trackedEntity).State = EntityState.Detached;
            _context.Students.Update(student);
            await _context.SaveChangesAsync();
        }
        else {
            System.Console.WriteLine("Student " + student.Name + $", with id {student.Id} does not exist in the database. Try to add him/her first.");
        }
    }
}
