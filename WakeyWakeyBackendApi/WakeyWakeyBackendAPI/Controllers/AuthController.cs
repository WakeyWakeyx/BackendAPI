using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WakeyWakeyBackendAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
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
        public void Register([FromBody] string value)
        {
        }

        // DELETE api/<AuthController.cs>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
