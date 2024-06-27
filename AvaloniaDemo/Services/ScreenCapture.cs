using System.Drawing;
using AvaloniaDemo.Typings;

namespace AvaloniaDemo.Services;

public static class ScreenCapture
{
    public static Bitmap AreaCapture(OcrAreaConfig config)
    {
        var item = new OcrItem(config);
        return item.Capture();
    }
}