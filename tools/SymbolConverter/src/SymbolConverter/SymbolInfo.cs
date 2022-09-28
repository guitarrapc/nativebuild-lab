namespace SymbolConverter;

public record SymbolInfo(string Line, DetectionType DetectionType, string Delimiter, string Symbol)
{
    public string? RenamedSymbol { get; set; }
    public IReadOnlyDictionary<string, string>? Metadata { get; set; }

    public string GetSourceFile()
    {
        if (Metadata is null) return "";
        return Metadata["file"];
    }
};
