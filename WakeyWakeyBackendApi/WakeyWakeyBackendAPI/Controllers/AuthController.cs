using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using WakeyWakeyBackendAPI.DTOs;
using WakeyWakeyBackendAPI.Models;

namespace WakeyWakeyBackendAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        //here i am injecting the DB context into the controller
        private readonly AppDbContext _context;
        private readonly PasswordHasher<User> _passwordHasher;

        //here i am initializing the DB context via constructor injection
        public AuthController(AppDbContext context, PasswordHasher<User> passwordHasher)
        {
            _context = context;
            _passwordHasher = passwordHasher;
        }


        // GET: api/<AuthController>/GetUsers
        [HttpGet("getUsers")]
        public async Task<ActionResult<IEnumerable<UsersDTO>>> GetUsers()
        {
            //will use EF core to pull the users from the DB 
            var users = await _context.Users
            .Select(x => new UsersDTO
            {
                Id = x.Id,
                Email = x.Email
            })
            .ToListAsync();

            return Ok(users);
        }

        // GET api/<AuthController.cs>/5
        [HttpGet("getUserEmail/{id}")]
        public async Task<ActionResult<GetUserEmailDTO>> GetUserEmail(int id)
        {
            //will also query for the user from the DB using the id as the primary key 
            var result = await _context.Users.Where(u => u.Id == id).Select(x => x.Email).FirstOrDefaultAsync();

            if (result == null)
            {
                return NotFound(new { message = $"Username not found for user with id: {id}" });
            }

            return Ok(result);
        }

        
        [HttpPost("create")]
        public async Task<ActionResult<string>> Register([FromBody] RegisterUserDTO registerRequest)
        {
            var user = new User { 
                Email = registerRequest.Email,
                Password = registerRequest.Password
            };
            var hashedPassword = _passwordHasher.HashPassword(user, registerRequest.Password);
            user.Password = hashedPassword;
            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return Ok(new { message = "User registered successfully" });

        }


        [HttpPost("login")]
        public async Task<ActionResult<bool>> Login([FromBody] LoginUserDTO request)
        {
            var user = await _context.Users.Where(x => x.Email == request.Email).FirstOrDefaultAsync();
            if (user == null) {
                return NotFound("Couldn't find user with associated email.");
            }
            
            var result = _passwordHasher.VerifyHashedPassword(user, user.Password, request.Password);
            return result == PasswordVerificationResult.Success ? Ok(result) : Unauthorized();
        }

        // DELETE api/<AuthController.cs>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
