import React, { useState, useEffect } from "react";
import { useParams, Link } from 'react-router-dom';
import { BiEdit } from "react-icons/bi";
import { MdOutlineArrowBack, MdOutlineAddBox, MdDelete, MdCancel } from "react-icons/md";
import "../styles/city.css";
import AddPeople from "../Posts/AddPeople";
import EditPeople from "../Puts/EditPeople";
import AddPlace from "../Posts/AddPlace";
import EditPlace from "../Puts/EditPlace";
import AddNotes from "../Posts/AddNotes";
import EditNotes from "../Puts/EditNotes";

export default function City() {
    const [city, setCity] = useState({});
    const [people, setPeople] = useState([]);
    const [place, setPlace] = useState([]);
    const [note, setNote] = useState([]);
    const [selectedValue, setSelectedValue] = useState(null);
    const [clickPerson, setClickPerson] = useState(false);
    const [clickPlace, setClickPlace] = useState(false);
    const [clickNote, setClickNote] = useState(false);
    const [edit, setEdit] = useState(false);
    const [peopleDidUpdate, setPeopleDidUpdate] = useState(false);
    const [placeDidUpdate, setPlaceDidUpdate] = useState(false);
    const [noteDidUpdate, setNoteDidUpdate] = useState(false);
    const { cityId } = useParams()

    const handleChange = (e) => {
        setSelectedValue(e.target.value);
      };

    const addPerson = () => {
        setClickPerson(true)
    }

    const addPlace = () => {
        setClickPlace(true)
    }

    const addNote = () => {
        setClickNote(true)
    }

    const editSelection = () => {
        setEdit(true)
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
              setPeople(data);
            });
        }, []);

        useEffect(() => {
            if (peopleDidUpdate) {
              fetch(`https://localhost:7108/api/People/GetPeopleByCityId/${cityId}`)
                .then(response => response.json())
                .then((data) => {
                  setPeople(data);
                  setPeopleDidUpdate(false);
                });
            }
          }, [peopleDidUpdate]);

    useEffect(() => {
        fetch(`https://localhost:7108/api/Place/GetPlacesByCityId/${cityId}`)
        .then(response => response.json())
        .then((data) => {
            setPlace(data)
        })
    }, [])

    useEffect(() => {
        if (placeDidUpdate) {
          fetch(`https://localhost:7108/api/Place/GetPlacesByCityId/${cityId}`)
            .then(response => response.json())
            .then((data) => {
              setPlace(data);
              setPlaceDidUpdate(false);
            });
        }
      }, [placeDidUpdate]);

    useEffect(() => {
        fetch(`https://localhost:7108/api/Notes/GetNotesByCityId/${cityId}`)
        .then(response => response.json())
        .then((data) => {
            setNote(data)
        })
    }, [])

    useEffect(() => {
        if (noteDidUpdate) {
          fetch(`https://localhost:7108/api/Notes/GetNotesByCityId/${cityId}`)
            .then(response => response.json())
            .then((data) => {
              setNote(data);
              setNoteDidUpdate(false);
            });
        }
      }, [noteDidUpdate]);

    return (
        <div className="full-city-body">
            <h1 className="city-header">{city.cityName}, {city.state} - {city.country}</h1>
            <Link className='edit-city-link' to={`/city/edit/${cityId}`}><BiEdit></BiEdit>edit</Link>
            <section className="city-section-body">
                <h6 className="city-section-title">People</h6>
                <MdOutlineAddBox onClick={addPerson} >add</MdOutlineAddBox>
                {clickPerson === true ? <AddPeople setClickPerson={setClickPerson} setPeopleDidUpdate={setPeopleDidUpdate} cityId={cityId} /> : null}
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
                            {selectedValue === person.person && edit === true ? 
                                <EditPeople setEdit={setEdit} setPeopleDidUpdate={setPeopleDidUpdate} person={person} people={people}/> : <p className="city-text">{person.person}</p>}
                            
                            {selectedValue === person.person && edit === false ? 
                                <div className="city-icons"><BiEdit onClick={editSelection}></BiEdit>&nbsp;
                                    <MdCancel onClick={() => setSelectedValue(null)}></MdCancel></div> : null}
                                </div>
                        )
                    }) : null}
                    
                </div>          
                <h6 className="city-section-title">Places</h6>
                <MdOutlineAddBox onClick={addPlace} >add</MdOutlineAddBox>
                {clickPlace === true ? <AddPlace setClickPlace={setClickPlace} setPlaceDidUpdate={setPlaceDidUpdate} cityId={cityId} /> : null}
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
                            {selectedValue === aPlace.placeName && edit === true ? 
                                <EditPlace setEdit={setEdit} setPlaceDidUpdate={setPlaceDidUpdate} aPlace={aPlace} place={place}/> : <p className="city-text">{aPlace.placeName}</p>}
                            
                            {selectedValue === aPlace.placeName && edit === false ? 
                                <div className="city-icons"><BiEdit onClick={editSelection}></BiEdit>&nbsp;
                                    <MdCancel onClick={() => setSelectedValue(null)}></MdCancel></div> : null}
                            </div>
                        )
                    }) : null}
                    
                </div>
                <h6 className="city-section-title">Notes</h6>
                <MdOutlineAddBox onClick={addNote} >add</MdOutlineAddBox>
                {clickNote === true ? <AddNotes setClickNote={setClickNote} setNoteDidUpdate={setNoteDidUpdate} cityId={cityId} /> : null}
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
                            {selectedValue === aNote.note && edit === true ? 
                                <EditNotes setEdit={setEdit} setNoteDidUpdate={setNoteDidUpdate} aNote={aNote} note={note}/> : <p className="city-text">{aNote.note}</p>}
                            
                            {selectedValue === aNote.note && edit === false ? 
                                <div className="city-icons"><BiEdit onClick={editSelection}></BiEdit>&nbsp;
                                    <MdCancel onClick={() => setSelectedValue(null)}></MdCancel></div> : null}
                            </div>
                        )
                    }) : null}
                </div>
            </section>
            <Link className="back-to-cities" to="/cities"><MdOutlineArrowBack></MdOutlineArrowBack>back</Link>
        </div>
    )

}
