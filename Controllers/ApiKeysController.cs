using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Web.Resource;
using PaceBackend.Data;
using PaceBackend.Entities;

namespace PaceBackend.Controllers
{
    [Authorize]
    [RequiredScope("Generic.Read")]
    [Route("api/[controller]")]
    [ApiController]
    public class ApiKeysController(ILogger<ApiKeysController> logger, IHttpContextAccessor contextAccessor, DataContext context) : ControllerBase
    {
        private readonly ILogger<ApiKeysController> _logger = logger;
        private readonly IHttpContextAccessor _contextAccessor = contextAccessor;
        private readonly DataContext _context = context;

        // GET: api/products
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetAll()
        {
            // Retrieve the OID of the authenticated user from claims
            string sub;
            try
            {
                sub = User.Claims.First(c => c.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier").Value;
            }
            catch (Exception e)
            {
                logger.LogError(e, "Could not find the necessary claim for the user");
                throw;
            }
            
            Console.WriteLine($"subject: {sub}");

            var users = await _context.Users.ToListAsync();
            
            return Ok(users);
        }

        // GET: api/products/{id}
        [HttpGet("{id}")]
        public ActionResult<User> GetById(int id)
        {
            var product = _context.Users.FirstOrDefault(p => p.Id == id);
            if (product == null)
            {
                return NotFound($"User with ID {id} not found.");
            }
            return Ok(product);
        }

        // POST: api/products
        // [HttpPost]
        // public ActionResult Create([FromBody] Product newProduct)
        // {
        //     // if (newProduct == null)
        //     // {
        //     //     return BadRequest("Product cannot be null.");
        //     // }
        //     //
        //     // newProduct.Id = Products.Count + 1; // Simulate auto-increment ID
        //     // Products.Add(newProduct);
        //     //
        //     // return CreatedAtAction(nameof(GetById), new { id = newProduct.Id }, newProduct);
        // }

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
