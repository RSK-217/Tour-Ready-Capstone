using Microsoft.AspNetCore.Mvc;
using Tour_Ready_Capstone.Models;

namespace Tour_Ready_Capstone.Interfaces
{
    public interface IGroup
    {
        public List<GroupsByUserId> GetAllGroupsByUserId(int id);
        public Group GetGroupById(int id);
        public Group CreateGroup(Group group);
        public void UpdateGroup(Group group);
        public void DeleteGroup(int id);

    }
}
