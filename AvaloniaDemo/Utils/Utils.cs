using System;
using System.IO;
using System.Reflection;
using Avalonia.Controls;
using MsBox.Avalonia;
using MsBox.Avalonia.Enums;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;

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

    public static T? GetFromJson<T>(string json) where T : class
    {
        var result = IsValidJson(json);
        return result
            ? JsonConvert.DeserializeObject<T>(json, new JsonSerializerSettings()
            {
                ContractResolver = new DefaultContractResolver()
                {
                    NamingStrategy = new CamelCaseNamingStrategy()
                }
            })
            : null;
    }

    public static string ToJson(object obj)
    {
        return JsonConvert.SerializeObject(obj, new JsonSerializerSettings()
        {
            ContractResolver = new CamelCasePropertyNamesContractResolver(),
            Formatting = Formatting.Indented
        });
    }

    public static T? GetFromJsonFile<T>(string path) where T : class
    {
        return GetFromJson<T>(File.ReadAllText(path));
    }

    public static void WriteToJsonFile(object obj, string path)
    {
        File.WriteAllText(path, ToJson(obj));
    }
}