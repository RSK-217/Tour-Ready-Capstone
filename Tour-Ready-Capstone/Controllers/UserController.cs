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
        public UserController(IUser userRepository)

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
        public ActionResult GetUserById(int id)
        {
            var user = _userRepo.GetUserById(id);
            return Ok(user);
        }

        // GET api/<UserController>/5
        [HttpGet("GetUserByFirebaseId/{firebaseId}")]
        public ActionResult GetUserByFirebaseId(string firebaseId)
        {
            var user = _userRepo.GetUserByFirebaseId(firebaseId);
            return Ok(user);
        }

        // GET api/<UserController>/5
        [HttpGet("CheckIfUserExists/{firebaseId}")]

        public bool CheckIfUserExists(string firebaseId)
        {
            bool result = _userRepo.CheckIfUserExists(firebaseId);
            return result;
        }

        // POST api/<UserController>
        [HttpPost("RegisterUser")]
        public ActionResult CreateUser(User user)
        {
            var newUser = _userRepo.CreateUser(user);
            return Ok(newUser);
        }

        // PUT api/<UserController>/5
        [HttpPut("{id}")]
        public void UpdateUser(User user)
        {
            _userRepo.UpdateUser(user);
        
        }

        
    }
}
