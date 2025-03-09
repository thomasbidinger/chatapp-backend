using System.Text.Json;
using System.Text.Json.Serialization;
using PaceBackend.DTOs.Requests;

namespace PaceBackend.Clients;

public class GeminiClient : IChatClient
{
    private ILogger<GeminiClient> _logger;
    
    private readonly string _apiKey;
    private readonly HttpClient _httpClient;
    private readonly JsonSerializerOptions _jsonOptions;

    // https://cloud.google.com/vertex-ai/generative-ai/docs/model-reference/inference#sample-requests
    private const string MODEL_ID = "gemini-2.0-flash";
    private const string BASE_URL = $"https://generativelanguage.googleapis.com/v1beta/models";

    private static readonly Dictionary<string, string> RoleMap = new()
    {
        { "user", "user" },
        { "assistant", "model" },
    };

    public GeminiClient(ILogger<GeminiClient> logger)
    {
        // set the logger
        _logger = logger ?? throw new Exception("Cannot create logger");
        
        // set the api key
        _apiKey = Environment.GetEnvironmentVariable("CHATAPP_GEMINI_API_KEY") ?? throw new Exception("API key not found");
    
        // set the base url
        _httpClient = new HttpClient();
        
        // set the json options
        _jsonOptions = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            WriteIndented = true,
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
        };

    }
    
    public async Task<string> GetResponseAsync(string modelId, ChatMessageRequest[] chatMessageRequests)
    {
        var requestUrl = $"{BASE_URL}/{MODEL_ID}:generateContent?key={_apiKey}";
        
        // Build chat history from messages array
        var contents = new List<object>();
        foreach (var chatMessage in chatMessageRequests)
        {
            contents.Add(new
            {
                role = RoleMap[chatMessage.Role],
                parts = new []
                {
                    new {text = chatMessage.Content}
                }
            });
        }
            
        // Construct the request body with the converstation history
        var requestBody = new
        {
            contents,  // The array of conversation messages
            generationConfig = new
            {
                temperature = 0.7,
                topP = 0.95,
                topK = 64
            }
        };
        
        try
        {
            // post request auto serialize body as json
            var response = await _httpClient.PostAsJsonAsync(requestUrl, requestBody, _jsonOptions);
            
            response.EnsureSuccessStatusCode();
            
            var responseContent = await response.Content.ReadAsStringAsync();
            var jsonResponse = JsonDocument.Parse(responseContent);
            _logger.LogDebug($"API response: {responseContent}");
                
            // Extract the model's response from the JSON
            var textContent = jsonResponse.RootElement
                .GetProperty("candidates")[0]
                .GetProperty("content")
                .GetProperty("parts")[0]
                .GetProperty("text")
                .GetString();
                
            return textContent;
        }
        catch (HttpRequestException ex)
        {
            throw new Exception($"API request failed: {ex.Message}", ex);
        }
        catch (JsonException ex)
        {
            throw new Exception($"JSON serialization failed: {ex.Message}", ex);
        }

    }
}