using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using WakeyWakeyBackendAPI.Models;

namespace WakeyWakeyBackendAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        //here i am injecting the DB context into the controller
        private readonly AppDbContext _context;

        //here i am initializing the DB context via constructor injection
        public AuthController(AppDbContext context)
        {
            _context = context;
        }


        // GET: api/<AuthController>
        [HttpGet]
        public IEnumerable<string> GetUsers()
        {
            //will use EF core to pull the users from the DB 
            return new string[] { "value1", "value2" };
        }

        // GET api/<AuthController.cs>/5
        [HttpGet("{id}")]
        public string GetUserName(int id)
        {
            //will also query for the user from the DB using the id as the primary key 
            return "value";
        }

        
        [HttpPost]
        public async Task<IActionResult> Register([FromBody] User user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return Ok(new { message = "User registered successfully" });

        }

        // DELETE api/<AuthController.cs>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
