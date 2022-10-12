namespace SymbolConverter;

public static class SymbolDelimiters
{
    public static readonly IReadOnlyList<string> MethodDelimiters = new[]
    {
        "(", // method
        " ",
        ")",
        ",",
        "}", // mapping ... {bar, foo_bar}
        ";", // delegate ... foo->bar_init = piyo_bar_init;
        "\n", "\r\n", // new line after ... #define foo_c      foo
    };

    public static readonly IReadOnlyList<string> TypedefDelimiters = new[]
    {
        ";",
        " ",
        ")", // inside sizeof ... sizeof(foo)
        "*", // ptr ... (foo*)
        "\n", "\r\n", // new line after ... struct foo\n{
        ",", // parameter type ... FOO(bar, hoge)
    };

    public static readonly IReadOnlyList<string> ExternDelimiters = new[]
    {
        ";",
        " ",
        "(", // method
        ")", // prt ... (*foo)
        ",", // use in parameter ... foo,
        "\"", // dlsym call ... dlsym( bar_so, "foo" )
        ".", // field access .. foo.prop = val;
    };

    public static readonly IReadOnlyList<string> MacroDelimiters = new[]
    {
        "",
    };
}
