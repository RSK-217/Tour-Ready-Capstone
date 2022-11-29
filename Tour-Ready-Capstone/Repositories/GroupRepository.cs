﻿using Tour_Ready_Capstone.Models;
using Tour_Ready_Capstone.Interfaces;
using Microsoft.Data.SqlClient;

namespace Tour_Ready_Capstone.Repositories
{
    public class GroupRepository : BaseRepository, IGroup
    {
        private readonly string _baseSqlSelect = @"SELECT Id,
                                                    GroupName,
                                                    Image
                                                   FROM [Group]";
        public GroupRepository(IConfiguration config) : base(config) { }

        public List<Group> GetAllGroups()
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = _baseSqlSelect;

                    using (var reader = cmd.ExecuteReader())
                    {
                        var results = new List<Group>();
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

        public Group GetGroupById(int id)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = $"{_baseSqlSelect} WHERE Id = @id";

                    cmd.Parameters.AddWithValue("@id", id);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        Group? result = null;
                        if (reader.Read())
                        {
                            return LoadFromData(reader);
                        }

                        return result;

                    }
                }
            }
        }

        public Group CreateGroup(Group group)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                    INSERT INTO [Group] (GroupName, Image)
                    OUTPUT INSERTED.ID
                    VALUES (@groupName, @image);
                ";
                    ;
                    cmd.Parameters.AddWithValue("@groupName", group.GroupName);
                    cmd.Parameters.AddWithValue("@image", group.Image);


                    int id = (int)cmd.ExecuteScalar();

                    group.Id = id;
                    return group;
                }
            }
        }

        public void UpdateGroup(Group group)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();

                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                            UPDATE [Group]
                            SET
                                GroupName = @groupName,
                                Image = @image
                            WHERE Id = @id";

                    cmd.Parameters.AddWithValue("@id", group.Id);
                    cmd.Parameters.AddWithValue("@groupName", group.GroupName);
                    cmd.Parameters.AddWithValue("@image", group.Image);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void DeleteGroup(int id)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();

                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                            DELETE FROM [Group]
                            WHERE Id = @id
                        ";

                    cmd.Parameters.AddWithValue("@id", id);

                    cmd.ExecuteNonQuery();
                }
            }
        }
        private Group LoadFromData(SqlDataReader reader)
        {
            return new Group
            {
                Id = reader.GetInt32(reader.GetOrdinal("Id")),
                GroupName = reader.GetString(reader.GetOrdinal("GroupName")),
                Image = reader.GetString(reader.GetOrdinal("Image"))
            };
        }
    }
}
