using Microsoft.AspNetCore.Mvc;
using Tour_Ready_Capstone.Models;

namespace Tour_Ready_Capstone.Interfaces
{
    public interface IPlace
    {
        public Place GetPlaceById(int id);
        public List<Place> GetAllPlacesByCityId(int id);
        public Place CreatePlace(Place place);
        public void UpdatePlace(Place place);
        public void DeletePlace(int id);
    }
}
