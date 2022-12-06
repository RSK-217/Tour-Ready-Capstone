using Microsoft.AspNetCore.Mvc;
using Tour_Ready_Capstone.Models;

namespace Tour_Ready_Capstone.Interfaces
{
    public interface IShow
    {
        public ShowsByIdViewModel GetShowById(int id);
        public List<ShowsByIdViewModel> GetAllShowsByUserId(int id);

        public List<ShowsByIdViewModel> GetAllShowsByGroupId(int id);

        public Show CreateShow(Show show);

        public void UpdateShow(Show show);

        public void DeleteShow(int id);
    }
}
