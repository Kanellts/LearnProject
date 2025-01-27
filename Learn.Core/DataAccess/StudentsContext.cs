using Learn.Core.DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Learn.Core.DataAccess;
public class StudentsContext : DbContext
{
    
    private readonly IConfiguration _configuration;
    public StudentsContext(IConfiguration configuration)
    {
        _configuration = configuration;
    }
    //For queries to be displayed we can use this:
    // public StudentsContext(IConfiguration configuration, DbContextOptions<StudentsContext> options) : base(options)
    // {
    //     _configuration = configuration;
    // }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if(!optionsBuilder.IsConfigured)
        {

            optionsBuilder.UseNpgsql(_configuration.GetConnectionString("DefaultConnection"));
        }
    }
    
    public DbSet<Student> Students { get; set; }
}


