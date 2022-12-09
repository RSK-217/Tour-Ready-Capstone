using Microsoft.AspNetCore.Authorization;
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

        // GET api/<ShowController>/5
        [HttpGet("GetShowById/{id}")]
        public ActionResult GetShowById(int id)
        {
            var show = _showRepo.GetShowById(id);
            return Ok(show);
        }
        // GET: api/<ShowController>/5
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
        public ActionResult CreateShow(Show show)
        {
            var newShow = _showRepo.CreateShow(show);
            return Ok(newShow);
        }

        // PUT api/<ShowController>/5
        [HttpPut("{id}")]
        public void UpdateShow(Show show)
        {
            _showRepo.UpdateShow(show);
        }

        // DELETE api/<ShowController>/5
        [HttpDelete("{id}")]
        public void DeleteShow(int id)
        {
            _showRepo.DeleteShow(id);
        }
    }
}
