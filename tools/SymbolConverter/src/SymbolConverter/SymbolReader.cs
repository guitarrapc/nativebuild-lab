using System.Text.RegularExpressions;
using System.Text;

public class SymbolReader
{
    public IReadOnlyList<SymbolInfo?> Read(DetectionType detectionType, string[] content, Func<string, string> RenameExpression, string? file = null)
    {
        var metadata = new Dictionary<string, string>
        {
            { "file", file ?? "" },
        };
        return detectionType switch
        {
            DetectionType.Method => ReadMethodInfo(content, RenameExpression, metadata),
            DetectionType.Typedef => ReadTypedefInfo(content, RenameExpression, metadata),
            _ => throw new NotSupportedException(),
        };
    }

    private IReadOnlyList<SymbolInfo?> ReadMethodInfo(string[] content, Func<string, string> RenameExpression, Dictionary<string, string> metadata)
    {
        const string delimiter = "(";
        // `foo bar(`
        // ` foo bar(  foo`
        var regex = new Regex($@"\b(?<type>\w+?)\s+(?<method>\w+?)\{delimiter}.*$", RegexOptions.Compiled);
        //content.Dump(path);

        var methods = content
            .Select(x => x.TrimStart()) // ` void foo()` -> `void foo()`
            .Where(x => !string.IsNullOrWhiteSpace(x)) // empty line
            .Where(x => !x.StartsWith("//")) // -> // void method(int foo);
            .Where(x => !x.StartsWith("#"))  // -> #if defined
            .Select(x =>
            {
                var match = regex.Match(x);
                if (match.Success)
                {
                    //match.Dump();
                    var line = match.Groups[0].Value;
                    var type = match.Groups["type"].Value;
                    var method = match.Groups["method"].Value;
                    metadata.TryAdd("ReturnType", type);
                    return new SymbolInfo(line, DetectionType.Method, delimiter, method)
                    {
                        RenamedSymbol = RenameExpression.Invoke(method),
                        Metadata = metadata,
                    };
                }
                else
                {
                    return null;
                }
            })
            .Where(x => x != null)
            .OrderByDescending(x => x?.Symbol.Length)
            .ToArray();

        return methods;
    }

    private IReadOnlyList<SymbolInfo?> ReadTypedefInfo(string[] content, Func<string, string> RenameExpression, Dictionary<string, string> metadata)
    {
        const string delimiter = ";";
        // `typedef uint64_t foo;`
        // `typedef enum {
        // } foo;`
        var typedefStartRegex = new Regex(@"^\s*typedef.*", RegexOptions.Compiled);
        var typedefEndRegex = new Regex($@"(\btypedef.*|}}\s+\w+){delimiter}", RegexOptions.Compiled);
        // `typedef uint64_t foo;`
        var typedefSinglelineRegex = new Regex($@"\btypedef\s+\w+\s+(?<type>\w+){delimiter}", RegexOptions.Compiled);
        // `} foo;`
        var typedefMultilineRegex = new Regex($@"\s*}}\s+(?<type>\w+){delimiter}", RegexOptions.Compiled);

        // Get typedef line in single string
        var typedefLines = new List<string>();
        for (var i = 0; i < content.Length; i++)
        {
            var line = content[i];
            var sb = new StringBuilder();
            if (typedefStartRegex.IsMatch(line))
            {
                sb.AppendLine(line);

                while (!typedefEndRegex.IsMatch(content[i]))
                {
                    i++;
                    sb.AppendLine(content[i]);
                }

                typedefLines.Add(sb.ToString());
            }
        }

        var typedefs = typedefLines
            .Select(x => x.TrimStart())
            .Select(line =>
            {
                var multilineMatch = typedefMultilineRegex.Match(line);
                if (multilineMatch.Success)
                {
                    //multilineMatch.Dump();
                    var typeName = multilineMatch.Groups["type"].Value;
                    return new SymbolInfo(line, DetectionType.Typedef, delimiter, typeName)
                    {
                        RenamedSymbol = RenameExpression.Invoke(typeName),
                        Metadata = metadata,
                    };
                }
                else
                {
                    var singlelineMatch = typedefSinglelineRegex.Match(line);
                    if (singlelineMatch.Success)
                    {
                        //singlelineMatch.Dump();
                        var typeName = singlelineMatch.Groups["type"].Value;
                        return new SymbolInfo(line, DetectionType.Typedef, delimiter, typeName)
                        {
                            RenamedSymbol = RenameExpression.Invoke(typeName),
                            Metadata = metadata,
                        };
                    }
                    else
                    {
                        return null;
                    }
                }
            })
            .Where(x => x != null)
            .OrderByDescending(x => x?.Symbol.Length)
            .ToArray();

        return typedefs;
    }
}
