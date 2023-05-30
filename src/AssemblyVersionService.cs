public class AssemblyVersionService
{
    public Version? GetFileVersion(FileInfo fileInfo)
    {
        Version? fileVersion = null;

        var fileVersionInformation = System.Diagnostics.FileVersionInfo.GetVersionInfo(fileInfo.FullName);
        if (string.IsNullOrEmpty(fileVersionInformation?.FileVersion))
        {
            System.Diagnostics.Trace.WriteLine($"File version information not found for: [{fileInfo}]");
            return fileVersion;
        }

        if (Version.TryParse(fileVersionInformation.FileVersion, out Version? realFileVersion))
        {
            System.Diagnostics.Trace.WriteLine($"File version [{realFileVersion}] returned for: [{fileInfo}]");
            fileVersion = realFileVersion;
        }
        else
        {
            System.Diagnostics.Trace.WriteLine($"File version not found for: [{fileInfo}]");
        }

        return fileVersion;
    }
}