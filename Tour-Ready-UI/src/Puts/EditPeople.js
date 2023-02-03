import React, {useState} from 'react'
import { MdCancel, MdCheck, MdDelete } from 'react-icons/md'

export default function EditPeople({setEdit, setPeopleDidUpdate, person}) {
  const [personToEdit, setPersonToEdit] = useState({
    id: person.id,
    person: person.person,
    cityId: person.cityId
  });

  const Cancel = () => {
        setEdit(false)
      }
  
      const DeletePerson = () => {
        fetch(`https://localhost:7108/api/People/${person.id}`, {
            method: "DELETE"
        })
            .then(
                setEdit(false),
                setPeopleDidUpdate(true))
            
    }    

    const UpdatePerson = async (e) => {
      e.preventDefault();
      const newPerson = {
        id: personToEdit.id,
        person: personToEdit.person,
        cityId: personToEdit.cityId,
      };
    
      try {
        await fetch(`https://localhost:7108/api/People/${person.id}`, {
          method: "PUT",
          headers: {
            "Access-Control-Allow-Origin": "https://localhost:7108",
            "Content-Type": "application/json",
          },
          body: JSON.stringify(newPerson),
        });
        setEdit(false);
        setPeopleDidUpdate(true);
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
                                    const copy = { ...personToEdit }
                                    copy.person = e.target.value
                                    setPersonToEdit(copy)
                                }}
                            type="text"
                            className="form-control"
                            value={personToEdit.person}
                        />
                    </div>
                </fieldset>
    </form>
    <MdCheck onClick={UpdatePerson}></MdCheck>
    <MdDelete onClick={DeletePerson}></MdDelete>
    <MdCancel onClick={Cancel}></MdCancel>
    </div>
  )
}

