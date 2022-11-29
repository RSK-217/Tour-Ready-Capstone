using Tour_Ready_Capstone.Models;
using Tour_Ready_Capstone.Interfaces;
using Microsoft.Data.SqlClient;

namespace Tour_Ready_Capstone.Repositories
{
    public class UserRepository : BaseRepository, IUser
    {
        private readonly string _baseSqlSelect = @"SELECT Id,
                                                    FirebaseId,
                                                    Name,
                                                    Title,
                                                    Email,
                                                    Phone,
                                                    Image
                                                   FROM [User]";
        public UserRepository(IConfiguration config) : base(config) { }

        public List<User> GetAllUsers()
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = _baseSqlSelect;

                    using (var reader = cmd.ExecuteReader())
                    {
                        var results = new List<User>();
                        while (reader.Read())
                        {
                            var user = LoadFromData(reader);

                            results.Add(user);
                        }

                        return results;
                    }
                }
            }
        }
        private User LoadFromData(SqlDataReader reader)
        {
            return new User
            {
                Id = reader.GetInt32(reader.GetOrdinal("Id")),
                FirebaseId = reader.GetString(reader.GetOrdinal("FirebaseId")),
                Name = reader.GetString(reader.GetOrdinal("Name")),
                Title = reader.GetString(reader.GetOrdinal("Title")),
                Email = reader.GetString(reader.GetOrdinal("Email")),
                Phone = reader.GetString(reader.GetOrdinal("Phone")),
                Image = reader.GetString(reader.GetOrdinal("Image"))
            };
        }
    }
}
