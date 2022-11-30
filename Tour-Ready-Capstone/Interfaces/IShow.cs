using Microsoft.AspNetCore.Mvc;
using Tour_Ready_Capstone.Models;

namespace Tour_Ready_Capstone.Interfaces
{
    public interface IShow
    {
        public ShowWithGroupName GetShowById(int id);
        public List<ShowWithGroupName> GetAllShowsByUserId(int id);

        public List<ShowWithGroupName> GetAllShowsByGroupId(int id);

        public Show CreateShow(Show show);

        public void UpdateShow(Show show);
    }
}
