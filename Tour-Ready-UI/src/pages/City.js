import React, { useState, useEffect } from "react";
import { useParams, Link } from 'react-router-dom';
import { BiEdit } from "react-icons/bi";
import { MdOutlineArrowBack, MdOutlineAddBox, MdDelete, MdCancel } from "react-icons/md";
import "../styles/city.css";

export default function City() {
    const [city, setCity] = useState({});
    const [people, setPeople] = useState([]);
    const [place, setPlace] = useState([]);
    const [note, setNote] = useState([]);
    const [selectedValue, setSelectedValue] = useState(null);
    const { cityId } = useParams()

    const handleChange = (e) => {
        setSelectedValue(e.target.value);
      };

    useEffect(() => {
        fetch(`https://localhost:7108/api/City/GetCityById/${cityId}`)
            .then(response => response.json())
            .then((data) => {
                setCity(data)
            })
    }, [cityId])

    useEffect(() => {
        fetch(`https://localhost:7108/api/People/GetPeopleByCityId/${cityId}`)
        .then(response => response.json())
        .then((data) => {
            setPeople(data)
        })
    }, [cityId])

    useEffect(() => {
        fetch(`https://localhost:7108/api/Place/GetPlacesByCityId/${cityId}`)
        .then(response => response.json())
        .then((data) => {
            setPlace(data)
        })
    }, [cityId])

    useEffect(() => {
        fetch(`https://localhost:7108/api/Notes/GetNotesByCityId/${cityId}`)
        .then(response => response.json())
        .then((data) => {
            setNote(data)
        })
    }, [cityId])

    return (
        <div className="full-city-body">
            <h1 className="city-header">{city.cityName}, {city.state} - {city.country}</h1>
            <Link className='edit-city-link' to={`/city/edit/${cityId}`}><BiEdit></BiEdit>edit</Link>
            <section className="city-section-body">
                <div className="city-people">
                    <h6 className="city-section-title">People</h6>
                    {people ? people.map((person) => {
                        return (
                            <div className="city-section-content" key={person.id}>
                            <input
                                type="radio"
                                value={person.person}
                                onChange={handleChange}
                                checked={selectedValue === person.person}
                            />
                            <p>{person.person}</p>
                            {selectedValue === person.person ? <><BiEdit></BiEdit><MdDelete></MdDelete><MdCancel onClick={() => setSelectedValue(null)}></MdCancel></> : null}
                            </div>
                        )
                    }) : null}
                </div>
            
                <div className="city-places">
                    <h6 className="city-section-title">Places</h6>
                    {place ? place.map((aPlace) => {
                        return (
                            <div className="city-section-content" key={aPlace.id}>
                            <input
                                type="radio"
                                value={aPlace.placeName}
                                onChange={handleChange}
                                checked={selectedValue === aPlace.placeName}
                            />
                            <p>{aPlace.placeName}</p>
                            {selectedValue === aPlace.placeName ? <><BiEdit></BiEdit><MdDelete></MdDelete><MdCancel onClick={() => setSelectedValue(null)}></MdCancel></> : null}
                            </div>
                        )
                    }) : null}
                    
                </div>
                <div className="city-notes">
                    <h6 className="city-section-title">Notes</h6>
                    {note ? note.map((aNote) => {
                        return (
                            <div className="city-section-content" key={aNote.id}>
                            <input
                                type="radio"
                                value={aNote.note}
                                onChange={handleChange}
                                checked={selectedValue === aNote.note}
                            />
                            <p>{aNote.note}</p>
                            {selectedValue === aNote.note ? <><BiEdit></BiEdit><MdDelete></MdDelete><MdCancel onClick={() => setSelectedValue(null)}></MdCancel></> : null}
                            </div>
                        )
                    }) : null}
                </div>
            </section>
            <Link className="back-to-cities" to="/cities"><MdOutlineArrowBack></MdOutlineArrowBack>back</Link>
        </div>
    )

}
