using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Web.Resource;
using PaceBackend.Data;
using PaceBackend.Entities;
using PaceBackend.Services;

namespace PaceBackend.Controllers
{
    [Authorize]
    [RequiredScope("Generic.Read")]
    [Route("api/[controller]")]
    [ApiController]
    public class ClaimsController(ILogger<ClaimsController> logger, IHttpContextAccessor contextAccessor, DataContext context, UsersService usersService) : ControllerBase
    {
        private readonly ILogger<ClaimsController> _logger = logger;
        private readonly IHttpContextAccessor _contextAccessor = contextAccessor;
        private readonly DataContext _context = context;
        private readonly UsersService _usersService = usersService;

        // GET: api/claims
        // [RequiredScope("Generic.Read2")]
        [HttpGet]
        public async Task<ActionResult<Dictionary<string, string>>> GetAll()
        {
            Dictionary<string, string> claims = new Dictionary<string, string>();
            foreach (var claim in User.Claims)
            {
                Console.WriteLine($"type: {claim.Type}, value: {claim.Value}");
                claims.Add(claim.Type, claim.Value);
            }
            
            return Ok(claims);
        }
        
    }
}
