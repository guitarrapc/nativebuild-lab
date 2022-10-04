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

        var methodLines = ExtractMethodLines(content);
        var symbols = methodLines
            .Select(x => x.TrimStart())
            .Select(x =>
            {
                var match = methodRegex.Match(x);
                if (match.Success)
                {
                    //match.Dump();
                    var line = match.Groups[0].Value;
                    var type = match.Groups["type"].Value;
                    var name = match.Groups["method"].Value;
                    return new SymbolInfo(line, DetectionType.Method, delimiter, name)
                    {
                        RenamedSymbol = RenameExpression.Invoke(name),
                        Metadata = new Dictionary<string, string>(metadata)
                        {
                            {"ReturnType", type},
                        },
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

        return symbols;
    }

    private IReadOnlyList<SymbolInfo?> ReadTypedefInfo(string[] content, Func<string, string> RenameExpression, IReadOnlyDictionary<string, string> metadata)
    {
        const string delimiter = ";";
        // `typedef uint64_t foo;`
        var typedefSinglelineRegex = new Regex($@"\btypedef\s+(?<type>\w+)\s+(?<name>\w+){delimiter}", RegexOptions.Compiled);
        // `} foo;`
        var typedefMultilineRegex = new Regex($@"\s*}}\s+(?<name>\w+){delimiter}", RegexOptions.Compiled);

        // Get typedef line in single string
        var typedefLines = ExtractTypedefLines(content);
        var symbols = typedefLines
            .Select(x => x.TrimStart())
            .Select(line =>
            {
                var multilineMatch = typedefMultilineRegex.Match(line);
                if (multilineMatch.Success)
                {
                    //multilineMatch.Dump();
                    var name = multilineMatch.Groups["name"].Value;
                    return new SymbolInfo(line, DetectionType.Typedef, delimiter, name)
                    {
                        RenamedSymbol = RenameExpression.Invoke(name),
                        Metadata = new Dictionary<string, string>(metadata)
                        {
                            {"ReturnType", name}, // Alias Type
                        },
                    };
                }
                else
                {
                    var singlelineMatch = typedefSinglelineRegex.Match(line);
                    if (singlelineMatch.Success)
                    {
                        //singlelineMatch.Dump();
                        var name = singlelineMatch.Groups["name"].Value;
                        return new SymbolInfo(line, DetectionType.Typedef, delimiter, name)
                        {
                            RenamedSymbol = RenameExpression.Invoke(name),
                            Metadata = new Dictionary<string, string>(metadata)
                            {
                                {"ReturnType", name}, // Alias Type
                            },
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

        return symbols;
    }

    private static IReadOnlyList<string> ExtractMethodLines(string[] content)
    {
        var defineStartRegex = new Regex(@"\w*#\s*define\s+", RegexOptions.Compiled);
        var defineContinueRegex = new Regex(@".*\\$", RegexOptions.Compiled);

        var structStartRegex = new Regex(@"\s*struct\s*\w+", RegexOptions.Compiled);
        var structEndRegex = new Regex(@"\s*};\s*$", RegexOptions.Compiled);

        var staticInlineStartRegex = new Regex(@"\s*static\s+inline\s+\w+\s+\w+", RegexOptions.Compiled);
        var staticInlineParenthesisStartRegex = new Regex(@"^\s*{", RegexOptions.Compiled);
        var staticInlineEndRegex = new Regex(@"^\s*}", RegexOptions.Compiled);

        static bool IsEmptyLine(string str) => string.IsNullOrWhiteSpace(str);
        static bool IsCommentLine(string str) => str.StartsWith("//") || str.StartsWith("/*") || str.StartsWith("*/") || str.StartsWith("*");
        static bool IsPragmaLine(string str) => str.StartsWith("#");

        var methodLines = new List<string>();
        for (var i = 0; i < content.Length; i++)
        {
            var line = content[i].TrimStart();
            if (IsEmptyLine(line)) continue;
            if (IsCommentLine(line)) continue;

            // skip "#define" block
            if (defineStartRegex.IsMatch(line))
            {
                while (++i <= content.Length - 1 && defineContinueRegex.IsMatch(content[i]))
                {
                }
                continue;
            }

            if (IsPragmaLine(line))
            {
                continue;
            }

            // skip "struct" block
            if (structStartRegex.IsMatch(line))
            {
                while (++i <= content.Length - 1 && !structStartRegex.IsMatch(content[i]) && !structEndRegex.IsMatch(content[i]))
                {
                    continue;
                }
                if (i <= content.Length - 1 && structEndRegex.IsMatch(content[i]))
                {
                    continue;
                }
            }

            // skip "static inline" block
            if (staticInlineStartRegex.IsMatch(line))
            {
                var complete = false;
                var rest = 0;
                while (++i <= content.Length - 1 && !complete)
                {
                    // find parenthesis pair which close static inline method.
                    var current = content[i];
                    if (staticInlineParenthesisStartRegex.IsMatch(content[i]))
                    {
                        rest++;
                    }
                    if (staticInlineEndRegex.IsMatch(content[i]))
                    {
                        rest--;
                        complete = rest == 0;
                    }
                    continue;
                }
                continue;
            }

            methodLines.Add(content[i]);
        }

        return methodLines;
    }

    private static IReadOnlyList<string> ExtractTypedefLines(string[] content)
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
