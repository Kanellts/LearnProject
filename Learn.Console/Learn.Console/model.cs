using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

public class StudentsContext : DbContext 
{
    public DbSet<Student> Students { get; set; }

    // public string DbPath { get; }

    public StudentsContext() 
    {
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseNpgsql(@"Host=myserver;Username=mylogin;Password=mypass;Database=mydatabase");
}

public class Student
{
    public int Id { get; set;}
    public string name { get; set; }
    public int age { get; set; }
}