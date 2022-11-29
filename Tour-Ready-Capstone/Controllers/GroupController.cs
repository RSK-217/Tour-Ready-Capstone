using Microsoft.AspNetCore.Mvc;
using Tour_Ready_Capstone.Models;
using Tour_Ready_Capstone.Repositories;
using Tour_Ready_Capstone.Interfaces;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Tour_Ready_Capstone.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GroupController : ControllerBase
    {
        private readonly IGroup _groupRepo;

        public GroupController(
        IGroup groupRepository)

        {
            _groupRepo = groupRepository;

        }

        // GET: api/<GroupController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<GroupController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<GroupController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<GroupController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<GroupController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
