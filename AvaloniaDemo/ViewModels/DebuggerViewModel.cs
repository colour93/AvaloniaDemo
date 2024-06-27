using System;
using System.ComponentModel;
using Avalonia.Media.Imaging;
using AvaloniaDemo.Typings;
using CommunityToolkit.Mvvm.ComponentModel;

namespace AvaloniaDemo.ViewModels;

public partial class DebuggerViewModel : ObservableObject
{
    public class DebuggerResult
    {
        public Bitmap? MainContentImage { get; set; }
        public string? MainContentText { get; set; }
        public Bitmap? RoleNameImage { get; set; }
        public string? RoleNameText { get; set; }

        public string? Response { get; set; }
        public string? ResultText { get; set; }
    }

    [ObservableProperty] private DebuggerResult _result;

    public OpenAiConfig OpenAIConfig { get; set; }
    public OcrAreaConfig MainContentOCRConfig { get; set; }
    public OcrAreaConfig? RoleNameOCRConfig { get; set; }

    public DebuggerViewModel(OpenAiConfig openaiConfig, OcrAreaConfig mainContentOcrConfig,
        OcrAreaConfig? roleNameOcrConfig)
    {
        Result = new DebuggerResult();
        OpenAIConfig = openaiConfig;
        MainContentOCRConfig = mainContentOcrConfig;
        RoleNameOCRConfig = roleNameOcrConfig;
    }
}