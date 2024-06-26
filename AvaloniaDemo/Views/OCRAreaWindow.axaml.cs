using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using System.Drawing;
using System.Drawing.Imaging;
using AvaloniaDemo.Typings;

namespace AvaloniaDemo;

public partial class OCRAreaWindow : Window
{
    public OCRAreaConfig? Config { get; private set; }

    public OCRAreaWindow(OCRAreaConfig? config, string title)
    {
        InitializeComponent();

        Title = title;

        if (config != null)
        {
            Height = config.Height;
            Width = config.Width;
            Position = new PixelPoint(config.Left, config.Top);
        }
    }

    private void ButtonSaveClick(object sender, RoutedEventArgs e)
    {
        Config = new OCRAreaConfig((int)Width, (int)Height, Position.X, Position.Y);
        Close(Config);
    }

    private void ButtonCancelClick(object sender, RoutedEventArgs e)
    {
        Close(null);
    }
}