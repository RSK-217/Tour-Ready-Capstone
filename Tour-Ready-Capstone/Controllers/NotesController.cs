using Microsoft.AspNetCore.Mvc;
using Tour_Ready_Capstone.Interfaces;
using Tour_Ready_Capstone.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Tour_Ready_Capstone.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotesController : ControllerBase
    {
        private readonly INotes _noteRepo;

        public NotesController(INotes noteRepo)
        {
            _noteRepo = noteRepo;
        }

        // GET api/<NotesController>/5
        [HttpGet("GetNoteById/{id}")]
        public ActionResult GetNoteById(int id)
        {
            var note = _noteRepo.GetNoteById(id);
            return Ok(note);
        }

        // GET: api/<NotesController>/5
        [HttpGet("GetNotesByCityId/{id}")]
        public ActionResult GetAllNotesByCityId(int id)
        {
            var note = _noteRepo.GetAllNotesByCityId(id);
            return Ok(note);
        }

        // POST api/<NotesController>
        [HttpPost]
        public ActionResult CreateNote(Notes note)
        {
            var newNote = _noteRepo.CreateNote(note);
            return Ok(newNote);
        }

        // PUT api/<NotesController>/5
        [HttpPut("{id}")]
        public void UpdateNote(Notes note)
        {
            _noteRepo.UpdateNote(note);
        }

        // DELETE api/<NotesController>/5
        [HttpDelete("{id}")]
        public void DeleteNote(int id)
        {
            _noteRepo.DeleteNote(id);
        }
    }
}
