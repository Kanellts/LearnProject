using CommandLine;

namespace Learn.Console;
public class StudentOperations
{
  [Option("create", Group = "StudentDbOperation", Required = false, HelpText = "Add a new student entry to the database (requires --name and --age)")]
  public bool addStudent { get; set; }
  [Option("update", Group = "StudentDbOperation", Required = false, HelpText = "Update a given student entry (requires --id, and either --name and/or --age)")]
  public bool updateStudent { get; set; }
  [Option("get", Group = "StudentDbOperation", Required = false, HelpText = "Prints every student on the database")]
  public bool getAllStudents { get; set; }
  [Option("id", Required = false, HelpText = "The id of the student (required for the update operation)")]
  public int newStudentId { get; set; }
  
  [Option("name", Required = false, HelpText = "The name of the student (required for create operation, optional for update operation).")]
  public string? newStudentName { get; set; }
  
  [Option("age", Required = false, HelpText = "The age of the student (required for create operation, optional for update operation)")]
  public int newStudentAge { get; set; }
}
