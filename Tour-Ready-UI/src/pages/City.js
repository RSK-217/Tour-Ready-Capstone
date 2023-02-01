import React, { useState, useEffect } from "react";
import { useParams, Link } from 'react-router-dom';
import { BiEdit } from "react-icons/bi";
import { MdOutlineArrowBack, MdOutlineAddBox, MdDelete, MdCancel } from "react-icons/md";
import "../styles/city.css";
import AddPeople from "../Posts/AddPeople";

export default function City() {
    const [city, setCity] = useState({});
    const [people, setPeople] = useState([]);
    const [place, setPlace] = useState([]);
    const [note, setNote] = useState([]);
    const [selectedValue, setSelectedValue] = useState(null);
    const [clickPerson, setClickPerson] = useState(false)
    const { cityId } = useParams()

    const handleChange = (e) => {
        setSelectedValue(e.target.value);
      };

    const addPerson = () => {
        setClickPerson(true)
    }

    useEffect(() => {
        fetch(`https://localhost:7108/api/City/GetCityById/${cityId}`)
            .then(response => response.json())
            .then((data) => {
                setCity(data)
            })
    }, [])

    useEffect(() => {
        fetch(`https://localhost:7108/api/People/GetPeopleByCityId/${cityId}`)
        .then(response => response.json())
        .then((data) => {
            setPeople(data)
        })
    }, [people])

    useEffect(() => {
        fetch(`https://localhost:7108/api/Place/GetPlacesByCityId/${cityId}`)
        .then(response => response.json())
        .then((data) => {
            setPlace(data)
        })
    }, [place])

    useEffect(() => {
        fetch(`https://localhost:7108/api/Notes/GetNotesByCityId/${cityId}`)
        .then(response => response.json())
        .then((data) => {
            setNote(data)
        })
    }, [note])

    return (
        <div className="full-city-body">
            <h1 className="city-header">{city.cityName}, {city.state} - {city.country}</h1>
            <Link className='edit-city-link' to={`/city/edit/${cityId}`}><BiEdit></BiEdit>edit</Link>
            <section className="city-section-body">
                <h6 className="city-section-title">People</h6>
                <MdOutlineAddBox onClick={addPerson}>add</MdOutlineAddBox>
                {clickPerson === true ? <AddPeople setClickPerson={setClickPerson} cityId={cityId} /> : null}
                {clickPerson}
                <div className="city-section-box">
                    {people ? people.map((person) => {
                        return (
                            <div className="city-section-content" key={person.id}>
                            <input
                                type="radio"
                                value={person.person}
                                onChange={handleChange}
                                checked={selectedValue === person.person}
                            />
                            <p className="city-text">{person.person}</p>
                            {selectedValue === person.person ? <div className="city-icons"><BiEdit></BiEdit>&nbsp;<MdDelete></MdDelete>&nbsp;<MdCancel onClick={() => setSelectedValue(null)}></MdCancel></div> : null}
                            </div>
                        )
                    }) : null}
                </div>          
                <h6 className="city-section-title">Places</h6>
                <div className="city-section-box">
                    {place ? place.map((aPlace) => {
                        return (
                            <div className="city-section-content" key={aPlace.id}>
                            <input
                                type="radio"
                                value={aPlace.placeName}
                                onChange={handleChange}
                                checked={selectedValue === aPlace.placeName}
                            />
                            <p className="city-text">{aPlace.placeName}</p>
                            {selectedValue === aPlace.placeName ? <div className="city-icons"><BiEdit></BiEdit>&nbsp;<MdDelete></MdDelete>&nbsp;<MdCancel onClick={() => setSelectedValue(null)}></MdCancel></div> : null}
                            </div>
                        )
                    }) : null}
                    
                </div>
                <h6 className="city-section-title">Notes</h6>
                <div className="city-section-box">
                    {note ? note.map((aNote) => {
                        return (
                            <div className="city-section-content" key={aNote.id}>
                            <input
                                type="radio"
                                value={aNote.note}
                                onChange={handleChange}
                                checked={selectedValue === aNote.note}
                            />
                            <p className="city-text">{aNote.note}</p>
                            {selectedValue === aNote.note ? <div className="city-icons"><BiEdit></BiEdit>&nbsp;<MdDelete></MdDelete>&nbsp;<MdCancel onClick={() => setSelectedValue(null)}></MdCancel></div> : null}
                            </div>
                        )
                    }) : null}
                </div>
            </section>
            <Link className="back-to-cities" to="/cities"><MdOutlineArrowBack></MdOutlineArrowBack>back</Link>
        </div>
    )

}
