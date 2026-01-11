using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WakeyWakeyBackendAPI.DTOs;
using WakeyWakeyBackendAPI.Models;
using WakeyWakeyBackendAPI.Services;

namespace WakeyWakeyBackendAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        //here I am injecting the DB context into the controller
        private readonly AppDbContext _context;
        private readonly PasswordHasher<User> _passwordHasher;
        private readonly JwtService _jwtService;

        //here I am initializing the DB context via constructor injection
        public AuthController(AppDbContext context, PasswordHasher<User> passwordHasher, JwtService jwtService)
        {
            _context = context;
            _passwordHasher = passwordHasher;
            _jwtService = jwtService;
        }

        // GET: api/<AuthController>/GetUsers
        [Authorize]
        [HttpGet("getUsers")]
        public async Task<ActionResult<IEnumerable<UsersDto>>> GetUsers()
        {
            //will use EF core to pull the users from the DB 
            var users = await _context.Users
            .Select(x => new UsersDto
            {
                Id = x.Id,
                Email = x.Email
            })
            .ToListAsync();

            return Ok(users);
        }


        //fetch user method
        //[Authorize]
        //[HttpGet("fetchUser")]
        //public async Task<ActionResult<User>> FetchUser()
        //{
        //    //would fetch user details based on the access token the user would be assigned when they create their account
        //}


        // GET api/<AuthController.cs>/5
        [HttpGet("getUserEmail/{id}")]
        public async Task<ActionResult<GetUserEmailDto>> GetUserEmail(int id)
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
        public async Task<ActionResult<string>> Register([FromBody] RegisterUserDto registerRequest)
        {
            var user = new User { 
                Name = registerRequest.Name,
                Email = registerRequest.Email,
                Password = registerRequest.Password,
            };
            var hashedPassword = _passwordHasher.HashPassword(user, registerRequest.Password);
            user.Password = hashedPassword;
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            var result = new RegisterUserResponseDto()
            {
                Name = user.Name,
                Email = user.Email
            };
            return Ok(result);

        }


        [HttpPost("login")]
        public async Task<ActionResult<bool>> Login([FromBody] LoginUserDto request)
        {
            var user = await _context.Users.Where(x => x.Email == request.Email).FirstOrDefaultAsync();
            if (user == null) {
                return NotFound("Couldn't find user with associated email.");
            }
            
            // Return a new jwt token if credentials are correct
            var result = _passwordHasher.VerifyHashedPassword(user, user.Password, request.Password);
            return result == PasswordVerificationResult.Failed 
                ?  Unauthorized("Invalid credentials")
                : Ok(_jwtService.CreateToken(user));
        }

        // DELETE api/<AuthController.cs>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
