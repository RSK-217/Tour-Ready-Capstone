using Tour_Ready_Capstone.Models;
using Tour_Ready_Capstone.Interfaces;
using Microsoft.Data.SqlClient;

namespace Tour_Ready_Capstone.Repositories
{
    public class GroupMemberRepository : BaseRepository, IGroupMember
    {
        private readonly string _baseSqlSelect = @"SELECT Id,
                                                    UserId,
                                                    GroupId,
                                                    IsEditor
                                                   FROM [GroupMember]";
        public GroupMemberRepository(IConfiguration config) : base(config) { }

        public List<GroupMember> GetMemberByUserId(int id)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = $"{_baseSqlSelect} WHERE UserId = @userId";

                    cmd.Parameters.AddWithValue("@userId", id);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        var results = new List<GroupMember>();
                        while (reader.Read())
                        {
                            var group = LoadFromData(reader);

                            results.Add(group);
                        }

                        return results;

                    }
                }
            }
        }

        public List<GroupMemberByGroupId> GetAllMembersByGroupId(int id)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();

                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = $@"SELECT gm.id, userId, groupId, isEditor, g.groupName, g.[image], u.[name]
                                         FROM (([GroupMember] gm
                                         JOIN [Group] g ON gm.groupId = g.id)
                                         JOIN [User] u ON gm.userId = u.id)
                                         WHERE GroupId = @groupId";

                    cmd.Parameters.AddWithValue("@groupId", id);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        List<GroupMemberByGroupId>? groupMembers = new List<GroupMemberByGroupId>();
                        while (reader.Read())
                        {
                            GroupMemberByGroupId groupMember = LoadFromDataTwo(reader);

                            groupMembers.Add(groupMember);  
                        }

                        return groupMembers;
                    }
                }
            }
        }
        private GroupMember LoadFromData(SqlDataReader reader)
        {
            return new GroupMember
            {
                Id = reader.GetInt32(reader.GetOrdinal("Id")),
                UserId = reader.GetInt32(reader.GetOrdinal("UserId")),
                GroupId = reader.GetInt32(reader.GetOrdinal("GroupId")),
                IsEditor = reader.GetBoolean(reader.GetOrdinal("IsEditor"))
            };
        }

        private GroupMemberByGroupId LoadFromDataTwo(SqlDataReader reader)
        {
            return new GroupMemberByGroupId
            {
                Id = reader.GetInt32(reader.GetOrdinal("Id")),
                UserId = reader.GetInt32(reader.GetOrdinal("UserId")),
                GroupId = reader.GetInt32(reader.GetOrdinal("GroupId")),
                IsEditor = reader.GetBoolean(reader.GetOrdinal("IsEditor")),
                GroupName = reader.GetString(reader.GetOrdinal("GroupName")),
                Image = reader.GetString(reader.GetOrdinal("Image")),
                Name = reader.GetString(reader.GetOrdinal("Name")),
            };
        }
    }
}
