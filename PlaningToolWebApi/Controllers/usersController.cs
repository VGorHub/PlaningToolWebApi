using Microsoft.AspNetCore.Mvc;
using PlaningToolWebApi.Context;
using PlaningToolWebApi.Models;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PlaningToolWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly DBContext dbContext;

        public UsersController(DBContext dbContext)
        {
            this.dbContext = dbContext;
        }


        [HttpPost("loginUser")]
        public ActionResult<IEnumerable<User>> LoginUser(string login, string password)
        {

            var user = dbContext.users.Where(p => p.login == login && p.password == password).ToList();
            
            if (!user.Any())
            {
                return  NotFound();
            }
            else
            {
                return user;
            }

        }

        [HttpPost("signIn")]
        public ActionResult<IEnumerable<User>> SignIn(string login, string password, string role)
        {

            var user = dbContext.users.Where(p => p.login == login).ToList();

            if (!user.Any())
            {
                User user1 = new User();
                user1.role = role;
                user1.password = password;
                user1.login = login;

                dbContext.users.Add(user1);
                dbContext.SaveChanges();
                return Ok();
                
            }
            else
            {
                return BadRequest();
            }



        }
        // GET: api/<usersController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<usersController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<usersController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<usersController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<usersController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
