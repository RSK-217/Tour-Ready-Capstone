﻿using Tour_Ready_Capstone.Models;
using Tour_Ready_Capstone.Interfaces;
using Microsoft.Data.SqlClient;

namespace Tour_Ready_Capstone.Repositories
{
    public class ShowRepository : BaseRepository, IShow
    {
        private readonly string _baseSqlSelect = @"SELECT id, 
                                                          userId, 
                                                          groupId,   
                                                          venue, 
                                                          showDate, 
                                                          cityId,
                                                          setList, 
                                                          showNotes, 
                                                          merchSales, 
                                                          payout, 
                                                          isFavorite
                                                    FROM [Show]";
                                                    
        public ShowRepository(IConfiguration config) : base(config) { }

        public ShowWithGroupName GetShowById(int id)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = $@"SELECT s.id, 
                                                          s.userId, 
                                                          groupId, 
                                                          g.groupName,  
                                                          venue, 
                                                          showDate, 
                                                          cityId,
                                                          c.city,
                                                          setList, 
                                                          showNotes, 
                                                          merchSales, 
                                                          payout, 
                                                          isFavorite
                                                    FROM (([Show] s
                                                    JOIN [Group] g ON s.groupId = g.id)
                                                    JOIN [City] c ON s.cityId = c.id)
                                        WHERE s.Id = @id";

                    cmd.Parameters.AddWithValue("@id", id);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        ShowWithGroupName? result = null;
                        if (reader.Read())
                        {
                            return LoadFromDataTwo(reader);
                        }

                        return result;

                    }
                }
            }
        }
        public List<ShowWithGroupName> GetAllShowsByUserId(int id)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = $@"SELECT s.id, 
                                                          s.userId, 
                                                          groupId, 
                                                          g.groupName,  
                                                          venue, 
                                                          showDate, 
                                                          cityId,
                                                          c.city,
                                                          setList, 
                                                          showNotes, 
                                                          merchSales, 
                                                          payout, 
                                                          isFavorite
                                                    FROM (([Show] s
                                                    JOIN [Group] g ON s.groupId = g.id)
                                                    JOIN [City] c ON s.cityId = c.id)
                                        WHERE s.UserId = @userId";

                    cmd.Parameters.AddWithValue("@userId", id);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        var results = new List<ShowWithGroupName>();
                        while (reader.Read())
                        {
                            var group = LoadFromDataTwo(reader);

                            results.Add(group);
                        }

                        return results;

                    }
                }
            }
        }

        public List<ShowWithGroupName> GetAllShowsByGroupId(int id)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = $@"SELECT s.id, 
                                                          s.userId, 
                                                          groupId, 
                                                          g.groupName,  
                                                          venue, 
                                                          showDate, 
                                                          cityId,
                                                          c.city,
                                                          setList, 
                                                          showNotes, 
                                                          merchSales, 
                                                          payout, 
                                                          isFavorite
                                                    FROM (([Show] s
                                                    JOIN [Group] g ON s.groupId = g.id)
                                                    JOIN [City] c ON s.cityId = c.id)
                                        WHERE GroupId = @groupId";

                    cmd.Parameters.AddWithValue("@groupId", id);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        var results = new List<ShowWithGroupName>();
                        while (reader.Read())
                        {
                            var group = LoadFromDataTwo(reader);

                            results.Add(group);
                        }

                        return results;

                    }
                }
            }
        }

        private ShowWithGroupName LoadFromData(SqlDataReader reader)
        {
            return new ShowWithGroupName
            {
                Id = reader.GetInt32(reader.GetOrdinal("Id")),
                UserId = reader.GetInt32(reader.GetOrdinal("UserId")),
                GroupId = reader.GetInt32(reader.GetOrdinal("GroupId")),
                Venue = reader.GetString(reader.GetOrdinal("Venue")),
                ShowDate = reader.GetDateTime(reader.GetOrdinal("ShowDate")),
                CityId = reader.GetInt32(reader.GetOrdinal("CityId")),
                SetList = reader.GetString(reader.GetOrdinal("SetList")),
                ShowNotes = reader.GetString(reader.GetOrdinal("ShowNotes")),
                MerchSales = reader.GetInt32(reader.GetOrdinal("MerchSales")),
                Payout = reader.GetInt32(reader.GetOrdinal("Payout")),
                IsFavorite = reader.GetBoolean(reader.GetOrdinal("IsFavorite")),
            };
        }
        private ShowWithGroupName LoadFromDataTwo(SqlDataReader reader)
        {
            return new ShowWithGroupName
            {
                Id = reader.GetInt32(reader.GetOrdinal("Id")),
                UserId = reader.GetInt32(reader.GetOrdinal("UserId")),
                GroupId = reader.GetInt32(reader.GetOrdinal("GroupId")),
                GroupName = reader.GetString(reader.GetOrdinal("GroupName")),
                Venue = reader.GetString(reader.GetOrdinal("Venue")),
                ShowDate = reader.GetDateTime(reader.GetOrdinal("ShowDate")),
                CityId = reader.GetInt32(reader.GetOrdinal("CityId")),
                City = reader.GetString(reader.GetOrdinal("City")),
                SetList = reader.GetString(reader.GetOrdinal("SetList")),
                ShowNotes = reader.GetString(reader.GetOrdinal("ShowNotes")),
                MerchSales = reader.GetInt32(reader.GetOrdinal("MerchSales")),
                Payout = reader.GetInt32(reader.GetOrdinal("Payout")),
                IsFavorite = reader.GetBoolean(reader.GetOrdinal("IsFavorite")),
            };
        }

    }
}