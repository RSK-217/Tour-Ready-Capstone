using Microsoft.AspNetCore.Mvc;
using Tour_Ready_Capstone.Models;

namespace Tour_Ready_Capstone.Interfaces
{
    public interface IPeople
    {
        public People GetPersonById(int id);
        public List<People> GetAllPeopleByCityId(int id);
        public People CreatePerson(People people);
        public void UpdatePerson(People people);
        public void DeletePerson(int id);

    }
}
