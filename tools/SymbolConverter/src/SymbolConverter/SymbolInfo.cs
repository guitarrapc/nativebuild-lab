namespace SymbolConverter;

public record SymbolInfo(string Line, DetectionType DetectionType, IReadOnlyList<string> Delimiters, string Symbol)
{
    public string? RenamedSymbol { get; init; }
    public IReadOnlyDictionary<string, string>? Metadata { get; set; }

    public string GetSourceFile()
    {
        if (Metadata is null) return "";
        return Metadata["file"];
    }
};

public static class SymbolDelimiters
{
    public static readonly IReadOnlyList<string> MethodDelimiters = new[]
    {
        "(", // method
        " ",
        ")",
        ",",
        "}", // mapping ... {bar, foo_bar}
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
    };
}
