using Learn.Core.DataAccess;
using Learn.Core.DataAccess.Models;
using Learn.Core.Services;
using Learn.Core.Repository;
using Microsoft.EntityFrameworkCore;

namespace Learn.IntegrationTesting;

public class StudentServiceTests
{
    private readonly StudentsContext _studentContext;
    private readonly StudentsService _studentsService;
    public StudentServiceTests() {
        var builder = new DbContextOptionsBuilder<StudentsContext>();
        builder.UseInMemoryDatabase(databaseName: "StudentDBInMemory");

        DbContextOptions<StudentsContext> options = builder.Options; 

        _studentContext = new StudentsContext(options);
        // Delete existing DB before creating a new one
        _studentContext.Database.EnsureDeletedAsync();
        _studentContext.Database.EnsureCreatedAsync();

        _studentsService = new StudentsService(new StudentRepository(_studentContext));
    }

    [Fact]
    public async Task CreateStudentAsyncSuccess()
    {
        var testNewStudent = new Student{ Id = 1, Name = "John Smith", Age = 22};

        var createdStudent = await _studentsService.CreateStudentAsync(testNewStudent);
        var newlyAddedStudent = await _studentContext.Students.FindAsync(1);

        Assert.NotNull(createdStudent);
        Assert.Equal(1, newlyAddedStudent?.Id);
        Assert.Equal("John Smith", newlyAddedStudent?.Name);
        Assert.Equal(22, newlyAddedStudent?.Age);
    }

    [Fact]
    public async Task UpdateStudentAsyncSuccess()
    {
        //Add a new student first to have the id available
        var testUpdatedStudent = new Student{Id = 2, Name = "Veronica Smith", Age = 24};
        await _studentsService.CreateStudentAsync(testUpdatedStudent);

        testUpdatedStudent.Name = "Veronica Smith Updated";
        testUpdatedStudent.Age = 19;

        var updatedStudent = await _studentsService.UpdateStudentAsync(testUpdatedStudent);
        var newlyUpdatedStudent = await _studentContext.Students.FindAsync(2);

        Assert.Equal("Veronica Smith Updated", newlyUpdatedStudent?.Name);
        Assert.Equal(19, newlyUpdatedStudent?.Age);
    }

    [Fact]
    public async Task DeleteStudentAsyncSuccess()
    {
        //Add a new student first to have the id available
        var testDeletedStudent = new Student{Id = 3, Name = "Jane Doe", Age = 21};
        await _studentsService.CreateStudentAsync(testDeletedStudent);

        await _studentsService.DeleteStudentAsync(3);

        var deletedStudent = await _studentContext.Students.FindAsync(3);

        Assert.Null(deletedStudent);
    }

}