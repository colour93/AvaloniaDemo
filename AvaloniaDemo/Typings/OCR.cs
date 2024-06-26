using System.Drawing;
using System.Drawing.Imaging;

namespace AvaloniaDemo.Typings;

public class OCRAreaConfig
{
    public int Width { get; set; }
    public int Height { get; set; }
    public int Left { get; set; }
    public int Top { get; set; }

    public OCRAreaConfig(int width = 0, int height = 0, int left = 0, int top = 0)
    {
        Width = width;
        Height = height;
        Left = left;
        Top = top;
    }
}

public class OCRItem
{
    public OCRAreaConfig Config { get; set; }

    public OCRItem(OCRAreaConfig config)
    {
        Config = config;
    }

    public Bitmap Capture()
    {
        Bitmap bitmap = new(Config.Width, Config.Height, PixelFormat.Format24bppRgb);
        Graphics graphics = Graphics.FromImage(bitmap);
        graphics.CopyFromScreen(Config.Left, Config.Top, 0, 0, bitmap.Size);
        return bitmap;
    }
}