using Microsoft.AspNetCore.Mvc;
using Tour_Ready_Capstone.Models;
using Tour_Ready_Capstone.Repositories;
using Tour_Ready_Capstone.Interfaces;


// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Tour_Ready_Capstone.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUser _userRepo;
        public UserController(
        IUser userRepository)

        {
            _userRepo = userRepository;

        }
        // GET: api/<UserController>
        [HttpGet]
        public ActionResult GetAllUsers()
        {
            var users = _userRepo.GetAllUsers();
            return Ok(users);
        }

        // GET api/<UserController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<UserController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<UserController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<UserController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
