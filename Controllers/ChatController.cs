using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Web.Resource;
using OpenAI.Assistants;
using OpenAI.Chat;
using PaceBackend.Data;
using PaceBackend.DTOs.Requests;
using PaceBackend.Entities;
using PaceBackend.Services;
using PaceBackend.Util;

namespace PaceBackend.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ChatController(ILogger<ChatController> logger, IHttpContextAccessor contextAccessor, DataContext context, ChatService chatService) : ControllerBase
    {
        // // POST: api/Chat
        // [HttpPost]
        // public async Task<ActionResult<string>> GetResponse([FromBody] ChatMessageRequest chatMessageRequest)
        // {
        //     string response;
        //     try
        //     {
        //         response = await chatService.GetResponseAsync(chatMessageRequest.Content);
        //         
        //         if (string.IsNullOrWhiteSpace(response))
        //         {
        //             return BadRequest("Could not get response message.");
        //         }
        //     }
        //     catch (Exception e)
        //     {
        //         logger.LogError(e, "Could not find the necessary claim for the user");
        //         throw;
        //     }
        //     
        //     return Ok(response);
        // }
        
        // POST: api/Chat
        [HttpPost]
        public async Task<ActionResult<string>> GetResponseWithContext([FromBody] ChatMessageRequest[] chatMessages)
        {
            string response;
            try
            {
                response = await chatService.GetResponseAsync(chatMessages);
                
                if (string.IsNullOrWhiteSpace(response))
                {
                    return BadRequest("Could not get response message.");
                }
            }
            catch (Exception e)
            {
                logger.LogError(e, "Could not find the necessary claim for the user");
                throw;
            }
            
            return Ok(response);
        }
    }
}
