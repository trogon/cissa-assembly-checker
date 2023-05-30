using System.CommandLine;

public class RefsCommand : Command
{
    private AssemblyNameService assemblyNameService;
    private FileSelectorService fileSelectorService;

    public RefsCommand(AssemblyNameService assemblyNameService, FileSelectorService fileSelectorService) : base("refs", "List the assembly (.dll) references.")
    {
        this.assemblyNameService = assemblyNameService;
        this.fileSelectorService = fileSelectorService;

        var dataArgument = this.InitFilesAndDirectoriesArgument();
        this.AddArgument(dataArgument);
        this.SetHandler((data) => { ProcessCommand(data); }, dataArgument);
    }

    private Argument<FileSystemInfo[]> InitFilesAndDirectoriesArgument()
    {
        return new Argument<FileSystemInfo[]>("files or directories", "List of files and directories to study.") { Arity = ArgumentArity.OneOrMore };
    }

    private void ProcessCommand(FileSystemInfo[] data)
    {
        var userFiles = fileSelectorService.GetFiles(data);
        var filesToStudy = fileSelectorService.FilterAssemblyFiles(userFiles);
        var existingFilesToStudy = fileSelectorService.FilterExistingFiles(filesToStudy);

        foreach (var file in existingFilesToStudy)
        {
            this.PrintRefs(file);
        }
    }

    private void PrintRefs(FileInfo file)
    {
        Console.WriteLine($"Assembly: {file}");

        var refs = assemblyNameService.GetReferencedAssemblies(file);
        foreach (var lib in refs)
        {
            Console.WriteLine($"Referenced assembly: {lib}");
        }
    }
}
