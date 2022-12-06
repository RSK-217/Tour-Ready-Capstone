using Tour_Ready_Capstone.Models;
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

        public ShowsByIdViewModel GetShowById(int id)
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
                                                          c.cityName,
                                                          c.state,
                                                          c.country,
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
                        ShowsByIdViewModel? result = null;
                        if (reader.Read())
                        {
                            return LoadFromData(reader);
                        }

                        return result;

                    }
                }
            }
        }
        public List<ShowsByIdViewModel> GetAllShowsByUserId(int id)
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
                                                          c.cityName,
                                                          c.state,
                                                          c.country,
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
                        var results = new List<ShowsByIdViewModel>();
                        while (reader.Read())
                        {
                            var show = LoadFromData(reader);

                            results.Add(show);
                        }

                        return results;

                    }
                }
            }
        }

        public List<ShowsByIdViewModel> GetAllShowsByGroupId(int id)
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
                                                          c.cityName,
                                                          c.state,
                                                          c.country,
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
                        var results = new List<ShowsByIdViewModel>();
                        while (reader.Read())
                        {
                            var show = LoadFromData(reader);

                            results.Add(show);
                        }

                        return results;

                    }
                }
            }
        }

        public Show CreateShow(Show show)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                    INSERT INTO [Show] (UserId, GroupId, Venue, ShowDate, CityId, SetList, ShowNotes, MerchSales, Payout, IsFavorite)
                    OUTPUT INSERTED.ID
                    VALUES (@userId, @groupId, @venue, @showDate, @cityId, @setList, @showNotes, @merchSales, @payout, @isFavorite)";
                    
                    cmd.Parameters.AddWithValue("@userId", show.UserId);
                    cmd.Parameters.AddWithValue("@groupId", show.GroupId);
                    cmd.Parameters.AddWithValue("@venue", show.Venue);
                    cmd.Parameters.AddWithValue("@showDate", show.ShowDate);
                    cmd.Parameters.AddWithValue("@CityId", show.CityId);
                    cmd.Parameters.AddWithValue("@SetList", show.SetList);
                    cmd.Parameters.AddWithValue("@ShowNotes", show.ShowNotes);
                    cmd.Parameters.AddWithValue("@MerchSales", show.MerchSales);
                    cmd.Parameters.AddWithValue("@Payout", show.Payout);
                    cmd.Parameters.AddWithValue("@IsFavorite", show.IsFavorite);

                    int id = (int)cmd.ExecuteScalar();

                    show.Id = id;
                    return show;
                }
            }
        }

        public void UpdateShow(Show show)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();

                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                            UPDATE [Show]
                            SET
                                UserId = @userId,
                                GroupId = @groupId,
                                Venue = @venue,
                                ShowDate = @showDate,
                                CityId = @cityId,
                                SetList = @setList,
                                ShowNotes = @showNotes,
                                MerchSales = @merchSales,
                                Payout = @payout,
                                IsFavorite = @isFavorite
                            WHERE Id = @id";

                    cmd.Parameters.AddWithValue("@id", show.Id);
                    cmd.Parameters.AddWithValue("@userId", show.UserId);
                    cmd.Parameters.AddWithValue("@groupId", show.GroupId);
                    cmd.Parameters.AddWithValue("@venue", show.Venue);
                    cmd.Parameters.AddWithValue("@showDate", show.ShowDate);
                    cmd.Parameters.AddWithValue("@CityId", show.CityId);
                    cmd.Parameters.AddWithValue("@SetList", show.SetList);
                    cmd.Parameters.AddWithValue("@ShowNotes", show.ShowNotes);
                    cmd.Parameters.AddWithValue("@MerchSales", show.MerchSales);
                    cmd.Parameters.AddWithValue("@Payout", show.Payout);
                    cmd.Parameters.AddWithValue("@IsFavorite", show.IsFavorite);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void DeleteShow(int id)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();

                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                            DELETE FROM [Show]
                            WHERE Id = @id
                        ";

                    cmd.Parameters.AddWithValue("@id", id);

                    cmd.ExecuteNonQuery();
                }
            }
        }
        private ShowsByIdViewModel LoadFromData(SqlDataReader reader)
        {
            return new ShowsByIdViewModel
            {
                Id = reader.GetInt32(reader.GetOrdinal("Id")),
                UserId = reader.GetInt32(reader.GetOrdinal("UserId")),
                GroupId = reader.GetInt32(reader.GetOrdinal("GroupId")),
                GroupName = reader.GetString(reader.GetOrdinal("GroupName")),
                Venue = reader.GetString(reader.GetOrdinal("Venue")),
                ShowDate = reader.GetDateTime(reader.GetOrdinal("ShowDate")),
                CityId = reader.GetInt32(reader.GetOrdinal("CityId")),
                CityName = reader.GetString(reader.GetOrdinal("CityName")),
                State = reader.GetString(reader.GetOrdinal("State")),
                Country = reader.GetString(reader.GetOrdinal("Country")),
                SetList = reader.GetString(reader.GetOrdinal("SetList")),
                ShowNotes = reader.GetString(reader.GetOrdinal("ShowNotes")),
                MerchSales = reader.GetInt32(reader.GetOrdinal("MerchSales")),
                Payout = reader.GetInt32(reader.GetOrdinal("Payout")),
                IsFavorite = reader.GetBoolean(reader.GetOrdinal("IsFavorite")),
            };
        }

    }
}
