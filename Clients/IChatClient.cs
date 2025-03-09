using OpenAI.Chat;
using PaceBackend.DTOs.Requests;

namespace PaceBackend.Clients;

public interface IChatClient
{
    /// <summary>
    /// Send a chat to a bot and get a response
    /// </summary>
    /// <param name="modelId"></param>
    /// <param name="chatMessages"></param>
    /// <returns></returns>
    Task<string> GetResponseAsync(string modelId, ChatMessageRequest[] chatMessages);
}