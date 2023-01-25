using Tour_Ready_Capstone.Models;
using Tour_Ready_Capstone.Interfaces;
using Microsoft.Data.SqlClient;

namespace Tour_Ready_Capstone.Repositories
{
    public class PeopleRepository : BaseRepository, IPeople
    {
        private readonly string _baseSqlSelect = @"SELECT id,
                                                       person,
                                                       cityId
                                                FROM [People]";

        public PeopleRepository(IConfiguration config) : base(config) { }
        
        public People GetPersonById(int id)
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
                        People? result = null;
                        if (reader.Read())
                        {
                            return LoadFromData(reader);
                        }

                        return result;

                    }
                }
            }
        }

        public List<People> GetAllPeopleByCityId(int id) 
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
                        var results = new List<People>();
                        while (reader.Read()) 
                        {
                            var people = LoadFromData(reader);
                            results.Add(people);
                        }

                        return results;
                    }
                }
            }
        }

        public People CreatePerson(People people) 
        {
            using SqlConnection conn = Connection;
            {
                conn.Open();
                using SqlCommand cmd = conn.CreateCommand();
                {
                    cmd.CommandText = @"
                    INSERT INTO [People] (Person, CityId)
                    OUTPUT INSERTED.ID
                    VALUES (@person, @cityId)";

                    cmd.Parameters.AddWithValue("@person", people.Person);
                    cmd.Parameters.AddWithValue("@cityId", people.CityId);

                    int id = (int)cmd.ExecuteScalar();

                    people.Id = id;
                    return people;
                }
            }
        }

        public void UpdatePerson(People people)
        {
            using SqlConnection conn = Connection;
            {
                conn.Open();
                using SqlCommand cmd = conn.CreateCommand();
                {
                    cmd.CommandText = @"
                            UPDATE [People]
                            SET
                                Person = @person,
                                CityId = @cityId
                            WHERE Id = @id";

                    cmd.Parameters.AddWithValue("@id", people.Id);
                    cmd.Parameters.AddWithValue("@person", people.Person);
                    cmd.Parameters.AddWithValue("@cityId", people.CityId);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void DeletePerson(int id)
        {
            using SqlConnection conn = Connection;
            {
                conn.Open();

                using SqlCommand cmd = conn.CreateCommand();
                {
                    cmd.CommandText = @"
                            DELETE FROM [People]
                            WHERE Id = @id";

                    cmd.Parameters.AddWithValue("@id", id);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        private People LoadFromData(SqlDataReader reader)
        {
            return new People
            {
                Id = reader.GetInt32(reader.GetOrdinal("Id")),
                Person = reader.GetString(reader.GetOrdinal("Person")),
                CityId = reader.GetInt32(reader.GetOrdinal("CityId"))
            };
        }
    }
}
