using CommandLine;

namespace Learn.Console;
public class StudentOperations
{
  [Option("create", Required = false, HelpText = "Add a new student entry to the database (requires --name and --age)")]
  public bool addStudent { get; set; }
  [Option("update", Required = false, HelpText = "Update a given student entry (requires --id, --name and --age)")]
  public bool updateStudent { get; set; }
  [Option("get", Required = false, HelpText = "Prints every student on the database")]
  public bool getAllStudents { get; set; }
  [Option("id", Required = false, HelpText = "The id of the student (required for the update operation)")]
  public int newStudentId { get; set; }
  
  [Option("name", Required = false, HelpText = "The name of the student (required for create/update operations")]
  public required string newStudentName { get; set; }
  
  [Option("age", Required = false, HelpText = "The age of the student (required for create/update operations")]
  public int newStudentAge { get; set; }
}
