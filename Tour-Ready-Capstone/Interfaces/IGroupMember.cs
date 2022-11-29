using Microsoft.AspNetCore.Mvc;
using Tour_Ready_Capstone.Models;

namespace Tour_Ready_Capstone.Interfaces
{
    public interface IGroupMember
    {
        public List<GroupMember> GetMemberByUserId(int id);

        public List<GroupMemberByGroupId> GetAllMembersByGroupId(int id);
    }
}
