using System;
using Avalonia.Controls;
using Avalonia.Interactivity;
using AvaloniaDemo.Typings;
using AvaloniaDemo.ViewModels;
using MsBox.Avalonia;
using MsBox.Avalonia.Enums;
using OpenAI;
using OpenAI.Chat;

namespace AvaloniaDemo;

public partial class OpenAISettingsWindow : Window
{
    private OpenAiSettingsViewModel _viewModel;

    public OpenAISettingsWindow(OpenAiConfig? config)
    {
        _viewModel = new OpenAiSettingsViewModel(config);
        DataContext = _viewModel;
        InitializeComponent();
    }

    private void ButtonCancel_OnClick(object? sender, RoutedEventArgs e)
    {
        Close(null);
    }

    private void ButtonConfirm_OnClick(object? sender, RoutedEventArgs e)
    {
        Close(_viewModel.Config);
    }

    private void ButtonTest_OnClick(object? sender, RoutedEventArgs e)
    {
        var testContent = "简单介绍一下自己";
        try
        {
            var client = new OpenAIClient(_viewModel.Config.APIKey, new OpenAIClientOptions
            {
                Endpoint = new Uri(_viewModel.Config.BaseURL)
            });
            var chatClient = client.GetChatClient(_viewModel.Config.Model);
            var reply = chatClient.CompleteChat([
                new UserChatMessage(testContent)
            ]);
            MessageBoxManager.GetMessageBoxStandard("测试结果",
                    $"[User]: {testContent}\n[Assistant]: {reply.Value}\nToken: {reply.Value.Usage.TotalTokens}")
                .ShowWindowDialogAsync(this);
        }
        catch (Exception exception)
        {
            Console.WriteLine(exception);
            MessageBoxManager.GetMessageBoxStandard("错误", $"配置信息填写可能有误\n{exception.Message}")
                .ShowWindowDialogAsync(this);
        }
    }
}