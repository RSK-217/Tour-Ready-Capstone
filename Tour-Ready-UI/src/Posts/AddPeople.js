import React, { useState } from 'react'
import { MdCancel, MdCheck } from 'react-icons/md'

export default function AddPeople({ setClickPerson, cityId }) {
  const [people, setPeople] = useState({});

  const Cancel = () => {
    setClickPerson(null)
  }

  const savePerson = async (e) => {
    e.preventDefault()
    const newPerson = {
        person: people.person,
        cityId: cityId
    }

    const fetchOptions = {
        method: "POST",
        headers: {
            'Access-Control-Allow-Origin': 'https://localhost:7108',
            "Content-Type": "application/json"
        },
        body: JSON.stringify(newPerson)
    }

    const response = await fetch('https://localhost:7108/api/People', fetchOptions);
    await response.json();
    setClickPerson(null)
}

  return (
    <div>
    <form className="add-city-form">
                <fieldset>
                    <div className="form-group">
                        <input
                            onChange={
                                (e) => {
                                    const copy = { ...people }
                                    copy.person = e.target.value
                                    setPeople(copy)
                                }}
                            required autoFocus
                            type="text"
                            className="form-control"
                            placeholder=""
                        />
                    </div>
                </fieldset>
    </form>
    <MdCheck onClick={savePerson}></MdCheck>
    <MdCancel onClick={Cancel}></MdCancel>
    </div>
  )
}

