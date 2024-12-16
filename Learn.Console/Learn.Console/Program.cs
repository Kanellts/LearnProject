using Npgsql;
using System;
using System.Linq;

// namespace Learn.Console;
class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Hello World");
        // insertStudent();

        readStudent();
        // updateStudent();
        readStudent();

        Console.WriteLine("Press any key to continue");
        Console.ReadKey();
    }

    static void insertStudent()
    {
        using (var db = new StudentsContext())
        {
            Student student = new Student();
            student.Id = 2;
            student.Name = "Joe Smith";
            student.Age = 20;

            db.Add(student);

            db.SaveChanges();

        }
        return;
    }

    static void readStudent()
    {
        using (var db = new StudentsContext()) 
        {
            List<Student> students = db.Students.ToList();
            foreach(Student s in students)
            {
                Console.WriteLine("{0} {1}",s.Id,s.Name);
            }
        }
        return;
    }

}
