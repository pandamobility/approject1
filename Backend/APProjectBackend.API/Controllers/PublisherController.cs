using APProjectBackend.Model.Entities;
using APProjectBackend.Model.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
namespace APProjectBackend.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PublisherController : ControllerBase
    {
        protected PublisherRepository Repository { get; }
        public PublisherController(PublisherRepository repository)
        {
            Repository = repository;
        }
        [HttpGet("{publisher_id}")]
        public ActionResult<Publisher> GetPublisher([FromRoute] int publisher_id)
        {
            Publisher publisher = Repository.GetPublisherById(publisher_id);
            if (publisher == null)
            {
                return NotFound();
            }
            return Ok(publisher);
        }
        [HttpGet]
        public ActionResult<IEnumerable<Publisher>> GetPublishers()
        {
            return Ok(Repository.GetPublishers());
        }
        [HttpPost]
        public ActionResult Post([FromBody] Publisher publisher)
        {
            if (publisher == null)
            {
                return BadRequest("Publisher information incorrect");
            }
            bool status = Repository.InsertPublisher(publisher);
            if (status)
            {
                return Ok();
            }
            return BadRequest();
        }
        [HttpPut]
        public ActionResult UpdatePublisher([FromBody] Publisher publisher)
        {
            if (publisher == null)
            {
                return BadRequest("Publisher info not correct");
            }
            Publisher existinPublisher = Repository.GetPublisherById(publisher.Publisher_id);
            if (existinPublisher == null)
            {
                return NotFound($"Publisher with id {publisher.Publisher_id} not found");
            }
            bool status = Repository.UpdatePublisher(publisher);
            if (status)
            {
                return Ok();
            }
            return BadRequest("Something went wrong");
        }
        [HttpDelete("{publisher_id}")]
        public ActionResult DeletePublisher([FromRoute] int publisher_id)
        {
            Publisher existingPublisher = Repository.GetPublisherById(publisher_id);
            if (existingPublisher == null)
            {
                return NotFound($"Publisher with id {publisher_id} not found");
            }
            bool status = Repository.DeletePublisher(publisher_id);
            if (status)
            {
                return NoContent();
            }
            return BadRequest($"Unable to delete publisher with id {publisher_id}");
        }
    }
}
