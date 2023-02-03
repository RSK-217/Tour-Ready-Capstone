import React, { useState } from 'react'
import { MdCancel, MdCheck } from 'react-icons/md'

export default function AddNotes({ setClickNote, setNoteDidUpdate, cityId }) {
  const [note, setNote] = useState({});

  const Cancel = () => {
    setClickNote(false)
  }

  const saveNote = async (e) => {
    e.preventDefault()
    const newNote = {
        note: note.note,
        cityId: cityId
    }

    const fetchOptions = {
        method: "POST",
        headers: {
            'Access-Control-Allow-Origin': 'https://localhost:7108',
            "Content-Type": "application/json"
        },
        body: JSON.stringify(newNote)
    }

    const response = await fetch('https://localhost:7108/api/Notes', fetchOptions);
    await response.json();
    setClickNote(false);
    setNoteDidUpdate(true);
}

  return (
    <div>
    <form className="add-city-form">
                <fieldset>
                    <div className="form-group">
                        <input
                            onChange={
                                (e) => {
                                    const copy = { ...note }
                                    copy.note = e.target.value
                                    setNote(copy)
                                }}
                            required autoFocus
                            type="text"
                            className="form-control"
                            placeholder=""
                        />
                    </div>
                </fieldset>
    </form>
    <MdCheck onClick={saveNote}></MdCheck>
    <MdCancel onClick={Cancel}></MdCancel>
    </div>
  )
}