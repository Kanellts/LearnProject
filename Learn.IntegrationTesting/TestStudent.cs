using Learn.Core.DataAccess;
using Learn.Core.DataAccess.Models;
using Learn.Core.Services;
using Learn.Core.Repository;
using Microsoft.EntityFrameworkCore;

namespace Learn.IntegrationTesting;

public class TestStudent
{
    // private readonly StudentsContext _studentContext;
    // public IStudentsService TestStudent() {
    //     var builder = new DbContextOptionsBuilder<StudentsContext>();
    //     builder.UseInMemoryDatabase(databaseName: "StudentDBInMemory");

    //     DbContextOptions<StudentsContext> options = builder.Options; 
    //     StudentsContext _studentContext = new StudentsContext(options);
    //     // Delete existing DB before creating a new one
    //     _studentContext.Database.EnsureDeletedAsync();
    //     _studentContext.Database.EnsureCreatedAsync();
    //     return new StudentsService(new StudentRepository(_studentContext));
    // }

    // public IStudentsService GetInMemoryReadService() {
    //     return new StudentsService(new StudentRepository(_studentContext));
    // }
    // public IStudentsService GetInMemoryWriteService() {
    //     return new StudentsService(new StudentRepository(_studentContext));
    // }
}
