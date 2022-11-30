using Tour_Ready_Capstone.Models;
using Tour_Ready_Capstone.Interfaces;
using Microsoft.Data.SqlClient;

namespace Tour_Ready_Capstone.Repositories
{
    public class CityRepository : BaseRepository, ICity
    {
        private readonly string _baseSqlSelect = @"SELECT Id,
                                                          UserId,
                                                          CityName,
                                                          State,
                                                          Country,
                                                          CityNotes
                                                   FROM [CITY]";
        public CityRepository(IConfiguration config) : base(config) { }

        public City GetCityById(int id)
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
                        City? result = null;
                        if (reader.Read())
                        {
                            return LoadFromData(reader);
                        }

                        return result;

                    }
                }
            }
        }

        public List<City> GetAllCitiesByUserId(int id)
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
                        var results = new List<City>();
                        while (reader.Read())
                        {
                            var city = LoadFromData(reader);

                            results.Add(city);
                        }

                        return results;

                    }
                }
            }
        }

        public City CreateCity(City city)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                    INSERT INTO [City] (UserId, CityName, State, Country, CityNotes)
                    OUTPUT INSERTED.ID
                    VALUES (@userId, @cityName, @state, @country, @cityNotes)";

                    cmd.Parameters.AddWithValue("@userId", city.UserId);
                    cmd.Parameters.AddWithValue("@cityName", city.CityName);
                    cmd.Parameters.AddWithValue("@state", city.State);
                    cmd.Parameters.AddWithValue("@country", city.Country);
                    cmd.Parameters.AddWithValue("@cityNotes", city.CityNotes);

                    int id = (int)cmd.ExecuteScalar();

                    city.Id = id;
                    return city;
                }
            }
        }

        public void UpdateCity(City city)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                            UPDATE [City]
                            SET
                                UserId = @userId,
                                CityName = @cityName,
                                State = @state,
                                Country = @country,
                                CityNotes = @cityNotes
                            WHERE Id = @id";
                    
                    cmd.Parameters.AddWithValue("@id", city.Id);
                    cmd.Parameters.AddWithValue("@userId", city.UserId);
                    cmd.Parameters.AddWithValue("@cityName", city.CityName);
                    cmd.Parameters.AddWithValue("@state", city.State);
                    cmd.Parameters.AddWithValue("@country", city.Country);
                    cmd.Parameters.AddWithValue("@cityNotes", city.CityNotes);

                    cmd.ExecuteNonQuery();
                }
            }
        }
        private City LoadFromData(SqlDataReader reader)
        {
            return new City
            {
                Id = reader.GetInt32(reader.GetOrdinal("Id")),
                UserId = reader.GetInt32(reader.GetOrdinal("UserId")),
                CityName = reader.GetString(reader.GetOrdinal("CityName")),
                State = reader.GetString(reader.GetOrdinal("State")),
                Country = reader.GetString(reader.GetOrdinal("Country")),
                CityNotes = reader.GetString(reader.GetOrdinal("CityNotes"))
            };
        }
    }
}
