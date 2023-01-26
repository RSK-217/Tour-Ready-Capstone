using Tour_Ready_Capstone.Models;
using Tour_Ready_Capstone.Interfaces;
using Microsoft.Data.SqlClient;

namespace Tour_Ready_Capstone.Repositories
{
    public class PlaceRepository : BaseRepository , IPlace
    {
        private readonly string _baseSqlSelect = @"SELECT id,
                                                          placeName,
                                                          cityId
                                                    FROM [Places]";

        public PlaceRepository(IConfiguration configuration) : base(configuration) { }

        public Place GetPlaceById(int id)
        {
            using SqlConnection conn = Connection;
            {
                conn.Open();
                using SqlCommand cmd = conn.CreateCommand();
                {
                    cmd.CommandText = $"{_baseSqlSelect} WHERE Id = @id";

                    cmd.Parameters.AddWithValue("@id", id);

                    using SqlDataReader reader = cmd.ExecuteReader();
                    {
                        Place? result = null;
                        if (reader.Read())
                        {
                            return LoadFromData(reader);
                        }

                        return result;
                    }
                }

            }

        }

        public List<Place> GetAllPlacesByCityId(int id)
        {
            using SqlConnection conn = Connection;
            {
                conn.Open();
                using SqlCommand cmd = conn.CreateCommand();
                {
                    cmd.CommandText = $"{_baseSqlSelect} WHERE CityId = @cityId";

                    cmd.Parameters.AddWithValue("@cityId", id);

                    using SqlDataReader reader = cmd.ExecuteReader();
                    {
                        var results = new List<Place>();
                        while (reader.Read())
                        {
                            var place = LoadFromData(reader);
                            results.Add(place);
                        }

                        return results;
                    }
                }
            }
        }

        public Place CreatePlace(Place place)
        {
            using SqlConnection connection = Connection;
            {
                connection.Open();
                using SqlCommand cmd = connection.CreateCommand();
                {
                    cmd.CommandText = @"
                    INSERT INTO [Places] (PlaceName, CityId)
                    OUTPUT INSERTED.ID
                    VALUES (@placeName, @cityId)";

                    cmd.Parameters.AddWithValue("@placeName", place.PlaceName);
                    cmd.Parameters.AddWithValue("@cityId", place.CityId);

                    int id = (int)cmd.ExecuteScalar();

                    place.Id = id;
                    return place;
                }
            }
        }

        public void UpdatePlace(Place place)
        {
            using SqlConnection conn = Connection;
            {
                conn.Open();
                using SqlCommand cmd = conn.CreateCommand();
                {
                    cmd.CommandText = @"
                            UPDATE [Places]
                            SET
                                PlaceName = @placeName,
                                CityId = @cityId
                            WHERE Id = @id";
                }

                cmd.Parameters.AddWithValue("@id", place.Id);
                cmd.Parameters.AddWithValue("@placeName", place.PlaceName);
                cmd.Parameters.AddWithValue("@cityId", place.CityId);

                cmd.ExecuteNonQuery();
            }
        }

        public void DeletePlace(int id)
        {
            using SqlConnection conn = Connection;
            {
                conn.Open();
                using SqlCommand cmd = conn.CreateCommand();
                {
                    cmd.CommandText = @"DELETE FROM [Places]
                                        WHERE Id = @id";

                    cmd.Parameters.AddWithValue("@id", id);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        private Place LoadFromData(SqlDataReader reader)
        {
            return new Place
            {
                Id = reader.GetInt32(reader.GetOrdinal("Id")),
                PlaceName = reader.GetString(reader.GetOrdinal("PlaceName")),
                CityId = reader.GetInt32(reader.GetOrdinal("CityId"))
            };
        }
    }
}
