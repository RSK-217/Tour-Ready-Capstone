using Microsoft.AspNetCore.Mvc;
using Tour_Ready_Capstone.Interfaces;
using Tour_Ready_Capstone.Models;
using Tour_Ready_Capstone.Repositories;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Tour_Ready_Capstone.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShowController : ControllerBase
    {
        private readonly IShow _showRepo;

        public ShowController(
        IShow showRepository)

        {
            _showRepo = showRepository;

        }

        // GET: api/<ShowController>
        [HttpGet("GetShowByUserId/{id}")]
        public ActionResult GetAllShowsByUserId(int id)
        {
            var shows = _showRepo.GetAllShowsByUserId(id);
            return Ok(shows);
        }

        // GET api/<ShowController>/5
        [HttpGet("GetAllShowsByGroupId/{id}")]
        public ActionResult GetAllShowsByGroupId(int id)
        {
            var shows = _showRepo.GetAllShowsByGroupId(id);
            return Ok(shows);
        }

        // POST api/<ShowController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<ShowController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<ShowController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
