// See https://aka.ms/new-console-template for more information
using System.CommandLine;

var assemblyVersionService = new AssemblyVersionService();
var fileSelectorService = new FileSelectorService();

var rootCommand = new RootCommand("Cissa assmbly checker");
rootCommand.AddCommand(new StudyCommand(assemblyVersionService, fileSelectorService));

return await rootCommand.InvokeAsync(Environment.GetCommandLineArgs().Skip(1).ToArray());
