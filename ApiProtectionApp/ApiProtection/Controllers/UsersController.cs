using ApiProtection.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ApiProtection.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        // GET: api/Users
        [HttpGet]
        // [ResponseCache(Duration = 10, Location = ResponseCacheLocation.Any, NoStore = false)] // Any = anywhere, not just the client or the  // NoStore = false ensures the data is stored
        public IEnumerable<string> Get()
        {
            return new string[] { Random.Shared.Next(1, 101).ToString() }; // gives a random number between 1 and 100 inclusive
        }

        // GET api/Users/5
        [HttpGet("{id}")]
        [ResponseCache(Duration = 20, Location = ResponseCacheLocation.Any, NoStore = false)] 
        public string Get(int id)
        {
            // Note that there's no cross caching taking place. Caching will take place per ID.
            return $"Random Number: {Random.Shared.Next(1, 101)} for Id {id}";
        }

        // POST api/<UsersController>
        [HttpPost]
        public IActionResult Post([FromBody] UserModel user)
        {
            if (ModelState.IsValid)
            {
                return Ok("The model was valid");                
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        // PUT api/<UsersController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<UsersController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
