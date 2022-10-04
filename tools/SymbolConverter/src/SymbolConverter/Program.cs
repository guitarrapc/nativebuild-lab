using SymbolConverter;
using System.IO;

var app = ConsoleApp.Create(args);
app.AddCommands<SymbolApp>();
app.Run();

public class SymbolApp : ConsoleAppBase
{
    [Command("list", "List symbols from header files (.h)")]
    public async Task List(
        [Option("header-path", "Directory path contains header files.")]string headerPath)
    {
        var list = await SymbolOperation.ListAsync(headerPath);
        var symbolLookup = list.ToLookup(x => x!.GetSourceFile());

        Console.WriteLine($@"Source directory: {headerPath}");
        Console.WriteLine();
        foreach (var symbols in symbolLookup)
        {
            Console.WriteLine($"File: {symbols.Key}");
            foreach (var symbol in symbols)
            {
                if (symbol is not null)
                {
                    Console.WriteLine($"  Type: {symbol.DetectionType}, Symbol: {symbol.Symbol}");
                }
            }

            Console.WriteLine();
        }
    }

    [Command("prefix", "Add prefix to header and implemataion files.")]
    public async Task Prefix(
        [Option("header-path", "Directory path contains header files.")] string headerPath,
        [Option("prefix", "Prefix to add.")] string prefix,
        [Option("impl-path", "Directory path contains implementaion files. (Default: Use headerPath)")] string implPath = "",
        [Option("dryrun", "True to dry-run. false to apply changes.")] bool dryrun = true
    )
    {
        if (string.IsNullOrEmpty(implPath))
        {
            implPath = headerPath;
        }

        var symbols = await SymbolOperation.ListAsync(headerPath, prefix);

        // header
        Console.WriteLine();
        Console.WriteLine($@"Header Source directory: {headerPath}");
        var headerFiles = Directory.EnumerateFiles(headerPath, "*.h", new EnumerationOptions { RecurseSubdirectories = true });
        await SymbolOperation.ReplaceAsync(headerFiles, symbols, dryrun);

        // impl
        Console.WriteLine();
        Console.WriteLine($@"Impl Source directory: {implPath}");
        var implFiles = Directory.EnumerateFiles(implPath, "*.c", new EnumerationOptions { RecurseSubdirectories = true });
        await SymbolOperation.ReplaceAsync(implFiles, symbols, dryrun);
    }
}

public static class SymbolOperation
{
    public static async Task<IReadOnlyList<SymbolInfo?>> ListAsync(string headerPath, string prefix = "")
    {
        var headerFiles = Directory.EnumerateFiles(headerPath, "*.h", new EnumerationOptions { RecurseSubdirectories = true });
        var reader = new SymbolReader();

        var list = new List<SymbolInfo?>();
        foreach (var file in headerFiles)
        {
            var content = await File.ReadAllLinesAsync(file);
            var methods = reader.Read(DetectionType.Method, content, s => prefix + s, file);
            var typedefs = reader.Read(DetectionType.Typedef, content, s => prefix + s, file);

            if (methods.Any())
            {
                list.AddRange(methods);
            }

            if (typedefs.Any())
            {
                list.AddRange(typedefs);
            }
        }
        return list;
    }

    public static async Task ReplaceAsync(IEnumerable<string> files, IReadOnlyList<SymbolInfo?> symbols, bool dryrun, bool debug = false)
    {
        var writer = new SymbolWriter();
        foreach (var file in files)
        {
            var content = await File.ReadAllTextAsync(file);
            var result = writer.ReplaceSymbol(content, symbols.Select(x => x).ToArray());

            var changed = !content.Equals(result);
            Console.WriteLine($"{file} (changed: {changed})");
            if (!dryrun)
            {
                if (changed)
                {
                    File.WriteAllText(file, result);
                }
            }

            if (debug)
            {
                Console.WriteLine("--------------");
                Console.WriteLine(result);
                Console.WriteLine("--------------");
            }
        }
    }
}
