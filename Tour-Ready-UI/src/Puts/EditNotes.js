import React, {useState} from 'react'
import { MdCancel, MdCheck, MdDelete } from 'react-icons/md'

export default function EditNotes ({setEdit, setNoteDidUpdate, aNote}) {
  const [noteToEdit, setNoteToEdit] = useState({
    id: aNote.id,
    note: aNote.note,
    cityId: aNote.cityId
  });

  const Cancel = () => {
        setEdit(false)
      }
  
      const DeleteNote = () => {
        fetch(`https://localhost:7108/api/Notes/${aNote.id}`, {
            method: "DELETE"
        })
            .then(
                setEdit(false),
                setNoteDidUpdate(true))
            
    }    
    const UpdateNote = async (e) => {
        e.preventDefault();
        const newNote = {
          id: noteToEdit.id,
          note: noteToEdit.note,
          cityId: noteToEdit.cityId,
        };
      
        try {
          await fetch(`https://localhost:7108/api/Notes/${aNote.id}`, {
            method: "PUT",
            headers: {
              "Access-Control-Allow-Origin": "https://localhost:7108",
              "Content-Type": "application/json",
            },
            body: JSON.stringify(newNote),
          });
          setEdit(false);
          setNoteDidUpdate(true);
        } catch (error) {
          console.error("There was a problem with the fetch operation:", error);
        }
      };
      
    
  return (
    <div>
    <form className="edit-city-form">
                <fieldset>
                    <div className="form-group">
                        <input
                            onChange={
                                (e) => {
                                    const copy = { ...noteToEdit }
                                    copy.note = e.target.value
                                    setNoteToEdit(copy)
                                }}
                            type="text"
                            className="form-control"
                            value={noteToEdit.note}
                        />
                    </div>
                </fieldset>
    </form>
    <MdCheck onClick={UpdateNote}></MdCheck>
    <MdDelete onClick={DeleteNote}></MdDelete>
    <MdCancel onClick={Cancel}></MdCancel>
    </div>
  )
}
