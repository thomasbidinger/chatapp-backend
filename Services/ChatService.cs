using OpenAI.Chat;
using PaceBackend.Clients;
using PaceBackend.DTOs.Requests;

namespace PaceBackend.Services;

public class ChatService
{
    private readonly Dictionary<string, IChatClient> _modelToChatClient = new();
    
    public ChatService(ILogger<ChatService> logger, OpenAiClient openAiClient, GeminiClient geminiClient)
    {
        logger.LogInformation("Creating ChatService");
        _modelToChatClient.Add("gpt-4o", openAiClient);
        _modelToChatClient.Add("gemini-2.0-flash", geminiClient);
    }
    
    public async Task<string> GetResponseAsync(ChatMessageRequest[] chatMessages, string modelId)
    {
        if (!_modelToChatClient.Keys.Contains(modelId)) throw new ArgumentException($"Invalid model. Must be one of: [{_modelToChatClient.Keys.Aggregate((a, b) => a + ", " + b)}]");
        
        var client = _modelToChatClient[modelId];
        
        var response = await client.GetResponseAsync(modelId, chatMessages);
        
        return response;
    }
}