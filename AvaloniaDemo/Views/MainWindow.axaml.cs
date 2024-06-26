using Avalonia.Controls;
using Avalonia.Interactivity;
using System.Diagnostics;
using System;
using System.IO;
using Tesseract;
using System.Drawing;
using AvaloniaDemo.Typings;

namespace AvaloniaDemo.Views;

public partial class MainWindow : Window
{
    public OCRAreaConfig? MainContentArea { get; set; }
    public OCRAreaConfig? RoleNameArea { get; set; }

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
            if (areaConfig != null)
            {
                MainContentArea = areaConfig.MainContent;
                RoleNameArea = areaConfig.RoleName;
            }
        }
    }

    public async void ButtonMainContentAreaConfigClick(object source, RoutedEventArgs args)
    {
        var ocrAreaWindow = new OCRAreaWindow(MainContentArea, "文本识别区域设置");
        var config = await ocrAreaWindow.ShowDialog<OCRAreaConfig?>(this);
        if (config != null)
        {
            MainContentArea = config;
            var areaConfig = Utils.Utils.GetFromJsonFile<AreaConfig>(AreaConfigPath);
            areaConfig!.MainContent = config;
            Utils.Utils.WriteToJsonFile(areaConfig, AreaConfigPath);
        }
    }
    
    public async void ButtonRoleNameAreaConfigClick(object source, RoutedEventArgs args)
    {
        var ocrAreaWindow = new OCRAreaWindow(RoleNameArea, "角色识别区域设置");
        var config = await ocrAreaWindow.ShowDialog<OCRAreaConfig?>(this);
        if (config != null)
        {
            RoleNameArea = config;
            var areaConfig = Utils.Utils.GetFromJsonFile<AreaConfig>(AreaConfigPath);
            areaConfig!.RoleName = config;
            Utils.Utils.WriteToJsonFile(areaConfig, AreaConfigPath);
        }
    }

    public void ButtonOpenAISettingsClick(object source, RoutedEventArgs args)
    {
        var openAISettingsWindow = new OpenAISettingsWindow();
        openAISettingsWindow.Show();
    }

    public string PerformOCR(Bitmap bitmap)
    {
        try
        {
            // 初始化 Tesseract 引擎
            using var engine =
                new TesseractEngine(System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "tessdata"),
                    "eng+chi_sim", EngineMode.Default);
            // 从文件加载图像
            using var img = PixConverter.ToPix(bitmap);
            // 识别图像中的文本
            using var page = engine.Process(img);
            return page.GetText();
        }
        catch (Exception e)
        {
            Trace.TraceError(e.Message);
            return string.Empty;
        }
    }

    public async void ButtonPerformClick(object source, RoutedEventArgs args)
    {
        if (MainContentArea != null)
        {
            var mainContentItem = new OCRItem(MainContentArea);
            using var bitmap = mainContentItem.Capture();
            bitmap.Save("D:\\UserData\\Desktop\\Temp\\screenshot.png");
            var result = PerformOCR(bitmap);
            if (result != null)
            {
                //ResultTextBox.Text = result;
            }
        }
    }

    private void ButtonStartClick(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        var translatorWindow = new TranslatorWindow();
        translatorWindow.Show();
    }

    private void ButtonTestClick(object? sender, RoutedEventArgs e)
    {
    }
}