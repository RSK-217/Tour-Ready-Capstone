using Microsoft.AspNetCore.Mvc;
using Tour_Ready_Capstone.Models;
using Tour_Ready_Capstone.Repositories;
using Tour_Ready_Capstone.Interfaces;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Tour_Ready_Capstone.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CityController : ControllerBase
    {
        private readonly ICity _cityRepo;

        public CityController(
        ICity cityRepository)

        {
            _cityRepo = cityRepository;

        }

        // GET api/<CityController>/5
        [HttpGet("GetCityById/{id}")]
        public ActionResult GetCityById(int id)
        {
            var city = _cityRepo.GetCityById(id);
            return Ok(city);
        }

        // GET api/<CityController>/5
        [HttpGet("GetAllCitiesByUserId/{id}")]
        public ActionResult GetAllCitiesByUserId(int id)
        {
            var cities = _cityRepo.GetAllCitiesByUserId(id);
            return Ok(cities);
        }

        // POST api/<CityController>
        [HttpPost]
        public ActionResult CreateCity(City city)
        {
            var newCity = _cityRepo.CreateCity(city);
            return Ok(newCity);
        }

        // PUT api/<CityController>/5
        [HttpPut("{id}")]
        public void UpdateCity(City city)
        {
            _cityRepo.UpdateCity(city);
        }

        // DELETE api/<CityController>/5
        [HttpDelete("{id}")]
        public void DeleteCity(int id)
        {
            _cityRepo.DeleteCity(id);
        }
    }
}
