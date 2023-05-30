using System.CommandLine;

public class StudyCommand : Command
{
    private AssemblyVersionService assemblyVersionService;
    private FileSelectorService fileSelectorService;

    public StudyCommand(AssemblyVersionService assemblyVersionService, FileSelectorService fileSelectorService) : base("study", "Study the assembly (.dll) metadata.")
    {
        this.assemblyVersionService = assemblyVersionService;
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
            this.PrintStudy(file);
        }
    }

    private void PrintStudy(FileInfo file)
    {
        Console.WriteLine($"Assembly: {file}");
        
        var fileVersion = assemblyVersionService.GetFileVersion(file);
        Console.WriteLine($"File version: {fileVersion}");
    }
}
