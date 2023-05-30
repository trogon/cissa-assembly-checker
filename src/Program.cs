using System.CommandLine;

var assemblyNameService = new AssemblyNameService();
var assemblyVersionService = new AssemblyVersionService();
var fileSelectorService = new FileSelectorService();

var rootCommand = new RootCommand("Cissa Assmbly Checker");
rootCommand.AddCommand(new RefsCommand(assemblyNameService, fileSelectorService));
rootCommand.AddCommand(new StudyCommand(assemblyVersionService, fileSelectorService));

return await rootCommand.InvokeAsync(Environment.GetCommandLineArgs().Skip(1).ToArray());
