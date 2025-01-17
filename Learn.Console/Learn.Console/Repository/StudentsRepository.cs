using System;
using System.Collections.Generic;
using System.Linq;
using Learn.Console.DataAccess;
using Learn.Console.DataAccess.Models;
using Learn.Console.Repository;
using Microsoft.EntityFrameworkCore;

namespace Learn.Console.Repository
{
    public class StudentRepository : IStudentsRepository, IDisposable
    {
        // private readonly StudentsContext _context;
        // public StudentRepository(StudentsContext context)
        // {
        //     _context = context;
        // }

        public async Task<List<Student>> GetAllAsync() {
            using (var context = new StudentsContext()) 
            {
                var students = await context.Students.ToListAsync();
                return students;
            }
        }

        public async Task CreateAsync(Student student) {
            using (var context = new StudentsContext()) 
            {
                var checkExistingEntries = await context.Students.FirstOrDefaultAsync(x => x.Id == student.Id);
                if(checkExistingEntries == null)
                {
                    await context.Students.AddAsync(student);
                    await context.SaveChangesAsync();
                }
                else 
                {
                    System.Console.WriteLine("Entry already in db");
                }
            }
        }
        public async Task UpdateAsync(Student student) {
            using (var context = new StudentsContext()) 
            {
                // context.Entry(student).State = EntityState.Modified;
                var studentToChange = await context.Students.FirstOrDefaultAsync(x => x.Id == student.Id);

                if(studentToChange != null)
                {
                    studentToChange.Name = student.Name;
                    studentToChange.Age = student.Age;
                    await context.SaveChangesAsync();
                }
                else 
                {
                    System.Console.WriteLine("No entry updated");
                }
                await context.SaveChangesAsync();
            }
        }
        public async Task DeleteAsync(int studentId) {
            using (var context = new StudentsContext()) 
            {
                var studentToBeDeleted = await context.Students.FirstOrDefaultAsync(x => x.Id == studentId);

                if(studentToBeDeleted != null)
                {
                    context.Remove(studentToBeDeleted);
                    await context.SaveChangesAsync();
                    System.Console.WriteLine("We have deleted student 4");
                }
                else 
                {
                    System.Console.WriteLine("Student for deletion not found");
                }
            }
        }

        public void Dispose()
        {

        }

    }
}


