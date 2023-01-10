import React, { useState, useEffect } from "react";
import { useParams, Link } from 'react-router-dom';
import { BiEdit } from "react-icons/bi";
import { MdOutlineArrowBack } from "react-icons/md";
import "../styles/city.css";

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

    return (
        <div className="full-city-body">
            <h1 className="city-header">{city.cityName}, {city.state} - {city.country}</h1>
            <Link className='edit-city-link' to={`/city/edit/${cityId}`}><BiEdit></BiEdit>edit</Link>
            <section className="city-section-body">
                <div className="city-people">
                    <h6 className="city-section-title">People</h6>
                    <li className="city-section-content">{city.people}</li>
                </div>
                <div className="city-places">
                    <h6 className="city-section-title">Places</h6>
                    <li className="city-section-content">{city.places}</li>
                </div>
                <div className="city-notes">
                    <h6 className="city-section-title">Notes</h6>
                    <li className="city-section-content">{city.cityNotes}</li>
                </div>
            </section>
            <Link className="back-to-cities" to="/cities"><MdOutlineArrowBack></MdOutlineArrowBack>back</Link>
        </div>
    )

}
