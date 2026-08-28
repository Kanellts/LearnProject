using Learn.Core.DataAccess.Models;
using Learn.Core.Repository;

namespace Learn.Core.Services;

public class StudentsService : IStudentsService
{
    private readonly IStudentsRepository _repository;
    public StudentsService(IStudentsRepository repository) {
            _repository = repository;
    }

    public async Task<Student> CreateStudentAsync(Student student)
    {
        var result = await _repository.CreateAsync(student);
        return result;
    }

    public async Task<List<Student>> GetAllStudentsAsync()
    {
        return await _repository.GetAllAsync();
    }

    public async Task<Student> GetStudentByIdAsync(int studentId)
    {
        return await _repository.GetByIdAsync(studentId);
    }

    public async Task<Student> UpdateStudentAsync(Student student)
    {
        var updatedStudent = await _repository.GetByIdAsync(student.Id);
        
        if(updatedStudent is not null) {
            return await _repository.UpdateAsync(student);
        }
        else {
            // This i think should be displayed with logger through LogError, 
            // especially for the console app. Then, i must register logger service for the WebApi
            // too and have the WebApi use logger too.
            System.Console.WriteLine($"No student with id = {student.Id}, returns null"); 
            return student;
        }
    }

    public async Task DeleteStudentAsync(int studentId)
    {
        await _repository.DeleteAsync(studentId);
    }

}
