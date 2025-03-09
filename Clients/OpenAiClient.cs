using OpenAI.Chat;
using PaceBackend.DTOs.Requests;

namespace PaceBackend.Clients;

public class OpenAiClient : IChatClient
{
    private readonly ChatClient _client = new(model: "gpt-4o", apiKey: Environment.GetEnvironmentVariable("OPENAI_API_KEY"));
    
    public async Task<string> GetResponseAsync(string modelId, ChatMessageRequest[] chatMessageRequests)
    {
        // serialize the request
        List<ChatMessage> chatMessages = [];
        Console.WriteLine(chatMessageRequests.Length);
        foreach (var chatMessageRequest in chatMessageRequests)
        {
            Console.WriteLine(chatMessageRequest.Role);
            Console.WriteLine(chatMessageRequest.Content);
            
            if (chatMessageRequest.Role == "user")
            {
                chatMessages.Add(new UserChatMessage(chatMessageRequest.Content));
            }
            else if (chatMessageRequest.Role == "assistant")
            {
                chatMessages.Add(new AssistantChatMessage(chatMessageRequest.Content));
            }
            else
            {
                throw new Exception("Invalid role");
            }
        }
        
        ChatCompletion chatCompletion = await _client.CompleteChatAsync(chatMessages);
    
        return chatCompletion.Content[0].Text;
    }
}