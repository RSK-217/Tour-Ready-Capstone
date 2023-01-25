using Tour_Ready_Capstone.Models;
using Tour_Ready_Capstone.Interfaces;
using Microsoft.Data.SqlClient;


namespace Tour_Ready_Capstone.Repositories
{
    public class NotesRepository : BaseRepository, INotes
    {
        private readonly string _baseSqlSelect = @" SELECT id,
                                                           note,
                                                           cityId
                                                    FROM [Notes]";

        public NotesRepository(IConfiguration config) : base(config) { }

        public Notes GetNoteById(int id)
        {
            using SqlConnection conn = Connection;
            {
                conn.Open();
                using SqlCommand cmd = conn.CreateCommand();
                {
                    cmd.CommandText = $"{_baseSqlSelect} WHERE Id = @id";
                    cmd.Parameters.AddWithValue("id", id);

                    using SqlDataReader reader = cmd.ExecuteReader();
                    {
                        Notes? result = null;
                        if(reader.Read())
                        {
                            return LoadFromData(reader);
                        }

                        return result;
                    }
                }
            }
        }

        public List<Notes> GetAllNotesByCityId(int id)
        {
            using SqlConnection conn = Connection;
            {
                conn.Open();
                using SqlCommand cmd = conn.CreateCommand();
                {
                    cmd.CommandText = $"{_baseSqlSelect} WHERE CityId = @cityId";
                    cmd.Parameters.AddWithValue("cityId", id);

                    using SqlDataReader reader = cmd.ExecuteReader();
                    {
                        var results = new List<Notes>();
                        while (reader.Read())
                        {
                            var note = LoadFromData(reader);
                            results.Add(note);
                        }
                        return results;

                    }
                }
            }
        }

        public Notes CreateNote(Notes note)
        {
            using SqlConnection conn = Connection;
            {
                conn.Open();
                using SqlCommand cmd = conn.CreateCommand();
                {
                    cmd.CommandText = @"
                    INSERT INTO [Notes] (Note, CityId)
                    OUTPUT INSERTED.ID
                    VALUES (@note, @cityId)";

                    cmd.Parameters.AddWithValue("id", note.Id);
                    cmd.Parameters.AddWithValue("note", note.Note);
                    cmd.Parameters.AddWithValue("cityId", note.CityId);

                    int id = (int)cmd.ExecuteScalar();

                    note.Id = id;
                    return note;
                }
            }
        }

        public void UpdateNote(Notes note)
        {
            using SqlConnection conn = Connection;
            {
                conn.Open();
                using SqlCommand cmd = conn.CreateCommand();
                {
                    cmd.CommandText = @"
                            UPDATE [Notes]
                            SET
                                Note = @note,
                                CityId = @cityId
                            WHERE Id = @id";

                    cmd.Parameters.AddWithValue("id", note.Id);
                    cmd.Parameters.AddWithValue("note", note.Note);
                    cmd.Parameters.AddWithValue("cityId", note.CityId);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void DeleteNote(int id)
        {
            using SqlConnection conn = Connection;
            {
                conn.Open();
                using SqlCommand cmd = conn.CreateCommand();
                {
                    cmd.CommandText = @"
                                DELETE FROM [Notes]
                                WHERE Id = @id";

                    cmd.Parameters.AddWithValue("@id", id);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        private Notes LoadFromData(SqlDataReader reader)
        {
            return new Notes
            {
                Id = reader.GetInt32(reader.GetOrdinal("id")),
                Note = reader.GetString(reader.GetOrdinal("note")),
                CityId = reader.GetInt32(reader.GetOrdinal("cityId"))
            };
        }
    }
}
