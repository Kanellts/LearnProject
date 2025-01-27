using CommandLine;

namespace Learn.Console;
public class StudentOperations
{
  [Option('o', "outputPath", Required = false, HelpText = "Set output path for the generated file.")]
  public string? outputPath { get; set; }

  [Option("operation", Required = true, HelpText = "Give --create. --update or --get, to choose which CRU operation you want to use")]
  public string? dbOperation { get; set; }
  [Option("id", Required = false, HelpText = "The id of the student (required for the update operation)")]
  public int newStudentId { get; set; }
  
  [Option("name", Required = false, HelpText = "The name of the student (required for create operation, optional for update operation).")]
  public string? newStudentName { get; set; }
  
  [Option("age", Required = false, HelpText = "The age of the student (required for create operation, optional for update operation)")]
  public int newStudentAge { get; set; }
}
