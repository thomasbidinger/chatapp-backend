using OpenAI.Chat;
using PaceBackend.DTOs.Requests;

namespace PaceBackend.Clients;

public interface IChatClient
{
    /// <summary>
    /// Send a chat to the bot and get a response.
    /// </summary>
    /// <param name="message"></param>
    /// <returns></returns>
    Task<string> GetResponseAsync(string message);
    
    /// <summary>
    /// Send a chat to the bot and get a response.
    /// </summary>
    /// <param name="chatMessages"></param>
    /// <returns></returns>
    Task<string> GetResponseAsync(ChatMessageRequest[] chatMessages);
}