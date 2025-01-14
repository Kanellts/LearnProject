using Learn.Console.DataAccess.Models;
using Microsoft.EntityFrameworkCore;

namespace Learn.Console.DataAccess;
public class StudentsContext : DbContext
{
    private const string connectionString = "Host=localhost; Port=5432; Database=Learn; Username=postgres; Password=mysecretpassword";
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseNpgsql(connectionString);

    public DbSet<Student> Students { get; set; }
}


