using System;
using AvaloniaDemo.Typings;
using MsBox.Avalonia;
using OpenAI;
using OpenAI.Chat;

namespace AvaloniaDemo.Services;

public class OpenAiService : IDisposable
{
    private OpenAiConfig Config { get; set; }
    private OpenAIClient Client { get; set; }
    private ChatClient ChatClient { get; set; }

    public OpenAiService(OpenAiConfig config)
    {
        Config = config;
        Client = new OpenAIClient(config.APIKey, new OpenAIClientOptions
        {
            Endpoint = new Uri(config.BaseURL)
        });
        ChatClient = Client.GetChatClient(config.Model);
    }
    
    public void Dispose()
    {
        GC.SuppressFinalize(this);
    }

    public TranslatorContentH? GetTranslate(TranslatorContent content,
        string originLanguage = "English", string targetLanguage = "Chinese")
    {
        try
        {
            var contentJson = Utils.Utils.ToJson(content);
            var reply = ChatClient.CompleteChat([
                new SystemChatMessage(Config.Prompt.Replace("{{ORIGIN_LANGUAGE}}", originLanguage)
                    .Replace("{{TARGET_LANGUAGE}}", targetLanguage)),
                new UserChatMessage(contentJson)
            ]);

            var replyJson = reply.Value.ToString();
            if (!Utils.Utils.IsValidJson(replyJson))
            {
                throw new Exception("Response isn't JSON format.");
            }

            var resultContent = Utils.Utils.GetFromJson<TranslatorContentH>(replyJson);
            if (resultContent == null)
            {
                throw new Exception("Response format is incorrect.");
            }

            return resultContent;
        }
        catch (Exception exception)
        {
            Console.WriteLine(exception);
            MessageBoxManager.GetMessageBoxStandard("错误", $"配置信息填写可能有误\n{exception.Message}")
                .ShowAsync();
            return null;
        }
    }
}