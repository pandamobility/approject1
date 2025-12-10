using APProjectBackend.Model.Entities;
using APProjectBackend.Model.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
namespace APProjectBackend.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GenreController : ControllerBase
    {
        protected GenreRepository Repository { get; }
        public GenreController(GenreRepository repository)
        {
            Repository = repository;
        }
        [HttpGet("{genre_id}")]
        public ActionResult<Genre> GetGenre([FromRoute] int genre_id)
        {
            Genre genre = Repository.GetGenreById(genre_id);
            if (genre == null)
            {
                return NotFound();
            }
            return Ok(genre);
        }
        [HttpGet]
        public ActionResult<IEnumerable<Genre>> GetGenres()
        {
            return Ok(Repository.GetGenre());
        }
        [HttpPost]
        public ActionResult Post([FromBody] Genre genre)
        {
            if (genre == null)
            {
                return BadRequest("Genre information incorrect");
            }
            bool status = Repository.InsertGenre(genre);
            if (status)
            {
                return Ok();
            }
            return BadRequest();
        }
        [HttpPut]
        public ActionResult UpdateGenre([FromBody] Genre genre)
        {
            if (genre == null)
            {
                return BadRequest("Genre info not correct");
            }
            Genre existinGenre = Repository.GetGenreById(genre.Genre_id);
            if (existinGenre == null)
            {
                return NotFound($"Genre with id {genre.Genre_id} not found");
            }
            bool status = Repository.UpdateGenre(genre);
            if (status)
            {
                return Ok();
            }
            return BadRequest("Something went wrong");
        }
        [HttpDelete("{genre_id}")]
        public ActionResult DeleteGenre([FromRoute] int genre_id)
        {
            Genre existingGenre = Repository.GetGenreById(genre_id);
            if (existingGenre == null)
            {
                return NotFound($"Genre with id {genre_id} not found");
            }
            bool status = Repository.DeleteGenre(genre_id);
            if (status)
            {
                return NoContent();
            }
            return BadRequest($"Unable to delete genre with id {genre_id}");
        }
    }
}
