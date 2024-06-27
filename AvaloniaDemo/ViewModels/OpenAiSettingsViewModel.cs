using System;
using System.IO;
using Avalonia.Platform;
using AvaloniaDemo.Typings;
using ReactiveUI;

namespace AvaloniaDemo.ViewModels
{
    public class OpenAiSettingsViewModel : ReactiveObject
    {
        public OpenAiConfig Config { get; set; }

        public OpenAiSettingsViewModel(OpenAiConfig? config)
        {
            Config = (config == null || (config?.Prompt == null && config?.BaseURL == null && config?.Model == null &&
                                         config!.Temperature == 0 && config?.Prompt == null))
                ? new OpenAiConfig()
                {
                    BaseURL = "",
                    APIKey = "",
                    Model = "",
                    Temperature = 0.7,
                    Prompt =
                        (new StreamReader(AssetLoader.Open(new Uri("avares://AvaloniaDemo/Assets/init-prompt.md"))))
                        .ReadToEnd()
                }
                : config;
        }
    }
}