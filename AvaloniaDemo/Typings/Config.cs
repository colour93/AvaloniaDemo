using Newtonsoft.Json;

namespace AvaloniaDemo.Typings;

public class AreaConfig
{
    public OcrAreaConfig MainContent { get; set; }
    public OcrAreaConfig RoleName { get; set; }
}

public class OpenAiConfig
{
    public string BaseURL { get; set; }
    public string APIKey { get; set; }
    public string Model { get; set; }
    public double Temperature { get; set; }
    public string Prompt { get; set; }
}