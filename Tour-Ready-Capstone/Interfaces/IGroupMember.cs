using Microsoft.AspNetCore.Mvc;
using Tour_Ready_Capstone.Models;

namespace Tour_Ready_Capstone.Interfaces
{
    public interface IGroupMember
    {
        public List<GroupMemberByGroupId> GetMemberByUserId(int id);

        public List<GroupMemberByGroupId> GetAllMembersByGroupId(int id);
        public GroupMember CreateGroupMember(GroupMember groupMember);
        public void UpdateGroupMember(GroupMember groupMember);
        public void DeleteMember(int id);



    }
}
