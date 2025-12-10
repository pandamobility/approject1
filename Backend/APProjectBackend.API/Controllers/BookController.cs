using APProjectBackend.Model.Entities;
using APProjectBackend.Model.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
namespace APProjectBackend.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        protected BookRepository Repository { get; }
        public BookController(BookRepository repository)
        {
            Repository = repository;
        }
        [HttpGet("{book_id}")]
        public ActionResult<Book> GetBook([FromRoute] int book_id)
        {
            Book book = Repository.GetBookById(book_id);
            if (book == null)
            {
                return NotFound();
            }
            return Ok(book);
        }
        [HttpGet]
        public ActionResult<IEnumerable<Users>> GetBook()
        {
            return Ok(Repository.GetBook());
        }
        [HttpPost]
        public ActionResult Post([FromBody] Book book)
        {
            if (book == null)
            {
                return BadRequest("Book information incorrect");
            }
            bool status = Repository.InsertBook(book);
            if (status)
            {
                return Ok();
            }
            return BadRequest();
        }
        [HttpPut]
        public ActionResult UpdateBook([FromBody] Book book)
        {
            if (book == null)
            {
                return BadRequest("Book information incorrect");
            }
            Book existingBook = Repository.GetBookById(book.Book_id);
            if (existingBook == null)
            {
                return NotFound($"Book with id {book.Book_id} not found");
            }
            bool status = Repository.UpdateBook(book);
            if (status)
            {
                return Ok();
            }
            return BadRequest("Something went wrong");
        }
        [HttpDelete("{book_id}")]
        public ActionResult DeleteBook([FromRoute] int book_id)
        {
            Book existingBook = Repository.GetBookById(book_id);
            if (existingBook == null)
            {
                return NotFound($"Users with id {book_id} not found");
            }
            bool status = Repository.DeleteBook(book_id);
            if (status)
            {
                return NoContent();
            }
            return BadRequest($"Unable to delete user with id {book_id}");
        }
    }
}
