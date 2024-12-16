using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

namespace Learn.Console.Models 
{
    public class StudentsContext : DbContext 
    {
        public DbSet<Student> Students { get; set; }

        public string DbPath { get; }
        // public StudentsContext()
        // {
        //     var folder = Environment.SpecialFolder.LocalApplicationData;
        //     var path = Environment.GetFolderPath(folder);
        //     DbPath = System.IO.Path.Join(path, "mydatabase.db");
        // }
        // public StudentsContext() 
        // {
        // }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            => optionsBuilder.UseNpgsql(@"Host=localhost;Username=postgres;Password=pwd;Database=database");
        // protected override void OnConfiguring(DbContextOptionsBuilder options)
        //     => options.UseNpgsql($"Data Source={DbPath}");
    }

    public class Student
    {
        public int Id { get; set;}
        public string Name { get; set; }
        public int Age { get; set; }

        public Student(int id, string name, int age) {
            this.Id = id;
            this.Name = name;
            this.Age = age;
        }
    }
}