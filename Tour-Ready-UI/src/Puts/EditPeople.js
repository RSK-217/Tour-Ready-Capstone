import React, {useState} from 'react'
import { MdCancel, MdCheck, MdPeople } from 'react-icons/md'

export default function EditPeople({setEditPerson, person, people}) {
  const [personToEdit, setPersonToEdit] = useState({});

  const Cancel = () => {
        setEditPerson(false)
      }
      

      const UpdatePerson = (e) => {
        e.preventDefault()
        const newPerson = {
            person: people.person
        }

        fetch(`https://localhost:7108/api/City/${person.id}`, {
            method: "PUT",
            headers: {
                'Access-Control-Allow-Origin': 'https://localhost:7108',
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(newPerson)
        })
            // .then(() => {
                
            // })
    }

    
  return (
    <div>
    <form className="edit-city-form">
                <fieldset>
                    <div className="form-group">
                        <input
                            onChange={
                                (e) => {
                                    const copy = { ...personToEdit }
                                    copy.person = e.target.value
                                    setPersonToEdit(copy)
                                }}
                            required autoFocus
                            type="text"
                            className="form-control"
                            value="person"
                        />
                    </div>
                </fieldset>
    </form>
    <MdCheck onClick={UpdatePerson}></MdCheck>
    <MdCancel onClick={Cancel}></MdCancel>
    </div>
  )
}

