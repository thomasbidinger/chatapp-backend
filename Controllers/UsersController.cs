using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Web.Resource;
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
    public class UsersController(ILogger<UsersController> logger, IHttpContextAccessor contextAccessor, DataContext context, UsersService usersService) : ControllerBase
    {
        private readonly ILogger<UsersController> _logger = logger;
        private readonly IHttpContextAccessor _contextAccessor = contextAccessor;
        private readonly DataContext _context = context;
        private readonly UsersService _usersService = usersService;

        // GET: api/users
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetAll()
        {
            var users = await _usersService.List();
            
            return Ok(users);
        }

        // GET: api/Users/{id}
        [HttpGet("{id}")]
        public ActionResult<User> GetById(int id)
        {
            // var user = _context.Users.FirstOrDefault(p => p.Id == id);
            var user = _usersService.Get(id);
            
            if (user == null)
            {
                return NotFound($"User with ID {id} not found.");
            }
            return Ok(user);
        }
        
        
        
        
        
        // GET: api/Users/sub/{id}
        [HttpGet("/sub/{id}")]
        public ActionResult<User> GetBySubjectId(string subjectId)
        {
            var user = _context.Users.FirstOrDefault(p => p.Subject == subjectId);
            if (user == null)
            {
                return NotFound($"user with subject ID {subjectId} not found.");
            }
            return Ok(user);
        }
        
        // GET: api/Users/exists
        [HttpGet("exists")]
        public async Task<ActionResult<bool>> Exists()
        {
            var subject = ClaimsHelper.GetSubject(User);
            var exists = await _context.Users.AnyAsync(u => u.Subject == subject);
            return Ok(exists);
        }

        // POST: api/Users
        [HttpPost]
        public async Task<ActionResult> Create()
        {
            // Retrieve the OID of the authenticated user from claims
            try
            {
                bool success = await _usersService.Create(new CreateUserRequest()
                                                     {
                                                         Subject = ClaimsHelper.GetSubject(User),
                                                         Email = ClaimsHelper.GetEmail(User),
                                                         Name = ClaimsHelper.GetName(User),
                                                     }) ;
                
                if (!success)
                {
                    return BadRequest("Could not create user");
                }
            }
            catch (Exception e)
            {
                logger.LogError(e, "Could not find the necessary claim for the user");
                throw;
            }
            
            // return CreatedAtAction(nameof(GetById), new { id = newProduct.Id }, newProduct);
            return Ok();
        }
        
        
        

        // PUT: api/products/{id}
        // [HttpPut("{id}")]
        // public ActionResult Update(int id, [FromBody] Product updatedProduct)
        // {
        //     // var product = Products.FirstOrDefault(p => p.Id == id);
        //     // if (product == null)
        //     // {
        //     //     return NotFound($"Product with ID {id} not found.");
        //     // }
        //     //
        //     // product.Name = updatedProduct.Name;
        //     // product.Price = updatedProduct.Price;
        //     //
        //     // return NoContent();
        // }

        // DELETE: api/products/{id}
    //     [HttpDelete("{id}")]
    //     public ActionResult Delete(int id)
    //     {
    //         // var product = Products.FirstOrDefault(p => p.Id == id);
    //         // if (product == null)
    //         // {
    //         //     return NotFound($"Product with ID {id} not found.");
    //         // }
    //         //
    //         // Products.Remove(product);
    //         // return NoContent();
    //     }
    }
}
