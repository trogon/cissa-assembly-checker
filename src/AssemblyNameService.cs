using System.Reflection;

public class AssemblyNameService
{
    public IEnumerable<AssemblyName> GetReferencedAssemblies(FileInfo fileInfo)
    {
        var resolver = new PathAssemblyResolver(new string[] { fileInfo.FullName, typeof(object).Assembly.Location });
        using var mlc = new MetadataLoadContext(resolver, typeof(object).Assembly.GetName().ToString());

        // Load assembly into MetadataLoadContext
        Assembly assembly = mlc.LoadFromAssemblyPath(fileInfo.FullName);
        foreach (AssemblyName t in assembly.GetReferencedAssemblies())
        {
            yield return t;
        }

        yield break;
    }
}