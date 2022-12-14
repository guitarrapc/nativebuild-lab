using System.Text.RegularExpressions;
using System.Text;

namespace SymbolConverter;

public record SymbolReaderOption(bool DistinctSymbol = false, bool Sort = false)
{
    /// <summary>
    /// Macro identify regex. Value must contain name parameter like '(?<name>foo)'
    /// </summary>
    public IReadOnlyList<string>? MacroRegexes { get; init; } = Environment.GetEnvironmentVariable("SYMBOL_CONVERTER_MACRO_REGEXES")?.Split(",") ?? null;

    public void Validate()
    {
        if (MacroRegexes is not null)
        {
            foreach (var regex in MacroRegexes)
            {
                if (!regex.Contains("(?<name>"))
                {
                    throw new ArgumentException($"MacroRegexes must contains parameter group 'name', like '(?<name>foobar)' . But '{regex}' doesn't contain parameter.");
                }
            }
        }
    }
};

public class SymbolReader
{
    private readonly SymbolReaderOption _option;

    public SymbolReader() : this(option: new SymbolReaderOption())
    {
    }

    public SymbolReader(SymbolReaderOption option)
    {
        _option = option;
    }

    public IReadOnlyList<SymbolInfo?> Read(DetectionType detectionType, string[] content, Func<string, string> RenameExpression, string? file = null)
    {
        var metadata = new Dictionary<string, string>
        {
            { "file", file ?? "" },
        };
        return detectionType switch
        {
            DetectionType.ExternField => ReadExternFieldInfo(content, RenameExpression, metadata, _option),
            DetectionType.Method => ReadMethodInfo(content, RenameExpression, metadata, _option),
            DetectionType.Typedef => ReadTypedefInfo(content, RenameExpression, metadata, _option),
            DetectionType.Macro => ReadMacroInfo(content, RenameExpression, metadata, _option),
            _ => throw new NotSupportedException(),
        };
    }

    private IReadOnlyList<SymbolInfo?> ReadExternFieldInfo(string[] content, Func<string, string> RenameExpression, IReadOnlyDictionary<string, string> metadata, SymbolReaderOption option)
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
                    return new SymbolInfo(line, DetectionType.ExternField, SymbolDelimiters.ExternDelimiters, name)
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
                    return new SymbolInfo(line, DetectionType.ExternField, SymbolDelimiters.ExternDelimiters, name)
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
            .Where(x => x != null);

        if (option.DistinctSymbol)
        {
            symbols = symbols.DistinctBy(x => x!.Symbol);
        }

        if (option.Sort)
        {
            symbols = symbols.OrderByDescending(x => x!.Symbol.Length);
        }

        return symbols.ToArray();
    }

    private IReadOnlyList<SymbolInfo?> ReadMethodInfo(string[] content, Func<string, string> RenameExpression, IReadOnlyDictionary<string, string> metadata, SymbolReaderOption option)
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
                    return new SymbolInfo(line, DetectionType.Method, SymbolDelimiters.MethodDelimiters, name)
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
            .Where(x => x != null);

        if (option.DistinctSymbol)
        {
            symbols = symbols.DistinctBy(x => x!.Symbol);
        }

        if (option.Sort)
        {
            symbols = symbols.OrderByDescending(x => x!.Symbol.Length);
        }

        return symbols.ToArray();
    }

    private IReadOnlyList<SymbolInfo?> ReadTypedefInfo(string[] content, Func<string, string> RenameExpression, IReadOnlyDictionary<string, string> metadata, SymbolReaderOption option)
    {
        const string delimiter = ";";
        // `typedef uint64_t foo;`
        // `typedef struct uint64 bar;`
        var typedefSinglelineRegex = new Regex($@"\btypedef(\s+\w+)?\s+(?<type>\w+)\s+(?<name>\w+){delimiter}", RegexOptions.Compiled);
        // `} foo;`
        var typedefMultilineRegex = new Regex($@"\s*}}\s+(?<name>\w+){delimiter}", RegexOptions.Compiled);
        // `typedef int (*ptr_name)( void *p_ctx,`
        var typedefPtrlineRegex = new Regex($@"\btypedef\s+(?<type>\w+)\s+\(\*?(?<name>\w+)\)\(.*,", RegexOptions.Compiled);

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
                    return new SymbolInfo(line, DetectionType.Typedef, SymbolDelimiters.TypedefDelimiters, name)
                    {
                        RenamedSymbol = RenameExpression.Invoke(name),
                        Metadata = new Dictionary<string, string>(metadata)
                        {
                            {"ReturnType", name}, // Alias Type
                        },
                    };
                }

                var singlelineMatch = typedefSinglelineRegex.Match(line);
                if (singlelineMatch.Success)
                {
                    //singlelineMatch.Dump();
                    var name = singlelineMatch.Groups["name"].Value;
                    return new SymbolInfo(line, DetectionType.Typedef, SymbolDelimiters.TypedefDelimiters, name)
                    {
                        RenamedSymbol = RenameExpression.Invoke(name),
                        Metadata = new Dictionary<string, string>(metadata)
                        {
                            {"ReturnType", name}, // Alias Type
                        },
                    };
                }

                var ptrlineMatch = typedefPtrlineRegex.Match(line);
                if (ptrlineMatch.Success)
                {
                    //ptrlineMatch.Dump();
                    var name = ptrlineMatch.Groups["name"].Value;
                    return new SymbolInfo(line, DetectionType.Typedef, SymbolDelimiters.TypedefDelimiters, name)
                    {
                        RenamedSymbol = RenameExpression.Invoke(name),
                        Metadata = new Dictionary<string, string>(metadata)
                        {
                            {"ReturnType", name}, // Alias Type
                        },
                    };
                }

                return null;
            })
            .Where(x => x != null);

        if (option.DistinctSymbol)
        {
            symbols = symbols.DistinctBy(x => x!.Symbol);
        }

        if (option.Sort)
        {
            symbols = symbols.OrderByDescending(x => x!.Symbol.Length);
        }

        return symbols.ToArray();
    }

    private IReadOnlyList<SymbolInfo?> ReadMacroInfo(string[] content, Func<string, string> RenameExpression, IReadOnlyDictionary<string, string> metadata, SymbolReaderOption option)
    {
        if (_option.MacroRegexes is null || !_option.MacroRegexes.Any())
        {
            return Array.Empty<SymbolInfo>();
        }

        var regexes = _option.MacroRegexes.Select(x => new Regex(x, RegexOptions.Compiled)).ToArray();

        // Get typedef line in single string
        var lines = ExtractMacroLines(content);
        var symbols = lines
            .Select(x => x.TrimStart())
            .SelectMany(line =>
            {
                return regexes.Select(x =>
                {
                    var match = x.Match(line);
                    if (match.Success)
                    {
                        var name = match.Groups["name"].Value;
                        return new SymbolInfo(line, DetectionType.Macro, SymbolDelimiters.MacroDelimiters, name)
                        {
                            RenamedSymbol = RenameExpression.Invoke(name),
                            Metadata = new Dictionary<string, string>(metadata)
                            {
                                {"ReturnType", name}, // Alias Type
                            },
                        };
                    }
                    return null;
                })
                .ToArray();
            })
            .Where(x => x != null);

        if (option.DistinctSymbol)
        {
            symbols = symbols.DistinctBy(x => x!.Symbol);
        }

        if (option.Sort)
        {
            symbols = symbols.OrderByDescending(x => x!.Symbol.Length);
        }

        return symbols.ToArray();
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

        var lines = new List<string>();
        for (var i = 0; i < content.Length; i++)
        {
            if (IsEmptyLine(content[i])) continue;
            if (IsCommentLine(content[i])) continue;
            if (IsPragmaLine(content[i])) continue;

            var sb = new StringBuilder();
            if (externStartRegex.IsMatch(content[i]))
            {
                // add first line
                sb.AppendLine(content[i]);

                // is single line?
                if (!externEndRegex.IsMatch(content[i]))
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
        var parenthesisStartRegex = new Regex(@"^\s*(\w+\s+)?{", RegexOptions.Compiled);
        var parenthesisEndRegex = new Regex(@"^\s*}", RegexOptions.Compiled);

        // `foo bar(`
        // ` foo bar(  foo`
        var methodStartRegex = new Regex($@"^(\s|\w)*(?<type>\w+?)\s+(?<method>\w+?)\(.*$", RegexOptions.Compiled);
        // `<any>)`
        var methodEndRegex = new Regex($@".*\)", RegexOptions.Compiled);

        var defineStartRegex = new Regex(@"\w*#\s*define\s+", RegexOptions.Compiled);
        var defineContinueRegex = new Regex(@".*\\$", RegexOptions.Compiled);
        var staticInlineStartRegex = new Regex(@"\s*static\s+inline\s+\w+\s+\w+", RegexOptions.Compiled);
        var structStartRegex = new Regex(@"\s*struct\s+\.*", RegexOptions.Compiled);

        var typedefStartRegex = new Regex(@"^\s*typedef.*", RegexOptions.Compiled);
        var typedefEndRegex = new Regex(@"(\btypedef.*|}\s+\w+);", RegexOptions.Compiled);
        var typedefEndExceptionalRegex = new Regex(@"\s*;", RegexOptions.Compiled);
        var typedefContainsParethesis = new Regex(@"^\s*typedef\s+\w+\s*{", RegexOptions.Compiled);

        var lines = new List<string>();
        for (var i = 0; i < content.Length; i++)
        {
            if (IsEmptyLine(content[i].TrimStart())) continue;
            if (IsCommentLine(content[i].TrimStart())) continue;

            // skip "static inline" block
            if (staticInlineStartRegex.IsMatch(content[i]))
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
                    if (parenthesisEndRegex.IsMatch(content[i]))
                    {
                        rest--;
                        complete = rest == 0;
                    }
                    continue;
                }
                continue;
            }

            // skip typedef block
            if (typedefStartRegex.IsMatch(content[i]))
            {
                var j = i;
                if (typedefEndRegex.IsMatch(content[i]))
                {
                    // sigle line
                    continue;
                }
                else
                {
                    // multi line
                    var complete = false;
                    var invalid = false;
                    var rest = typedefContainsParethesis.IsMatch(content[i]) ? 1 : 0;
                    while (++j <= content.Length - 1 && !complete)
                    {
                        var current = content[j];
                        if (typedefStartRegex.IsMatch(current))
                        {
                            // not completed but new typedef line started.
                            --j; // reverse index to previous (before increment on while)
                            invalid = true;
                            break;
                        }
                        if (parenthesisStartRegex.IsMatch(current))
                        {
                            if (rest == 0)
                            {
                                // add `{`
                            }
                            rest++;
                        }
                        if (parenthesisEndRegex.IsMatch(current))
                        {
                            rest--;
                            if (rest == 0)
                            {
                                // add `} foo_t;`
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
                                break;
                            }
                            else if (typedefEndExceptionalRegex.IsMatch(current))
                            {
                                // add `;`
                                complete = true;
                                break;
                            }
                        }
                    }

                    if (!invalid)
                    {
                        i = j;
                    }
                }
            }

            // skip "struct" block
            if (structStartRegex.IsMatch(content[i]))
            {
                var complete = false;
                var rest = 0;
                while (++i <= content.Length - 1 && !complete)
                {
                    // find parenthesis pair which close static inline method.
                    if (parenthesisStartRegex.IsMatch(content[i]))
                    {
                        rest++;
                    }
                    if (parenthesisEndRegex.IsMatch(content[i]))
                    {
                        rest--;
                        complete = rest == 0;
                    }
                    continue;
                }
                continue;
            }

            // skip "#define" block
            if (defineStartRegex.IsMatch(content[i]))
            {
                while (++i <= content.Length - 1 && defineContinueRegex.IsMatch(content[i]))
                {
                }
                continue;
            }

            if (IsPragmaLine(content[i])) continue;

            var sb = new StringBuilder();
            if (methodStartRegex.IsMatch(content[i]))
            {
                // add first line
                sb.AppendLine(content[i]);

                // is single line?
                if (!methodEndRegex.IsMatch(content[i]))
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
        var parenthesisStartRegex = new Regex(@"^\s*(\w+\s+)?{", RegexOptions.Compiled);
        var parenthesisEndRegex = new Regex(@"^\s*}", RegexOptions.Compiled);

        var typedefLines = new List<string>();
        for (var i = 0; i < content.Length; i++)
        {
            if (IsEmptyLine(content[i])) continue;
            if (IsCommentLine(content[i])) continue;

            var sb = new StringBuilder();
            if (typedefStartRegex.IsMatch(content[i]))
            {
                // add first line
                sb.AppendLine(content[i]);

                // is single line?
                if (!typedefEndRegex.IsMatch(content[i]))
                {
                    // is multiline

                    // find `{` and `;` to complete typedef.
                    var complete = false;
                    var invalid = false;
                    var rest = typedefContainsParethesis.IsMatch(content[i]) ? 1 : 0;
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

    private static IReadOnlyList<string> ExtractMacroLines(string[] content)
    {
        var parenthesisStartRegex = new Regex(@"^\s*{", RegexOptions.Compiled);
        var parenthesisEndRegex = new Regex(@"^\s*}", RegexOptions.Compiled);

        var defineStartRegex = new Regex(@"\w*#\s*define\s+", RegexOptions.Compiled);
        var defineContinueRegex = new Regex(@".*\\$", RegexOptions.Compiled);

        static bool IsEmptyLine(string str) => string.IsNullOrWhiteSpace(str);
        static bool IsCommentLine(string str) => str.StartsWith("//") || str.StartsWith("/*") || str.StartsWith("*/") || str.StartsWith("*");

        var lines = new List<string>();
        for (var i = 0; i < content.Length; i++)
        {
            var line = content[i].TrimStart();
            if (IsEmptyLine(line)) continue;
            if (IsCommentLine(line)) continue;

            var sb = new StringBuilder();
            if (defineStartRegex.IsMatch(line))
            {
                // add first line
                sb.AppendLine(line);

                while (++i <= content.Length - 1 && defineContinueRegex.IsMatch(content[i]))
                {
                    sb.AppendLine(content[i]);
                }
                lines.Add(sb.ToString());
            }
        }

        return lines;
    }

    private static bool IsEmptyLine(string str) => string.IsNullOrWhiteSpace(str.TrimStart());
    private static bool IsCommentLine(string str)
    {
        var s = str.TrimStart();
        return s.StartsWith("//") || s.StartsWith("/*") || s.StartsWith("*/") || s.StartsWith("*");
    }
    private static bool IsPragmaLine(string str) => str.TrimStart().StartsWith("#");
}

