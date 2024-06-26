using Newtonsoft.Json;

namespace AvaloniaDemo.Typings;

public class AreaConfig
{
    public OCRAreaConfig MainContent { get; set; }
    public OCRAreaConfig RoleName { get; set; }
}

public class OpenAIConfig
{
    public string BaseURL { get; set; }
    public string APIKey { get; set; }
}