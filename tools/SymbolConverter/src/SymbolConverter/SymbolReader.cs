using System.Text.RegularExpressions;
using System.Text;

namespace SymbolConverter;

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
        var methodRegex = new Regex($@"\b(?<type>\w+?)\s+(?<method>\w+?)\{delimiter}.*$", RegexOptions.Compiled);
        //content.Dump(path);

        // TODO: static inline は除外する
        // TODO: インラインコメントは除外する
        var methodLines = ExtractMethodLines(content);
        var methods = methodLines
            .Select(x => x.TrimStart())
            .Select(x =>
            {
                var match = methodRegex.Match(x);
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
        var typedefSinglelineRegex = new Regex($@"\btypedef\s+\w+\s+(?<type>\w+){delimiter}", RegexOptions.Compiled);
        // `} foo;`
        var typedefMultilineRegex = new Regex($@"\s*}}\s+(?<type>\w+){delimiter}", RegexOptions.Compiled);

        // Get typedef line in single string
        var typedefLines = ExtractTypedefLines(content);
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

    private IReadOnlyList<string> ExtractMethodLines(string[] content)
    {
        var defineStartRegex = new Regex($@"\w*#\s*define\s+", RegexOptions.Compiled);
        var defineContinueRegex = new Regex($@".*\\$", RegexOptions.Compiled);

        static bool IsEmptyLine(string str) => string.IsNullOrWhiteSpace(str);
        static bool IsCommentLine(string str) => str.StartsWith("//") || str.StartsWith("/*") || str.StartsWith("*/") || str.StartsWith("*");
        static bool IsPragmaLine(string str) => str.StartsWith("#");

        var methodLines = new List<string>();
        for (var i = 0; i < content.Length; i++)
        {
            var line = content[i].TrimStart();
            if (IsEmptyLine(line)) continue;
            if (IsCommentLine(line)) continue;

            var sb = new StringBuilder();
            if (defineStartRegex.IsMatch(line))
            {
                //sb.AppendLine(line);

                var continued = false;
                while (++i <= content.Length - 1 && defineContinueRegex.IsMatch(content[i]))
                {
                    //sb.AppendLine(content[i]);
                    continued = true;
                }

                if (continued)
                {
                    //sb.AppendLine(content[i]);
                }
            }
            else if (IsPragmaLine(line))
            {
                continue;
            }
            else
            {
                sb.AppendLine(line);
            }

            if (sb.Length > 0)
            {
                methodLines.Add(sb.ToString());
            }
        }

        return methodLines;
    }

    private IReadOnlyList<string> ExtractTypedefLines(string[] content)
    {
        const string delimiter = ";";
        // `typedef uint64_t foo;`
        // `typedef enum {
        // } foo;`
        var typedefStartRegex = new Regex(@"^\s*typedef.*", RegexOptions.Compiled);
        // `typedef <any>;` or `} foo;`
        var typedefEndRegex = new Regex($@"(\btypedef.*|}}\s+\w+){delimiter}", RegexOptions.Compiled);

        var typedefLines = new List<string>();
        for (var i = 0; i < content.Length; i++)
        {
            var line = content[i];
            var sb = new StringBuilder();
            if (typedefStartRegex.IsMatch(line))
            {
                // add first line
                sb.AppendLine(line);

                // is typedef single line?
                if (!typedefEndRegex.IsMatch(line))
                {
                    // typedef is multiline

                    // add typedef element lines
                    // stop when semi-colon not found, it is invalid typedef.
                    // stop when new typedef line found, it means invalid typedef found.
                    // stop when typedef last line found.
                    var j = i;
                    while (++j <= content.Length - 1 && !typedefStartRegex.IsMatch(content[j]) && !typedefEndRegex.IsMatch(content[j]))
                    {
                        // {
                        // void* key;
                        // mbedtls_pk_rsa_alt_decrypt_func decrypt_func;
                        // mbedtls_pk_rsa_alt_sign_func sign_func;
                        // mbedtls_pk_rsa_alt_key_len_func key_len_func;
                        sb.AppendLine(content[j]);
                    }

                    // add last line
                    if (j <= content.Length - 1 && typedefEndRegex.IsMatch(content[j]))
                    {
                        // } mbedtls_rsa_alt_context;
                        sb.AppendLine(content[j]);
                    }
                    else
                    {
                        // it is invalid typedef, clear it.
                        sb.Clear();
                    }
                }

                if (sb.Length > 0)
                {
                    typedefLines.Add(sb.ToString());
                }
            }
        }

        return typedefLines;
    }
}
