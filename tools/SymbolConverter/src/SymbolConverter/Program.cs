using SymbolConverter;

var app = ConsoleApp.Create(args);
app.AddCommands<SymbolApp>();
app.Run();

public class SymbolApp : ConsoleAppBase
{
    [Command("list")]
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

    [Command("prefix")]
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

        var list = await SymbolOperation.ListAsync(headerPath, prefix);

        Console.WriteLine($@"Source directory: {headerPath}");
        Console.WriteLine();
        await SymbolOperation.ReplaceAsync(implPath, list, dryrun);
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

    public static async Task ReplaceAsync(string implPath, IReadOnlyList<SymbolInfo?> symbols, bool dryrun)
    {
        var impleFiles = Directory.EnumerateFiles(implPath, "*.c", new EnumerationOptions { RecurseSubdirectories = true });
        var writer = new SymbolWriter();
        foreach (var file in impleFiles)
        {
            var content = await File.ReadAllTextAsync(file);
            content = writer.ReplaceSymbol(content, symbols.Select(x => x).ToArray());

            Console.WriteLine("--------------");
            Console.WriteLine(file);
            Console.WriteLine("--------------");
            if (dryrun)
            {
                Console.WriteLine(content);
            }
            else
            {
                File.WriteAllText(file, content);
            }
        }
    }
}
