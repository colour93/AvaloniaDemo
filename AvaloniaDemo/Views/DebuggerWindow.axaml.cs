using Avalonia.Controls;
using Avalonia.Interactivity;
using AvaloniaDemo.Services;
using AvaloniaDemo.Typings;
using AvaloniaDemo.ViewModels;

namespace AvaloniaDemo.Views;

public partial class DebuggerWindow : Window
{
    private DebuggerViewModel _viewModel;

    public DebuggerWindow(OpenAiConfig? openaiConfig, OcrAreaConfig? mainContentOcrConfig,
        OcrAreaConfig? roleNameOcrConfig)
    {
        if (openaiConfig == null || mainContentOcrConfig == null)
        {
            Close();
            return;
        }

        _viewModel = new DebuggerViewModel(openaiConfig, mainContentOcrConfig, roleNameOcrConfig);
        DataContext = _viewModel;
        InitializeComponent();
    }

    private void ButtonExit_OnClick(object? sender, RoutedEventArgs e)
    {
        Close();
    }

    private void ButtonTest_OnClick(object? sender, RoutedEventArgs e)
    {
        using var ocr = new Ocr();
        
        using var mainContentImage = ScreenCapture.AreaCapture(_viewModel.MainContentOCRConfig);
        var mainContentText = ocr.PerformOcr(mainContentImage);
        using var roleNameImage = _viewModel.RoleNameOCRConfig != null
            ? ScreenCapture.AreaCapture(_viewModel.RoleNameOCRConfig)
            : null;
        var roleNameText = roleNameImage != null ? ocr.PerformOcr(roleNameImage) : null;

        var translatorContent = new TranslatorContent()
        {
            Content = mainContentText,
            Role = roleNameText == null ? "system" : roleNameText.Replace("\n", "").Trim()
        };

        using var openAiService = new OpenAiService(_viewModel.OpenAIConfig);

        var result = openAiService.GetTranslate(translatorContent);

        _viewModel.Result = new DebuggerViewModel.DebuggerResult()
        {
            MainContentImage = Utils.Image.ConvertToAvaloniaBitmap(mainContentImage),
            MainContentText = mainContentText,
            RoleNameImage = roleNameImage != null ? Utils.Image.ConvertToAvaloniaBitmap(roleNameImage) : null,
            RoleNameText = roleNameText,
            ResultText = result?.GetText(),
            Response = Utils.Utils.ToJson(result)
        };
    }
}