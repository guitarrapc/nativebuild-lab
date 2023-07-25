using SymbolConverter;
using System.Diagnostics;

var app = ConsoleApp.Create(args);
app.AddCommands<SymbolApp>();
app.Run();

public class SymbolApp : ConsoleAppBase
{
    private static readonly string[] defaultHeaderExtensions = new[] { "*.h" };
    private static readonly string[] defaultImplExtensions = new[] { "*.c" };
    private static readonly string[] defaultMacroExtensions = new[] { "*.h", "*.c" };

    [Command("list", "List symbols from header files (.h)")]
    public async Task List(
        [Option("header-paths", "Libary directory path to search header files.")] string[] headerPaths,
        [Option("macro-paths", "Implement directory path to search macro.")] string[]? macroPaths = null,
        [Option("header-extensions", "Header file extensions. (default: *.h)")] string[]? headerExtensions = null,
        [Option("macro-extensions",  "Macro file extensions. (default: *.h,*.c)")] string[]? macroExtensions = null,
        [Option("sort", "Sort symbols for each files.")] bool sort = false
    )
    {
        var macroDir = macroPaths ?? Array.Empty<string>();
        var headerExt = headerExtensions ?? defaultHeaderExtensions;
        var macroExt = macroExtensions ?? defaultMacroExtensions;

        Debug.WriteLine($@"Listing symbols.");
        Debug.WriteLine($@"  Header directory: {headerPaths.ToJoinedString()}");
        Debug.WriteLine($@"  Header extexntions: {headerExt.ToJoinedString()}");
        Debug.WriteLine($@"  Macro directory: {macroDir.ToJoinedString()}");
        Debug.WriteLine($@"  Macro extexntions: {macroExt.ToJoinedString()}");

        var operation = new SymbolOperation(new SymbolReaderOption(Sort: sort));
        var listMacros = macroPaths is not null ? await operation.ListMacrosAsync(macroPaths, macroExt) : Array.Empty<SymbolInfo>();
        var listSymbols = await operation.ListSymbolsAsync(headerPaths, headerExt);
        var symbolLookup = listMacros.Concat(listSymbols).ToLookup(x => x!.GetSourceFile());

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
        [Option("prefix", "Prefix to add.")] string prefix,
        [Option("header-paths", "Directory path contains header files.")] string[] headerPaths,
        [Option("impl-paths", "Directory path contains implementaion files. (Default: Use headerPath)")] string[]? implPaths = null,
        [Option("macro-paths", "imple directory path to search macro.")] string[]? macroPaths = null,
        [Option("header-extensions", "Header file extensions. (default: *.h)")] string[]? headerExtensions = null,
        [Option("impl-extensions", "Header file extensions. (default: *.c)")] string[]? implExtensions = null,
        [Option("macro-extensions", "Macro file extensions. (default: *.h,*.c)")] string[]? macroExtensions = null,
        [Option("method-delimiters", "Additional delimiters for method.")] string[]? methodDelimiters = null,
        [Option("typedef-delimiters", "Additional delimiters for typedef.")] string[]? typedefDelimiters = null,
        [Option("extern-delimiters", "Additional delimiters for extern.")] string[]? externDelimiters = null,
        [Option("macro-delimiters", "Additional delimiters for macro.")] string[]? macroDelimiters = null,
        [Option("dryrun", "True to dry-run. false to apply changes.")] bool dryrun = true
    )
    {
        var macroDir = macroPaths ?? Array.Empty<string>();
        var headerExt = headerExtensions ?? defaultHeaderExtensions;
        var implExt = implExtensions ?? defaultImplExtensions;
        var macroExt = macroExtensions ?? defaultMacroExtensions;
        if (implPaths is null)
        {
            implPaths = headerPaths;
        }
        SymbolDelimiters.AddDelimiters(methodDelimiters, typedefDelimiters, externDelimiters, macroDelimiters);

        var operation = new SymbolOperation(new SymbolReaderOption());
        var listMacros = macroPaths is not null ? await operation.ListMacrosAsync(macroPaths, macroExt, prefix) : Array.Empty<SymbolInfo>();
        var listSymbols = await operation.ListSymbolsAsync(headerPaths, headerExt, prefix);
        var list = listMacros.Concat(listSymbols).ToArray();

        if (list.Any())
        {
            // remove duplicate symbols
            var symbols = list.DistinctBy(x => x!.Symbol).ToArray();
            Console.WriteLine("Replace following symbols...");
            foreach (var symbol in symbols)
            {
                Console.WriteLine($"  Type: {symbol!.DetectionType}, Symbol: {symbol!.Symbol}, RenameTo: {symbol!.RenamedSymbol}");
            }
            Console.WriteLine();

            // header
            Console.WriteLine();
            Console.WriteLine($@"Header Source directory: {string.Join(",", headerPaths)}");
            await operation.ReplaceAsync(headerPaths, headerExt, symbols, dryrun);

            // impl
            Console.WriteLine();
            Console.WriteLine($@"Impl Source directory: {string.Join(",", implPaths)}");
            await operation.ReplaceAsync(implPaths, implExt, symbols, dryrun);
        }
    }
}

public static class EnumerableExtensions
{
    public static string ToJoinedString(this IEnumerable<string> values, string separator = ",") => string.Join(separator, values);
}

public class SymbolOperation
{
    private readonly SymbolReaderOption _option;

    public SymbolOperation(SymbolReaderOption option)
    {
        _option = option;
    }

    public async Task<IReadOnlyList<SymbolInfo?>> ListSymbolsAsync(string[] paths, string[] extensions, string prefix = "")
    {
        var list = new List<SymbolInfo?>();
        var reader = new SymbolReader(_option);
        foreach (var path in paths)
        {
            var files = Enumerable.Empty<string>();
            foreach (var extension in extensions)
            {
                files = files.Concat(Directory.EnumerateFiles(path, extension, new EnumerationOptions { RecurseSubdirectories = true }));
            }

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

    public async Task<IReadOnlyList<SymbolInfo?>> ListMacrosAsync(string[] paths, string[] extensions, string prefix = "")
    {
        var list = new List<SymbolInfo?>();
        var reader = new SymbolReader(_option);
        foreach (var path in paths)
        {
            var files = Enumerable.Empty<string>();
            foreach (var extension in extensions)
            {
                files = files.Concat(Directory.EnumerateFiles(path, extension, new EnumerationOptions { RecurseSubdirectories = true }));
            }

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

    public async Task ReplaceAsync(string[] paths, string[] extensions, IReadOnlyList<SymbolInfo?> symbols, bool dryrun, bool debug = false)
    {
        var writer = new SymbolWriter();
        foreach (var path in paths)
        {
            var files = Enumerable.Empty<string>();
            foreach (var extension in extensions)
            {
                files = files.Concat(Directory.EnumerateFiles(path, extension, new EnumerationOptions { RecurseSubdirectories = true }));
            }

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
}
