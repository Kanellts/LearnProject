using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;


public class StudentsContext : DbContext 
{
    private const string connectionString = "Host=localhost; Port=5432; Database=my-db; Username=postgres; Password=mysecretpassword";
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseNpgsql(connectionString);

    public DbSet<Student> Students { get; set; }

    
}

public class Student
{
    public int Id { get; set;}
    public string Name { get; set; }
    public int Age { get; set; }

}
