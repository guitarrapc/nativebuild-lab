public class SymbolWriter
{
    public string ReplaceSymbol(string content, IReadOnlyList<SymbolInfo?> symbols)
    {
        var current = content;
        foreach (var symbol in symbols)
        {
            if (symbol is not null)
            {
                var from = symbol.Symbol + symbol.Delimiter;
                var to = symbol.RenamedSymbol + symbol.Delimiter;
                if (current.Contains(from))
                {
                    current = current.Replace(from, to);
                }
            }
        }
        return current;
    }
}
