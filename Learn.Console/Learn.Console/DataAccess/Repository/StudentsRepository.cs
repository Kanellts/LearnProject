using System;
using System.Collections.Generic;
using System.Linq;
using Learn.Console.DataAccess.Models;
using Learn.Console.DataAccess.Repository;
using Microsoft.EntityFrameworkCore;

namespace Learn.Console.DataAccess.Repository
{
    public class StudentRepository : IStudentsRepository
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
                var students = await context.Students.AddAsync(student);
                // context.SaveChangesAsync();
            }
        }
        public async Task UpdateAsync(int studentId, Student student) {
            using (var context = new StudentsContext()) 
            {
                var studentToChange = await context.Students.FirstOrDefaultAsync(x => x.Id == studentId);

                if(studentToChange != null)
                {
                    studentToChange.Id = student.Id;
                    studentToChange.Name = student.Name;
                    studentToChange.Age = student.Age;
                    // context.SaveChangesAsync();
                }
            }
        }
        public async Task DeleteAsync(int studentId) {
            using (var context = new StudentsContext()) 
            {
                var studentToBeDeleted = await context.Students.FirstOrDefaultAsync(x => x.Id == studentId);

                if(studentToBeDeleted != null)
                {
                    context.Remove(studentToBeDeleted);
                    // context.SaveChangesAsync();
                }
            }
        }


    }
}


