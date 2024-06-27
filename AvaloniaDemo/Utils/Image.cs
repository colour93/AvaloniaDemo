using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

namespace AvaloniaDemo.Utils;

public static class Image
{
    public static Avalonia.Media.Imaging.Bitmap ConvertToAvaloniaBitmap(Bitmap drawingBitmap)
    {
        using var memoryStream = new MemoryStream();
        drawingBitmap.Save(memoryStream, System.Drawing.Imaging.ImageFormat.Png);
        memoryStream.Seek(0, SeekOrigin.Begin);
        return new Avalonia.Media.Imaging.Bitmap(memoryStream);
    }

    public static byte[] ConvertToByteMap(Bitmap drawingBitmap)
    {
        using var ms = new MemoryStream();
        drawingBitmap.Save(ms, ImageFormat.Bmp);
        return ms.ToArray();
    }

    public static Bitmap ConvertToGrayscale(Bitmap originalImage)
    {
        using var grayscaleImage = new Bitmap(originalImage.Width, originalImage.Height);

        // 遍历图像的每个像素
        for (int y = 0; y < originalImage.Height; y++)
        {
            for (int x = 0; x < originalImage.Width; x++)
            {
                Color originalColor = originalImage.GetPixel(x, y);

                // 计算灰度值
                int grayValue = (int)(originalColor.R * 0.3 + originalColor.G * 0.59 + originalColor.B * 0.11);

                // 创建灰度色彩
                Color grayColor = Color.FromArgb(grayValue, grayValue, grayValue);

                // 设置灰度图像的像素
                grayscaleImage.SetPixel(x, y, grayColor);
            }
        }

        return grayscaleImage;
    }
}