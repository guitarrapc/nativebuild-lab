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
            DetectionType.ExternField => ReadExternFieldInfo(content, RenameExpression, metadata),
            DetectionType.Method => ReadMethodInfo(content, RenameExpression, metadata),
            DetectionType.Typedef => ReadTypedefInfo(content, RenameExpression, metadata),
            _ => throw new NotSupportedException(),
        };
    }

    private IReadOnlyList<SymbolInfo?> ReadExternFieldInfo(string[] content, Func<string, string> RenameExpression, IReadOnlyDictionary<string, string> metadata)
    {
        const string delimiter = ";";
        // `extern int foo;`
        // `extern const foo_t foo;`
        var fieldRegex = new Regex($@"^\s*extern\s+(const\s+)?(?<type>\w+)(\*)?\s+(?<name>\w+)\s*{delimiter}\s*$", RegexOptions.Compiled);
        // `extern void (*foo)( p a );`
        // `extern void* (foo)( );`
        var fieldfunctionRegex = new Regex($@"^\s*extern\s+(?<type>\w+)(\*)?\s+\(\s*\*?(?<name>\w+)\s*\).*{delimiter}\s*$", RegexOptions.Compiled);

        var lines = ExtractExternFieldLines(content);
        var symbols = lines
            .Select(x => x.TrimStart())
            .Select(x =>
            {
                var matchFunctionPtr = fieldfunctionRegex.Match(x);
                if (matchFunctionPtr.Success)
                {
                    //match.Dump();
                    var line = matchFunctionPtr.Groups[0].Value;
                    var type = matchFunctionPtr.Groups["type"].Value;
                    var name = matchFunctionPtr.Groups["name"].Value;
                    return new SymbolInfo(line, DetectionType.ExternField, delimiter, name)
                    {
                        RenamedSymbol = RenameExpression.Invoke(name),
                        Metadata = new Dictionary<string, string>(metadata)
                        {
                            {"ReturnType", type},
                        },
                    };
                }

                var matchField = fieldRegex.Match(x);
                if (matchField.Success)
                {
                    //match.Dump();
                    var line = matchField.Groups[0].Value;
                    var type = matchField.Groups["type"].Value;
                    var name = matchField.Groups["name"].Value;
                    return new SymbolInfo(line, DetectionType.ExternField, delimiter, name)
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

    private IReadOnlyList<SymbolInfo?> ReadMethodInfo(string[] content, Func<string, string> RenameExpression, IReadOnlyDictionary<string, string> metadata)
    {
        const string delimiter = "(";
        // `foo bar(`
        // ` foo bar(  foo`
        var methodRegex = new Regex($@"(?<pre>.*\s)?\s?(?<type>\w+)\s+(?<method>\w+)\{delimiter}(?<post>.*)", RegexOptions.Compiled | RegexOptions.Multiline);
        //content.Dump(path);

        var lines = ExtractMethodLines(content);
        var symbols = lines
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
        var lines = ExtractTypedefLines(content);
        var symbols = lines
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

    private static IReadOnlyList<string> ExtractExternFieldLines(string[] content)
    {
        const string delimiter = ";";
        // `extern const int foo;`
        // `extern void* (function_pointer)();`
        // `extern foo
        // (*piyo)(int x0, int x1);`
        var externStartRegex = new Regex(@"^\s*extern.*", RegexOptions.Compiled);
        // `typedef <any>;` or `} foo;`
        var externEndRegex = new Regex($@".*{delimiter}", RegexOptions.Compiled);

        static bool IsEmptyLine(string str) => string.IsNullOrWhiteSpace(str);
        static bool IsCommentLine(string str) => str.StartsWith("//") || str.StartsWith("/*") || str.StartsWith("*/") || str.StartsWith("*");
        static bool IsPragmaLine(string str) => str.StartsWith("#");

        var lines = new List<string>();
        for (var i = 0; i < content.Length; i++)
        {
            var line = content[i].TrimStart();
            if (IsEmptyLine(line)) continue;
            if (IsCommentLine(line)) continue;
            if (IsPragmaLine(line)) continue;

            var sb = new StringBuilder();
            if (externStartRegex.IsMatch(line))
            {
                // add first line
                sb.AppendLine(line);

                // is single line?
                if (!externEndRegex.IsMatch(line))
                {
                    // is multiline

                    // add extern element lines
                    // stop when semi-colon not found, it is invalid.
                    // stop when new extern line found, it means invalid.
                    // stop when extern last line found.
                    var j = i;
                    while (++j <= content.Length - 1 && !externStartRegex.IsMatch(content[j]) && !externEndRegex.IsMatch(content[j]))
                    {
                        // {
                        // void* key;
                        // mbedtls_pk_rsa_alt_decrypt_func decrypt_func;
                        // mbedtls_pk_rsa_alt_sign_func sign_func;
                        // mbedtls_pk_rsa_alt_key_len_func key_len_func;
                        sb.AppendLine(content[j]);
                    }

                    // add last line
                    if (j <= content.Length - 1 && externEndRegex.IsMatch(content[j]))
                    {
                        // } mbedtls_rsa_alt_context;
                        sb.AppendLine(content[j]);
                    }
                    else
                    {
                        // it is invalid, clear it.
                        sb.Clear();
                    }
                }

                if (sb.Length > 0)
                {
                    lines.Add(sb.ToString());
                }
            }
        }

        return lines;
    }

    private static IReadOnlyList<string> ExtractMethodLines(string[] content)
    {
        var parenthesisStartRegex = new Regex(@"^\s*{", RegexOptions.Compiled);

        // `foo bar(`
        // ` foo bar(  foo`
        var methodStartRegex = new Regex($@"^(\s|\w)*(?<type>\w+?)\s+(?<method>\w+?)\(.*$", RegexOptions.Compiled);
        // `<any>)`
        var methodEndRegex = new Regex($@".*\)", RegexOptions.Compiled);

        var defineStartRegex = new Regex(@"\w*#\s*define\s+", RegexOptions.Compiled);
        var defineContinueRegex = new Regex(@".*\\$", RegexOptions.Compiled);
        var staticInlineStartRegex = new Regex(@"\s*static\s+inline\s+\w+\s+\w+", RegexOptions.Compiled);
        var staticInlineEndRegex = new Regex(@"^\s*}", RegexOptions.Compiled);

        static bool IsEmptyLine(string str) => string.IsNullOrWhiteSpace(str);
        static bool IsCommentLine(string str) => str.StartsWith("//") || str.StartsWith("/*") || str.StartsWith("*/") || str.StartsWith("*");
        static bool IsPragmaLine(string str) => str.StartsWith("#");

        var lines = new List<string>();
        for (var i = 0; i < content.Length; i++)
        {
            var line = content[i].TrimStart();
            if (IsEmptyLine(line)) continue;
            if (IsCommentLine(line)) continue;

            // skip "static inline" block
            if (staticInlineStartRegex.IsMatch(line))
            {
                var complete = false;
                var rest = 0;
                while (++i <= content.Length - 1 && !complete)
                {
                    // find parenthesis pair which close static inline method.
                    var current = content[i];
                    if (parenthesisStartRegex.IsMatch(content[i]))
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

            // skip "#define" block
            if (defineStartRegex.IsMatch(line))
            {
                while (++i <= content.Length - 1 && defineContinueRegex.IsMatch(content[i]))
                {
                }
                continue;
            }

            if (IsPragmaLine(line)) continue;

            var sb = new StringBuilder();
            if (methodStartRegex.IsMatch(line))
            {
                // add first line
                sb.AppendLine(line);

                // is single line?
                if (!methodEndRegex.IsMatch(line))
                {
                    // is multiline

                    // add method element lines
                    // stop when ) not found, it is invalid.
                    // stop when new method line found, it means invalid.
                    // stop when method last line found.
                    var j = i;
                    while (++j <= content.Length - 1 && !methodStartRegex.IsMatch(content[j]) && !methodEndRegex.IsMatch(content[j]))
                    {
                        sb.AppendLine(content[j]);
                    }

                    // add last line
                    if (j <= content.Length - 1 && methodEndRegex.IsMatch(content[j]))
                    {
                        sb.AppendLine(content[j]);
                    }
                    else
                    {
                        // it is invalid, clear it.
                        sb.Clear();
                    }
                }

                if (sb.Length > 0)
                {
                    lines.Add(sb.ToString());
                }
            }
        }

        return lines;
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
        var typedefEndExceptionalRegex = new Regex($@"\s*{delimiter}", RegexOptions.Compiled);

        var typedefContainsParethesis = new Regex(@"^\s*typedef\s+\w+\s*{", RegexOptions.Compiled);
        var parenthesisStartRegex = new Regex(@"^\s*{", RegexOptions.Compiled);
        var parenthesisEndRegex = new Regex(@"^\s*}", RegexOptions.Compiled);

        static bool IsEmptyLine(string str) => string.IsNullOrWhiteSpace(str);
        static bool IsCommentLine(string str) => str.StartsWith("//") || str.StartsWith("/*") || str.StartsWith("*/") || str.StartsWith("*");

        var typedefLines = new List<string>();
        for (var i = 0; i < content.Length; i++)
        {
            var line = content[i];
            if (IsEmptyLine(line)) continue;
            if (IsCommentLine(line)) continue;

            var sb = new StringBuilder();
            if (typedefStartRegex.IsMatch(line))
            {
                // add first line
                sb.AppendLine(line);

                // is single line?
                if (!typedefEndRegex.IsMatch(line))
                {
                    // is multiline

                    // find `{` and `;` to complete typedef.
                    var complete = false;
                    var invalid = false;
                    var rest = typedefContainsParethesis.IsMatch(line) ? 1 : 0;
                    while (++i <= content.Length - 1 && !complete)
                    {
                        var current = content[i];
                        if (typedefStartRegex.IsMatch(current))
                        {
                            // not completed but new typedef line started.
                            --i; // reverse index to previous (before increment on while)
                            invalid = true;
                            break;
                        }
                        if (parenthesisStartRegex.IsMatch(current))
                        {
                            if (rest == 0)
                            {
                                // add `{`
                                sb.AppendLine(current);
                            }
                            rest++;
                        }
                        if (parenthesisEndRegex.IsMatch(current))
                        {
                            rest--;
                            if (rest == 0)
                            {
                                // add `} foo_t;`
                                sb.AppendLine(current);
                            }

                            if (rest < 0)
                            {
                                // missing { but } reached.
                                invalid = true;
                                break;
                            }
                        }

                        if (rest == 0)
                        {
                            if (typedefEndRegex.IsMatch(current))
                            {
                                complete = true;
                            }
                            else if (typedefEndExceptionalRegex.IsMatch(current))
                            {
                                // add `;`
                                sb.AppendLine(current);
                                complete = true;
                            }
                        }

                        continue;
                    }

                    // invalid data, clear it.
                    if (invalid)
                    {
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

