using OpenAI.Chat;
using PaceBackend.Clients;
using PaceBackend.DTOs.Requests;

namespace PaceBackend.Services;

public class ChatService(ILogger<ChatService> logger, OpenAiClient openAiClient)
{
    private readonly Dictionary<string, IChatClient> _chatClients = new()
    {
        {"openai", openAiClient}
    };
    
    public async Task<string> GetResponseAsync(string message)
    {
        var client = _chatClients["openai"];
        var response = await client.GetResponseAsync(message);
        
        return response;
    }
    
    public async Task<string> GetResponseAsync(ChatMessageRequest[] chatMessages)
    {
        var client = _chatClients["openai"];
        
        var response = await client.GetResponseAsync(chatMessages);
        
        return response;
    }
}