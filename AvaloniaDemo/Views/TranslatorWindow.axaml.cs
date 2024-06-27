using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using AvaloniaDemo.ViewModels;

namespace AvaloniaDemo;

public partial class TranslatorWindow : Window
{
    public TranslatorWindow()
    {
        DataContext = new TranslatorViewModel();
        InitializeComponent();
    }
}