using System;
using System.Drawing;
using OpenCvSharp;
using Sdcb.PaddleInference;
using Sdcb.PaddleOCR;
using Sdcb.PaddleOCR.Models;
using Sdcb.PaddleOCR.Models.Local;

namespace AvaloniaDemo.Services;

public class Ocr : IDisposable
{
    private PaddleOcrAll All { get; set; }

    public Ocr()
    {
        All = new PaddleOcrAll(LocalFullModels.EnglishV4, PaddleDevice.Mkldnn());
    }
    
    public void Dispose()
    {
        All.Dispose();
        GC.SuppressFinalize(this);
    }
    
    public string PerformOcr(Bitmap bitmap, string language = "eng")
    {
        try
        {

            using var src = Cv2.ImDecode(Utils.Image.ConvertToByteMap(bitmap), ImreadModes.Color);
            var result = All.Run(src);
            // Console.WriteLine("Detected all texts: \n" + result.Text);
            // foreach (var region in result.Regions)
            // {
            //     Console.WriteLine(
            //         $"Text: {region.Text}, Score: {region.Score}, RectCenter: {region.Rect.Center}, RectSize:    {region.Rect.Size}, Angle: {region.Rect.Angle}");
            // }

            return result.Text;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return string.Empty;
        }
    }
}