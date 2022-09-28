namespace SymbolConverter;

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
                    // TODO: 重複したシンボルで多重書き換えが起こる
                    // TODO: 短いシンボルが、長いシンボルに含まれているときに多重で書き換えが起こる
                    current = current.Replace(from, to);
                }
            }
        }
        return current;
    }
}
