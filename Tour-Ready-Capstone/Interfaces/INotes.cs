using Microsoft.AspNetCore.Mvc;
using Tour_Ready_Capstone.Models;

namespace Tour_Ready_Capstone.Interfaces
{
    public interface INotes
    {
        public Notes GetNoteById(int id);
        public List<Notes> GetAllNotesByCityId(int id);
        public Notes CreateNote(Notes note);
        public void UpdateNote(Notes note);
        public void DeleteNote(int id); 




    }
}
