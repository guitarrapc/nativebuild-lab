using SymbolConverter;
using System.Diagnostics;

var app = ConsoleApp.Create(args);
app.AddCommands<SymbolApp>();
app.Run();

public class SymbolApp : ConsoleAppBase
{
    [Command("list", "List symbols from header files (.h)")]
    public async Task List(
        [Option("header-paths", "libary directory path to search header files.")] string[] headerPaths)
    {
        var list = await SymbolOperation.ListAsync(headerPaths);
        var symbolLookup = list.ToLookup(x => x!.GetSourceFile());

        Console.WriteLine($@"Source directory: {string.Join(",", headerPaths)}");
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
        [Option("header-paths", "Directory path contains header files.")] string[] headerPaths,
        [Option("prefix", "Prefix to add.")] string prefix,
        [Option("impl-paths", "Directory path contains implementaion files. (Default: Use headerPath)")] string[]? implPaths = null,
        [Option("dryrun", "True to dry-run. false to apply changes.")] bool dryrun = true
    )
    {
        if (implPaths is null)
        {
            implPaths = headerPaths;
        }

        var list = await SymbolOperation.ListAsync(headerPaths, prefix);

        if (list.Any())
        {
            // remove duplicate symbols
            var symbols = list.DistinctBy(x => x!.Symbol).Take(200).ToArray();
            Console.WriteLine("Replace following symbols...");
            foreach (var symbol in symbols)
            {
                Console.WriteLine($"  Type: {symbol!.DetectionType}, Symbol: {symbol!.Symbol}, RenameTo: {symbol!.RenamedSymbol}");
            }
            Console.WriteLine();

            // header
            Console.WriteLine();
            Console.WriteLine($@"Header Source directory: {string.Join(",", headerPaths)}");
            foreach (var headerPath in headerPaths)
            {
                var headerFiles = Directory.EnumerateFiles(headerPath, "*.h", new EnumerationOptions { RecurseSubdirectories = true });
                await SymbolOperation.ReplaceAsync(headerFiles, symbols, dryrun);
            }

            // impl
            Console.WriteLine();
            Console.WriteLine($@"Impl Source directory: {string.Join(",", implPaths)}");
            foreach (var implPath in implPaths)
            {
                var implFiles = Directory.EnumerateFiles(implPath, "*.c", new EnumerationOptions { RecurseSubdirectories = true });
                await SymbolOperation.ReplaceAsync(implFiles, symbols, dryrun);
            }
        }
    }
}

public static class SymbolOperation
{
    public static async Task<IReadOnlyList<SymbolInfo?>> ListAsync(string[] headerPaths, string prefix = "")
    {
        var list = new List<SymbolInfo?>();
        var reader = new SymbolReader();
        foreach (var headerPath in headerPaths)
        {
            var headerFiles = Directory.EnumerateFiles(headerPath, "*.h", new EnumerationOptions { RecurseSubdirectories = true });

            foreach (var file in headerFiles)
            {
                Debug.WriteLine($"Reading file '{file}'");

                var content = await File.ReadAllLinesAsync(file);
                var externFields = reader.Read(DetectionType.ExternField, content, s => prefix + s, file);
                var methods = reader.Read(DetectionType.Method, content, s => prefix + s, file);
                var typedefs = reader.Read(DetectionType.Typedef, content, s => prefix + s, file);

                if (externFields.Any())
                {
                    list.AddRange(externFields);
                }

                if (methods.Any())
                {
                    list.AddRange(methods);
                }

                if (typedefs.Any())
                {
                    list.AddRange(typedefs);
                }
            }
        }
        return list;
    }

    public static async Task ReplaceAsync(IEnumerable<string> files, IReadOnlyList<SymbolInfo?> symbols, bool dryrun, bool debug = false)
    {
        var writer = new SymbolWriter();
        foreach (var file in files)
        {
            Debug.WriteLine($"Reading file '{file}'");

            var content = await File.ReadAllTextAsync(file);
            var result = writer.ReplaceSymbol(content, symbols);

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
