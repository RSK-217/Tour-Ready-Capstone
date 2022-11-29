using Tour_Ready_Capstone.Models;
using Tour_Ready_Capstone.Interfaces;
using Microsoft.Data.SqlClient;

namespace Tour_Ready_Capstone.Repositories
{
    public class GroupMemberRepository : BaseRepository, IGroupMember
    {
        public GroupMemberRepository(IConfiguration config) : base(config) { }

        public List<GroupMemberByGroupId> GetMemberByUserId(int id)
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
                                         WHERE UserId = @userId";

                    cmd.Parameters.AddWithValue("@userId", id);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        var results = new List<GroupMemberByGroupId>();
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
                            GroupMemberByGroupId groupMember = LoadFromData(reader);

                            groupMembers.Add(groupMember);  
                        }

                        return groupMembers;
                    }
                }
            }
        }

        public GroupMember CreateGroupMember(GroupMember groupMember)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                    INSERT INTO [GroupMember] (UserId, GroupId, IsEditor)
                    OUTPUT INSERTED.ID
                    VALUES (@userId, @groupId, @IsEditor);
                ";
                    ;
                    cmd.Parameters.AddWithValue("@userId", groupMember.UserId);
                    cmd.Parameters.AddWithValue("@groupId", groupMember.GroupId);
                    cmd.Parameters.AddWithValue("@IsEditor", groupMember.IsEditor);


                    int id = (int)cmd.ExecuteScalar();

                    groupMember.Id = id;
                    return groupMember;
                }
            }
        }

        public void UpdateGroupMember(GroupMember groupMember)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();

                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                            UPDATE [GroupMember]
                            SET
                                UserId = @userId,
                                GroupId = @groupId,
                                IsEditor = @isEditor
                            WHERE Id = @id";

                    cmd.Parameters.AddWithValue("@id", groupMember.Id);
                    cmd.Parameters.AddWithValue("@userId", groupMember.UserId);
                    cmd.Parameters.AddWithValue("@groupId", groupMember.GroupId);
                    cmd.Parameters.AddWithValue("@isEditor", groupMember.IsEditor);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void DeleteMember(int id)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();

                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                            DELETE FROM [GroupMember]
                            WHERE Id = @id
                        ";

                    cmd.Parameters.AddWithValue("@id", id);

                    cmd.ExecuteNonQuery();
                }
            }
        }


        private GroupMemberByGroupId LoadFromData(SqlDataReader reader)
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
