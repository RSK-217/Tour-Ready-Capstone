using Microsoft.AspNetCore.Mvc;
using Tour_Ready_Capstone.Models;

namespace Tour_Ready_Capstone.Interfaces
{
    public interface IShow
    {
        public List<ShowWithGroupName> GetAllShowsByUserId(int id);
    }
}
