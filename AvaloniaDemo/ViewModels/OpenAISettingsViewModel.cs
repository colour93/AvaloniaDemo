using System;

namespace AvaloniaDemo.ViewModels
{
    public class OpenAISettings
    {
        public Uri BaseURL { get; set; }
        public string Key { get; set; }
        public string Model { get; set; }
        public decimal Temperature { get; set; }
        public string Prompt { get; set; }
    }

}
