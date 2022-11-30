using Microsoft.AspNetCore.Mvc;
using Tour_Ready_Capstone.Models;


namespace Tour_Ready_Capstone.Interfaces
{
    public interface ICity
    {
        public City GetCityById(int id);
    }
}
