using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;


public class StudentsContext : DbContext 
{
    private const string connectionString = "Host=localhost; Port=5432; Database=my-db; Username=postgres; Password=mysecretpassword";
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) 
        => optionsBuilder.UseNpgsql(connectionString)
        .UseSeeding((context,_) => 
        {
            var initialSeeding = context.Set<Student>().FirstOrDefault(b => b.Id == 0);
            if(initialSeeding == null) 
            {
                context.Set<Student>().Add(new Student {Id = 1, Name = "Jon Smith", Age = 22});
                context.Set<Student>().Add(new Student {Id = 2, Name = "Veronica Smith", Age = 19});
                context.Set<Student>().Add(new Student {Id = 3, Name = "Jane Doe", Age = 21});
                context.SaveChanges();
            }
        }//)
        // .UseAsyncSeeding(async (context,_,CancellationToken) => 
        // {
        //     var initialSeeding = await context.Set<Student>().FirstOrDefaultAsync(b => b.Id == 0, cancellationToken);
        //     if(initialSeeding == null) 
        //     {
        //         context.Set<Student>().Add(new Student {Id = 1, Name = "Jon Smith", Age = 22});
        //         context.Set<Student>().Add(new Student {Id = 2, Name = "Veronica Smith", Age = 19});
        //         context.Set<Student>().Add(new Student {Id = 3, Name = "Jane Doe", Age = 21});
        //         await context.SaveChanges();
        //     }
        // }
        );

    public DbSet<Student> Students { get; set; }
}

public class Student
{
    public int Id { get; set;}
    public string Name { get; set; }
    public int Age { get; set; }

}
