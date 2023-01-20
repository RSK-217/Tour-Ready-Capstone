using Microsoft.AspNetCore.Mvc;
using Tour_Ready_Capstone.Interfaces;
using Tour_Ready_Capstone.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Tour_Ready_Capstone.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
        public class PlaceController : ControllerBase
        {
            private readonly IPlace _placeRepo;

            public PlaceController(IPlace placeRepo)
            {
                _placeRepo = placeRepo;
            }

            // GET api/<PlaceController>/5
            [HttpGet("GetPlaceById/{id}")]
            public ActionResult GetPlaceById(int id)
            {
                var place = _placeRepo.GetPlaceById(id);
                return Ok(place);
            }

            // GET: api/<PlaceController>/5
            [HttpGet("GetPlacesByCityId/{id}")]
            public ActionResult GetAllPlacesByCityId(int id)
            {
                var place = _placeRepo.GetAllPlacesByCityId(id);
                return Ok(place);
            }

            // POST api/<PlaceController>
            [HttpPost]
            public ActionResult CreatePlace(Place place)
            {
                var newPlace = _placeRepo.CreatePlace(place);
                return Ok(newPlace);
            }

            // PUT api/<PlaceController>/5
            [HttpPut("{id}")]
            public void UpdatePlace(Place place)
            {
                _placeRepo.UpdatePlace(place);
            }

            // DELETE api/<PlaceController>/5
            [HttpDelete("{id}")]
            public void DeletePlace(int id)
            {
                _placeRepo.DeletePlace(id);
            }
        }
    }
