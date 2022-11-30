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
