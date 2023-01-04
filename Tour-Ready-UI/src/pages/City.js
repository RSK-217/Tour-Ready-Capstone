import React, {useState, useEffect} from "react";
import { useParams, Link } from 'react-router-dom';
import { BiEdit } from "react-icons/bi";
import { MdOutlineArrowBack } from "react-icons/md";

export default function City() {
    const [city, setCity] = useState({});
    const { cityId } = useParams()

    useEffect(() => {
        fetch(`https://localhost:7108/api/City/GetCityById/${cityId}`)
        .then(response => response.json())
        .then((data) => {
            setCity(data)
        })
    }, [cityId])
    
    console.log(city);


    return (
        <>
        <h1 className="city-header">{city.cityName}, {city.state} - {city.country}</h1>
        <Link className='edit-city-link' to={`/city/edit/${cityId}`}><BiEdit></BiEdit>edit</Link>
            <div className="city-people">
                <h6>People</h6>
                <li>{city.people}</li>   
            </div>
            <div className="city-places">
                <h6>Places</h6>
                <li>{city.places}</li>   
            </div>
            <div className="city-notes">
                <h6>Notes</h6>
                <li>{city.cityNotes}</li>   
            </div>
            <Link className="back-to-cities" to="/cities"><MdOutlineArrowBack></MdOutlineArrowBack>back</Link>
        </>
    )

 }
