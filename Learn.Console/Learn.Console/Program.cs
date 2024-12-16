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
        // Console.WriteLine("After the update:");

        // deleteStudent();
        // Console.WriteLine("After removing:");
        // readStudent();

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
                Console.WriteLine("{0} {1} {2}",s.Id,s.Name,s.Age);
            }
        }
        return;
    }

    static void updateStudent() 
    {
        using (var db = new StudentsContext())
        {
            Student student = db.Students.Find(1);
            student.Name = "Veronica Smith";
            db.SaveChanges();
        }
        return;
    }

    static void deleteStudent()
    {
        using (var db = new StudentsContext())
        {
            Student student = db.Students.Find(2);
            db.Students.Remove(student);
            db.SaveChanges();
        }
    }
}
