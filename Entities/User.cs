namespace PaceBackend.Entities;


public class User
{
    public int Id { get; set; }
    public required string Subject { get; set; }
    public required string Name { get; set; }
    public required string Email { get; set; }
    public string FirstName { get; set; } = "";
    public string LastName { get; set; } = "";
    
    public string? OpenAIKey { get; set; }
    
    public string? ClaudeKey { get; set; }

    public DateTime CreatedAt { get; set; } 
    
    public DateTime UpdatedAt { get; set; }
}