using APProjectBackend.Model.Entities;
using APProjectBackend.Model.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
namespace APProjectBackend.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        protected UsersRepository Repository { get; }
        public UsersController(UsersRepository repository)
        {
            Repository = repository;
        }
        [HttpGet("{user_id}")]
        public ActionResult<Users> GetUsers([FromRoute] int user_id)
        {
            Users users = Repository.GetUsersById(user_id);
            if (users == null)
            {
                return NotFound();
            }
            return Ok(users);
        }
        [HttpGet]
        public ActionResult<IEnumerable<Users>> GetUsers()
        {
            return Ok(Repository.GetUsers());
        }
        [HttpPost]
        public ActionResult Post([FromBody] Users users)
        {
            if (users == null)
            {
                return BadRequest("Users information incorrect");
            }
            bool status = Repository.InsertUsers(users);
            if (status)
            {
                return Ok();
            }
            return BadRequest();
        }
        [HttpPut]
        public ActionResult UpdateUsers([FromBody] Users users)
        {
            if (users == null)
            {
                return BadRequest("Users info not correct");
            }
            Users existinUsers = Repository.GetUsersById(users.User_id);
            if (existinUsers == null)
            {
                return NotFound($"Users with id {users.User_id} not found");
            }
            bool status = Repository.UpdateUsers(users);
            if (status)
            {
                return Ok();
            }
            return BadRequest("Something went wrong");
        }
        [HttpDelete("{users_id}")]
        public ActionResult DeleteUsers([FromRoute] int user_id)
        {
            Users existingUsers = Repository.GetUsersById(user_id);
            if (existingUsers == null)
            {
                return NotFound($"Users with id {user_id} not found");
            }
            bool status = Repository.DeleteUsers(user_id);
            if (status)
            {
                return NoContent();
            }
            return BadRequest($"Unable to delete user with id {user_id}");
        }
    }
}
