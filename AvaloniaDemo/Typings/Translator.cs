using System;

namespace AvaloniaDemo.Typings;

public class TranslatorContent
{
    public string Role { get; set; }
    public string Content { get; set; }
}

public class TranslatorContentH : TranslatorContent
{
    public string GetText()
    {
        return (Role != "system" || Role != "") ? $"[{Role}] {Content}" : $"{Content}";
    }
}