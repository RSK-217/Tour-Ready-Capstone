import React, { useState } from 'react'
import { MdCancel, MdCheck } from 'react-icons/md'

export default function AddPlace({ setClickPerson, setDidUpdate, cityId }) {
  const [place, setPlace] = useState({});

  const Cancel = () => {
    setClickPerson(null)
  }

  const savePlace = async (e) => {
    e.preventDefault()
    const newPlace = {
        placeName: place.placeName,
        cityId: cityId
    }

    const fetchOptions = {
        method: "POST",
        headers: {
            'Access-Control-Allow-Origin': 'https://localhost:7108',
            "Content-Type": "application/json"
        },
        body: JSON.stringify(newPlace)
    }

    const response = await fetch('https://localhost:7108/api/Place', fetchOptions);
    await response.json();
    setClickPerson(null);
    setDidUpdate(true);
}

  return (
    <div>
    <form className="add-city-form">
                <fieldset>
                    <div className="form-group">
                        <input
                            onChange={
                                (e) => {
                                    const copy = { ...place }
                                    copy.placeName = e.target.value
                                    setPlace(copy)
                                }}
                            required autoFocus
                            type="text"
                            className="form-control"
                            placeholder=""
                        />
                    </div>
                </fieldset>
    </form>
    <MdCheck onClick={savePlace}></MdCheck>
    <MdCancel onClick={Cancel}></MdCancel>
    </div>
  )
}