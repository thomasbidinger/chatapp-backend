namespace PaceBackend.DTOs.Requests;

public class ChatMessageRequest
{
    public required string Content { get; set; }
    public required string Role { get; set; }
}