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
                                                    Email,
                                                    Image
                                                   FROM [User]";
        public UserRepository(IConfiguration config) : base(config) { }

        public bool CheckIfUserExists(string firebaseId)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = $"{_baseSqlSelect} WHERE FirebaseID = @firebaseId";

                    cmd.Parameters.AddWithValue("@firebaseId", firebaseId);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        List<User> users = new List<User>();
                        while (reader.Read())
                        {
                            var user = LoadFromData(reader);

                            users.Add(user);
                        };
                        return users.Count > 0;
                    }
                    
                }
            }
        }
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

        public User GetUserById(int id)
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
                        User? result = null;
                        if (reader.Read())
                        {
                            return LoadFromData(reader);
                        }

                        return result;

                    }
                }
            }
        }

        public User GetUserByFirebaseId(string firebaseId)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = $"{_baseSqlSelect} WHERE FirebaseId = @firebaseId";

                    cmd.Parameters.AddWithValue("@firebaseId", firebaseId);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        User? result = null;
                        if (reader.Read())
                        {
                            return LoadFromData(reader);
                        }

                        return result;

                    }
                }
            }
        }

        public User CreateUser(User user)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                    INSERT INTO [User] (FirebaseId, Name, Email, Image)
                    OUTPUT INSERTED.ID
                    VALUES (@firebaseId, @Name, @email, @image);
                ";
                    cmd.Parameters.AddWithValue("@firebaseId", user.FirebaseId);
                    cmd.Parameters.AddWithValue("@name", user.Name);
                    cmd.Parameters.AddWithValue("@email", user.Email);
                    cmd.Parameters.AddWithValue("@image", user.Image);
                    

                    int id = (int)cmd.ExecuteScalar();

                    user.Id = id;
                    return user;
                }
            }
        }

        public void UpdateUser(User user)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();

                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                            UPDATE [User]
                            SET
                                Name = @name,
                                Email = @email,
                                Image = @image
                            WHERE Id = @id";
                    
                    cmd.Parameters.AddWithValue("@id", user.Id);
                    cmd.Parameters.AddWithValue("@name", user.Name);
                    cmd.Parameters.AddWithValue("@email", user.Email);
                    cmd.Parameters.AddWithValue("@image", user.Image);

                    cmd.ExecuteNonQuery();
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
                Email = reader.GetString(reader.GetOrdinal("Email")),
                Image = reader.GetString(reader.GetOrdinal("Image"))
            };
        }
    }
}
