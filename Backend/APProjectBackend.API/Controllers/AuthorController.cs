using APProjectBackend.Model.Entities;
using APProjectBackend.Model.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
namespace APProjectBackend.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorController : ControllerBase
    {
        protected AuthorRepository Repository { get; }
        public AuthorController(AuthorRepository repository)
        {
            Repository = repository;
        }
        [HttpGet("{author_id}")]
        public ActionResult<Author> GetAuthor([FromRoute] int author_id)
        {
            Author author = Repository.GetAuthorById(author_id);
            if (author == null)
            {
                return NotFound();
            }
            return Ok(author);
        }
        [HttpGet]
        public ActionResult<IEnumerable<Author>> GetAuthors()
        {
            return Ok(Repository.GetAuthors());
        }
        [HttpPost]
        public ActionResult Post([FromBody] Author author)
        {
            if (author == null)
            {
                return BadRequest("Author information incorrect");
            }
            bool status = Repository.InsertAuthor(author);
            if (status)
            {
                return Ok();
            }
            return BadRequest();
        }
        [HttpPut]
        public ActionResult UpdateAuthor([FromBody] Author author)
        {
            if (author == null)
            {
                return BadRequest("Author info not correct");
            }
            Author existinAuthor = Repository.GetAuthorById(author.Author_id);
            if (existinAuthor == null)
            {
                return NotFound($"Author with id {author.Author_id} not found");
            }
            bool status = Repository.UpdateAuthor(author);
            if (status)
            {
                return Ok();
            }
            return BadRequest("Something went wrong");
        }
        [HttpDelete("{author_id}")]
        public ActionResult DeleteAuthor([FromRoute] int author_id)
        {
            Author existingAuthor = Repository.GetAuthorById(author_id);
            if (existingAuthor == null)
            {
                return NotFound($"Author with id {author_id} not found");
            }
            bool status = Repository.DeleteAuthor(author_id);
            if (status)
            {
                return NoContent();
            }
            return BadRequest($"Unable to delete author with id {author_id}");
        }
    }
}
