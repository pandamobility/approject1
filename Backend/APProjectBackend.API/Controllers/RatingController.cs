using APProjectBackend.Model.Entities;
using APProjectBackend.Model.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
namespace APProjectBackend.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RatingController : ControllerBase
    {
        protected RatingRepository Repository { get; }
        public RatingController(RatingRepository repository)
        {
            Repository = repository;
        }
        [HttpGet("{rating_id}")]
        public ActionResult<Rating> GetRating([FromRoute] int rating_id)
        {
            Rating rating = Repository.GetRatingById(rating_id);
            if (rating == null)
            {
                return NotFound();
            }
            return Ok(rating);
        }
        [HttpGet]
        public ActionResult<IEnumerable<Rating>> GetRatings()
        {
            return Ok(Repository.GetRatings());
        }
        [HttpPost]
        public ActionResult Post([FromBody] Rating rating)
        {
            if (rating == null)
            {
                return BadRequest("Rating information incorrect");
            }
            bool status = Repository.InsertRating(rating);
            if (status)
            {
                return Ok();
            }
            return BadRequest();
        }
        [HttpPut]
        public ActionResult UpdateRating([FromBody] Rating rating)
        {
            if (rating == null)
            {
                return BadRequest("Rating info not correct");
            }
            Rating existinRating = Repository.GetRatingById(rating.Rating_id);
            if (existinRating == null)
            {
                return NotFound($"Rating with id {rating.Rating_id} not found");
            }
            bool status = Repository.UpdateRating(rating);
            if (status)
            {
                return Ok();
            }
            return BadRequest("Something went wrong");
        }
        [HttpDelete("{rating_id}")]
        public ActionResult DeleteRating([FromRoute] int rating_id)
        {
            Rating existingRating = Repository.GetRatingById(rating_id);
            if (existingRating == null)
            {
                return NotFound($"Rating with id {rating_id} not found");
            }
            bool status = Repository.DeleteRating(rating_id);
            if (status)
            {
                return NoContent();
            }
            return BadRequest($"Unable to delete rating with id {rating_id}");
        }
    }
}
