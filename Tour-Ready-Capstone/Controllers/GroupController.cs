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
        public ActionResult GetAllGroups()
        {
            var groups = _groupRepo.GetAllGroups();
            return Ok(groups);
        }

        // GET api/<GroupController>/5
        [HttpGet("{id}")]
        public ActionResult GetGroupById(int id)
        {
            var group = _groupRepo.GetGroupById(id);
            return Ok(group);
        }

        // POST api/<GroupController>
        [HttpPost]
        public ActionResult CreateGroup(Group group)
        {
            var newGroup = _groupRepo.CreateGroup(group);
            return Ok(newGroup);
        }

        // PUT api/<GroupController>/5
        [HttpPut("{id}")]
        public void UpdateGroup(Group group)
        {
            _groupRepo.UpdateGroup(group);
        }

        // DELETE api/<GroupController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            _groupRepo.DeleteGroup(id);
        }
    }
}
