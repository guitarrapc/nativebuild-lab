public record SymbolInfo(string Line, DetectionType DetectionType, string Delimiter, string Symbol)
{
    public string? RenamedSymbol { get; set; }
    public Dictionary<string, string>? Metadata { get; set; }
};
