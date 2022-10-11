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
    public static readonly IReadOnlyList<string> MethodDelimiters = new[] { "(", " ", ")", "," };
    public static readonly IReadOnlyList<string> TypedefDelimiters = new[] { ";", " " };
    public static readonly IReadOnlyList<string> ExternDelimiters = new[] { ";", " " };
}
