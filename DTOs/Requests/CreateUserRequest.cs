namespace PaceBackend.DTOs.Requests;

public class CreateUserRequest
{
    public required string Subject { get; set; }
    public required string Name { get; set; }
    public required string Email { get; set; }
}