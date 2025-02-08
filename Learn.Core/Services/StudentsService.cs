using Learn.Core.DataAccess.Models;
using Learn.Core.DataAccess;
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

    public async Task UpdateStudentAsync(Student student)
    {
        await _repository.UpdateAsync(student);
    }

    public async Task DeleteStudentAsync(int studentId)
    {
        await _repository.DeleteAsync(studentId);
    }

}
