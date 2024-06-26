using System;
using System.IO;
using Avalonia.Controls;
using MsBox.Avalonia;
using MsBox.Avalonia.Enums;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace AvaloniaDemo.Utils;

public static class Utils
{
    public static bool CheckConfigFileExists(string path)
    {
        if (File.Exists(path))
        {
            return IsValidJson(File.ReadAllText(path));
        }

        File.WriteAllText(path, "{}");
        return true;
    }

    public static bool IsValidJson(string strInput)
    {
        strInput = strInput.Trim();
        if ((strInput.StartsWith("{") && strInput.EndsWith("}")) ||
            (strInput.StartsWith("[") && strInput.EndsWith("]")))
        {
            try
            {
                var obj = JToken.Parse(strInput);
                return true;
            }
            catch (JsonReaderException jex)
            {
                Console.WriteLine(jex.Message);
                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        return false;
    }

    public static async void ErrorMsgBox(string title, string text)
    {
        var box = MessageBoxManager.GetMessageBoxStandard(title, text);
        await box.ShowAsync();
        Environment.Exit(0);
    }

    public static T? GetFromJsonFile<T>(string path) where T : class
    {
        var json = File.ReadAllText(path);
        var result = IsValidJson(json);
        return result ? JsonConvert.DeserializeObject<T>(json) : null;
    }

    public static void WriteToJsonFile(object obj, string path)
    {
        var json = JsonConvert.SerializeObject(obj, Formatting.Indented);
        File.WriteAllText(path, json);
    }
}