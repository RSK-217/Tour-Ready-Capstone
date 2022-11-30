using Microsoft.AspNetCore.Mvc;
using Tour_Ready_Capstone.Models;

namespace Tour_Ready_Capstone.Interfaces
{
    public interface IGroupMember
    {
        public List<MemebersOfGroupViewModel> GetMemberByUserId(int id);

        public List<MemebersOfGroupViewModel> GetAllMembersByGroupId(int id);
        public GroupMember CreateGroupMember(GroupMember groupMember);
        public void UpdateGroupMember(GroupMember groupMember);
        public void DeleteMember(int id);



    }
}
