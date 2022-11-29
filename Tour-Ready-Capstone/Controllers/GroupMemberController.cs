using Microsoft.AspNetCore.Mvc;
using Tour_Ready_Capstone.Models;
using Tour_Ready_Capstone.Repositories;
using Tour_Ready_Capstone.Interfaces;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Tour_Ready_Capstone.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GroupMemberController : ControllerBase
    {
        private readonly IGroupMember _groupMemberRepo;
        public GroupMemberController(IGroupMember groupMemberRepository)

        {
            _groupMemberRepo = groupMemberRepository;

        }
        
        // GET api/<GroupMemberController>/5
        [HttpGet("GetMemberByUserId/{id}")]
        public ActionResult GetMemberByUserId(int id)
        {
            var member = _groupMemberRepo.GetMemberByUserId(id);
            return Ok(member);
        }

        // GET api/<GroupMemberController>/5
        [HttpGet("GetAllMembersByGroupId/{id}")]
        public ActionResult GetAllMembersByGroupId(int id)
        {
            var members = _groupMemberRepo.GetAllMembersByGroupId(id);
            return Ok(members);
        }

        // GET api/<GroupMemberController>/5
        //[HttpGet("{id}")]
        //public string GetAllMembersByGroupId(int id)
        //{
        //    return "value";
        //}

        // POST api/<GroupMemberController>
        //[HttpPost]
        //public void Post([FromBody] string value)
        //{
        //}

        // PUT api/<GroupMemberController>/5
        //[HttpPut("{id}")]
        //public void Put(int id, [FromBody] string value)
        //{
        //}

        // DELETE api/<GroupMemberController>/5
        //[HttpDelete("{id}")]
        //public void Delete(int id)
        //{
        //}
    }
}
