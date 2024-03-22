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

        [HttpPost("joinEvent")]
        public ActionResult<IEnumerable<User>> JoinEvent(int userId, string password, int eventId)
        {
            var user = dbContext.users.Where(p => p.id == userId && p.password == password).ToList();

            if (!user.Any())
            {
                return Unauthorized();
            }
            else
            {
                var checkUserInMembers = dbContext.members.Where(p => p.userid == userId && p.eventid == eventId).ToList();
                if (!checkUserInMembers.Any())
                {
                    Member newMember = new Member();
                    newMember.userid = userId;
                    newMember.eventid = eventId;
                    dbContext.members.Add(newMember);
                    dbContext.SaveChanges();
                    return Ok();

                }
                else
                {
                    return BadRequest();
                }
            }
        }

        [HttpPost("signUp")]
        public ActionResult<IEnumerable<User>> SignUp(string login, string password, string role)
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
    }
}
