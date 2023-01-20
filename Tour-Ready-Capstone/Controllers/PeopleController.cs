using Microsoft.AspNetCore.Mvc;
using Tour_Ready_Capstone.Models;
using Tour_Ready_Capstone.Repositories;
using Tour_Ready_Capstone.Interfaces;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Tour_Ready_Capstone.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PeopleController : ControllerBase
    {
        private readonly IPeople _peopleRepo;

        public PeopleController(
        IPeople peopleRepository)

        {
            _peopleRepo = peopleRepository;

        }

        // GET api/<PeopleController>/5
        [HttpGet("GetPeopleById/{id}")]
        public ActionResult GetPersonById(int id)
        {
            var people = _peopleRepo.GetPersonById(id);
            return Ok(people);
        }

        // GET: api/<PeopleController>/5
        [HttpGet("GetPeopleByCityId/{id}")]
        public ActionResult GetAllPeopleByCityId(int id)
        {
            var people = _peopleRepo.GetAllPeopleByCityId(id);
            return Ok(people);
        }

        // POST api/<PeopleController>
        [HttpPost]
        public ActionResult CreatePerson(People people)
        {
            var newPerson = _peopleRepo.CreatePerson(people);
            return Ok(newPerson);
        }

        // PUT api/<PeopleController>/5
        [HttpPut("{id}")]
        public void UpdatePerson(People people)
        {
            _peopleRepo.UpdatePerson(people);
        }

        // DELETE api/<PeopleController>/5
        [HttpDelete("{id}")]
        public void DeletePerson(int id)
        {
            _peopleRepo.DeletePerson(id);
        }
    }
}
