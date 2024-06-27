using Avalonia.Controls;
using Avalonia.Interactivity;
using System.IO;
using AvaloniaDemo.Typings;

namespace AvaloniaDemo.Views;

public partial class MainWindow : Window
{
    public OcrAreaConfig? MainContentArea { get; set; }
    public OcrAreaConfig? RoleNameArea { get; set; }
    public OpenAiConfig? OpenAIConfig { get; set; }

    public const string ConfigFolderPath = "config";
    public static readonly string AreaConfigPath = Path.Combine(ConfigFolderPath, "area.json");
    public static readonly string OpenAIConfigPath = Path.Combine(ConfigFolderPath, "openai.json");
    public static readonly string TranslatorConfigPath = Path.Combine(ConfigFolderPath, "translator.json");

    public static string DataFolderPath = "data";

    public MainWindow()
    {
        // 初始化，判断配置文件是否存在
        if (!Directory.Exists(ConfigFolderPath))
        {
            Directory.CreateDirectory(ConfigFolderPath);
        }

        if (!Directory.Exists(DataFolderPath))
        {
            Directory.CreateDirectory(DataFolderPath);
        }

        InitializeComponent();

        if (!Utils.Utils.CheckConfigFileExists(AreaConfigPath) ||
            !Utils.Utils.CheckConfigFileExists(OpenAIConfigPath) ||
            !Utils.Utils.CheckConfigFileExists(TranslatorConfigPath))
        {
            Utils.Utils.ErrorMsgBox("错误", "配置文件校验失败，请检查格式");
        }
        else
        {
            var areaConfig = Utils.Utils.GetFromJsonFile<AreaConfig>(AreaConfigPath);
            MainContentArea = areaConfig?.MainContent;
            RoleNameArea = areaConfig?.RoleName;

            OpenAIConfig = Utils.Utils.GetFromJsonFile<OpenAiConfig>(OpenAIConfigPath);
        }
    }

    public async void ButtonMainContentAreaConfigClick(object source, RoutedEventArgs args)
    {
        var ocrAreaWindow = new OcrAreaWindow(MainContentArea, "文本识别区域设置");
        var config = await ocrAreaWindow.ShowDialog<OcrAreaConfig?>(this);
        if (config == null) return;
        MainContentArea = config;
        var areaConfig = Utils.Utils.GetFromJsonFile<AreaConfig>(AreaConfigPath);
        areaConfig!.MainContent = config;
        Utils.Utils.WriteToJsonFile(areaConfig, AreaConfigPath);
    }

    public async void ButtonRoleNameAreaConfigClick(object source, RoutedEventArgs args)
    {
        var ocrAreaWindow = new OcrAreaWindow(RoleNameArea, "角色识别区域设置");
        var config = await ocrAreaWindow.ShowDialog<OcrAreaConfig?>(this);
        if (config == null) return;
        RoleNameArea = config;
        var areaConfig = Utils.Utils.GetFromJsonFile<AreaConfig>(AreaConfigPath);
        areaConfig!.RoleName = config;
        Utils.Utils.WriteToJsonFile(areaConfig, AreaConfigPath);
    }

    public async void ButtonOpenAISettingsClick(object source, RoutedEventArgs args)
    {
        var openaiSettingsWindow = new OpenAISettingsWindow(OpenAIConfig);
        var config = await openaiSettingsWindow.ShowDialog<OpenAiConfig?>(this);
        if (config == null) return;
        OpenAIConfig = config;
        Utils.Utils.WriteToJsonFile(config, OpenAIConfigPath);
    }

    private void ButtonStartClick(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        var translatorWindow = new TranslatorWindow();
        translatorWindow.Show();
    }

    private void ButtonTestClick(object? sender, RoutedEventArgs e)
    {
        var debuggerWindow = new DebuggerWindow(OpenAIConfig, MainContentArea, RoleNameArea);
        debuggerWindow.Show();
    }
}