using SymbolConverter;
using System.Diagnostics;

var app = ConsoleApp.Create(args);
app.AddCommands<SymbolApp>();
app.Run();

public class SymbolApp : ConsoleAppBase
{
    [Command("list", "List symbols from header files (.h)")]
    public async Task List(
        [Option("header-paths", "libary directory path to search header files.")] string[] headerPaths,
        [Option("macro-paths", "imple directory path to search macro.")] string[]? macroPaths = null
    )
    {
        var operation = new SymbolOperation(new SymbolReaderOption());
        var listMacros = macroPaths is not null ? await operation.ListMacrosAsync(macroPaths) : Array.Empty<SymbolInfo>();
        var listSymbols = await operation.ListSymbolsAsync(headerPaths);
        var symbolLookup = listMacros.Concat(listSymbols).ToLookup(x => x!.GetSourceFile());

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
        [Option("macro-paths", "imple directory path to search macro.")] string[]? macroPaths = null,
        [Option("dryrun", "True to dry-run. false to apply changes.")] bool dryrun = true
    )
    {
        if (implPaths is null)
        {
            implPaths = headerPaths;
        }

        var operation = new SymbolOperation(new SymbolReaderOption());
        var listMacros = macroPaths is not null ? await operation.ListMacrosAsync(macroPaths, prefix) : Array.Empty<SymbolInfo>();
        var listSymbols = await operation.ListSymbolsAsync(headerPaths, prefix);
        var list = listMacros.Concat(listSymbols).ToArray();

        if (list.Any())
        {
            // remove duplicate symbols
            var symbols = list.DistinctBy(x => x!.Symbol).Take(1500).ToArray();
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
                await operation.ReplaceAsync(headerFiles, symbols, dryrun);
            }

            // impl
            Console.WriteLine();
            Console.WriteLine($@"Impl Source directory: {string.Join(",", implPaths)}");
            foreach (var implPath in implPaths)
            {
                var implFiles = Directory.EnumerateFiles(implPath, "*.c", new EnumerationOptions { RecurseSubdirectories = true });
                await operation.ReplaceAsync(implFiles, symbols, dryrun);
            }
        }
    }
}

public class SymbolOperation
{
    private readonly SymbolReaderOption _option;

    public SymbolOperation(SymbolReaderOption option)
    {
        _option = option;
    }

    public async Task<IReadOnlyList<SymbolInfo?>> ListSymbolsAsync(string[] headerPaths, string prefix = "")
    {
        var list = new List<SymbolInfo?>();
        var reader = new SymbolReader(_option);
        foreach (var headerPath in headerPaths)
        {
            var files = Directory.EnumerateFiles(headerPath, "*.h", new EnumerationOptions { RecurseSubdirectories = true });

            foreach (var file in files)
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

    public async Task<IReadOnlyList<SymbolInfo?>> ListMacrosAsync(string[] macroPaths, string prefix = "")
    {
        var list = new List<SymbolInfo?>();
        var reader = new SymbolReader(_option);
        foreach (var headerPath in macroPaths)
        {
            var cfiles = Directory.EnumerateFiles(headerPath, "*.c", new EnumerationOptions { RecurseSubdirectories = true });
            var hfiles = Directory.EnumerateFiles(headerPath, "*.h", new EnumerationOptions { RecurseSubdirectories = true });
            var files = cfiles.Concat(hfiles);

            foreach (var file in files)
            {
                Debug.WriteLine($"Reading file '{file}'");

                var content = await File.ReadAllLinesAsync(file);
                var macros = reader.Read(DetectionType.Macro, content, s => prefix + s, file);

                if (macros.Any())
                {
                    list.AddRange(macros);
                }
            }
        }
        return list;
    }

    public async Task ReplaceAsync(IEnumerable<string> files, IReadOnlyList<SymbolInfo?> symbols, bool dryrun, bool debug = false)
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
