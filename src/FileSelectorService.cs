public class FileSelectorService
{
    public IEnumerable<FileInfo> GetFiles(DirectoryInfo directoryInfo)
    {
        if (!directoryInfo.Exists)
        {
            System.Diagnostics.Trace.WriteLine($"Directroy info skipped, because does not exist: [{directoryInfo}]");
            yield break;
        }

        bool yieldFile = false;
        foreach (var directoryFileInfo in directoryInfo.EnumerateFiles())
        {
            yieldFile = true;
            yield return directoryFileInfo;
        }

        if (!yieldFile)
        {
            System.Diagnostics.Trace.WriteLine($"Directroy info skipped, because no files found: [{directoryInfo}]");
        }

        yield break;
    }

    public IEnumerable<FileInfo> GetFiles(IEnumerable<FileSystemInfo> fileSystemInfos)
    {
        foreach (var info in fileSystemInfos)
        {
            if (info is FileInfo fileInfo)
            {
                yield return fileInfo;
            }
            else if (info is DirectoryInfo directoryInfo)
            {
                foreach (var directoryFileInfo in GetFiles(directoryInfo))
                {
                    yield return directoryFileInfo;
                }
            }
            else
            {
                System.Diagnostics.Trace.WriteLine($"File system info skipped, because not file or directory: [{info}]");
            }
        }

        yield break;
    }

    public IEnumerable<FileInfo> FilterAssemblyFiles(IEnumerable<FileInfo> fileInfos)
    {
        List<FileInfo> files = new List<FileInfo>();

        foreach (var info in fileInfos)
        {
            if (info.Extension.Equals(".dll", StringComparison.OrdinalIgnoreCase))
            {
                yield return info;
            }
            else
            {
                System.Diagnostics.Trace.WriteLine($"File info skipped, because not an assembly: [{info}]");
            }
        }

        yield break;
    }

    public IEnumerable<FileInfo> FilterExistingFiles(IEnumerable<FileInfo> fileInfos)
    {
        foreach (var info in fileInfos)
        {
            if (info.Exists)
            {
                yield return info;
            }
            else
            {
                System.Diagnostics.Trace.WriteLine($"File info skipped, because does not exist: [{info}]");
            }
        }

        yield break;
    }
}
