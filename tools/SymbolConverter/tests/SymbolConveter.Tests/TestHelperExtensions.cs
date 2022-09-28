namespace SymbolConveter.Tests;
internal static class TestHelperExtensions
{
    /// <summary>
    /// Split Newline on both Windows/Linux environment.
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    public static string[] SplitNewLine(this string value) => value.Replace("\r\n", "\n").Split("\n");
}
