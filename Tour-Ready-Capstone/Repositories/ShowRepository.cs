using Tour_Ready_Capstone.Models;
using Tour_Ready_Capstone.Interfaces;
using Microsoft.Data.SqlClient;

namespace Tour_Ready_Capstone.Repositories
{
    public class ShowRepository : BaseRepository, IShow
    {
        private readonly string _baseSqlSelect = @"SELECT s.id, 
                                                          s.userId, 
                                                          groupId, 
                                                          g.groupName,  
                                                          venue, 
                                                          showDate, 
                                                          cityId,
                                                          c.name AS cityName,
                                                          setList, 
                                                          showNotes, 
                                                          merchSales, 
                                                          payout, 
                                                          isFavorite
                                                    FROM (([Show] s
                                                    JOIN [Group] g ON s.groupId = g.id)
                                                    JOIN [City] c ON s.cityId = c.id)";
                                                    
        public ShowRepository(IConfiguration config) : base(config) { }

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
                                                          c.name AS CityName,
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
                            var group = LoadFromData(reader);

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
                GroupName = reader.GetString(reader.GetOrdinal("GroupName")),
                Venue = reader.GetString(reader.GetOrdinal("Venue")),
                ShowDate = reader.GetDateTime(reader.GetOrdinal("ShowDate")),
                CityId = reader.GetInt32(reader.GetOrdinal("CityId")),
                CityName = reader.GetString(reader.GetOrdinal("CityName")),
                SetList = reader.GetString(reader.GetOrdinal("SetList")),
                ShowNotes = reader.GetString(reader.GetOrdinal("ShowNotes")),
                MerchSales = reader.GetInt32(reader.GetOrdinal("MerchSales")),
                Payout = reader.GetInt32(reader.GetOrdinal("Payout")),
                IsFavorite = reader.GetBoolean(reader.GetOrdinal("IsFavorite")),
            };
        }

    }
}
