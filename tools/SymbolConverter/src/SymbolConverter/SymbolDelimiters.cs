namespace SymbolConverter;

public static class SymbolDelimiters
{
    public static IReadOnlyList<string> MethodDelimiters => methodDelimiters ;
    private static List<string> methodDelimiters = new List<string>
    {
        "(", // method
        " ",
        ")",
        ",",
        "}", // mapping ... {bar, foo_bar}
        ";", // delegate ... foo->bar_init = piyo_bar_init;
        "\n", "\r\n", // new line after ... #define foo_c      foo
    };

    public static IReadOnlyList<string> TypedefDelimiters => typedefDelimiters;
    private static List<string> typedefDelimiters = new List<string>
    {
        ";",
        " ",
        ")", // inside sizeof ... sizeof(foo)
        "*", // ptr ... (foo*)
        "\n", "\r\n", // new line after ... struct foo\n{
        ",", // parameter type ... FOO(bar, hoge)
    };

    public static IReadOnlyList<string> ExternDelimiters => externDelimiters;
    private static List<string> externDelimiters = new List<string>
    {
        ";",
        " ",
        "(", // method
        ")", // prt ... (*foo)
        ",", // use in parameter ... foo,
        "\"", // dlsym call ... dlsym( bar_so, "foo" )
        ".", // field access .. foo.prop = val;
    };

    public static IReadOnlyList<string> MacroDelimiters => macroDelimiters;
    private static List<string> macroDelimiters = new List<string>
    {
        "",
    };

    public static void AddDelimiters(IEnumerable<string>? methodDelimiters, IEnumerable<string>? typedefDelimiters, IEnumerable<string>? externDelimiters, IEnumerable<string>? macroDelimiters)
    {
        if (methodDelimiters is not null)
        {
            AddMethodDelimiters(methodDelimiters);
        }

        if (typedefDelimiters is not null)
        {
            AddTypedefDelimiters(typedefDelimiters);
        }

        if (externDelimiters is not null)
        {
            AddExternDelimiters(externDelimiters);
        }

        if (macroDelimiters is not null)
        {
            AddMacroDelimiters(macroDelimiters);
        }
    }

    public static void AddMethodDelimiters(IEnumerable<string> delimiters) => methodDelimiters.AddRange(delimiters);
    public static void AddTypedefDelimiters(IEnumerable<string> delimiters) => typedefDelimiters.AddRange(delimiters);
    public static void AddExternDelimiters(IEnumerable<string> delimiters) => externDelimiters.AddRange(delimiters);
    public static void AddMacroDelimiters(IEnumerable<string> delimiters) => macroDelimiters.AddRange(delimiters);
}
