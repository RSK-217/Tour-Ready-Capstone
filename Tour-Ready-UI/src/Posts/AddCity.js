import React, { useState } from "react";
import { useHistory } from 'react-router-dom';
import "../styles/addCity.css";

export default function AddCity({ currentUser }) {
    const [city, setCity] = useState({});

    const history = useHistory();

    const cancelForm = () => {
        history.push('/cities')
    }

    const saveCity = async (e) => {
        e.preventDefault()
        const newShow = {
            userId: currentUser.id,
            cityName: city.cityName,
            state: city.state,
            country: city.country,
        }

        const fetchOptions = {
            method: "POST",
            headers: {
                'Access-Control-Allow-Origin': 'https://localhost:7108',
                "Content-Type": "application/json"
            },
            body: JSON.stringify(newShow)
        }

        const response = await fetch('https://localhost:7108/api/City', fetchOptions);
        await response.json();
        history.push(`/cities`);
    }

    return (
        <div className="add-city-body">
            <form className="add-city-form">
                <h2 className="add-city-title">add a city</h2>
                <fieldset>
                    <div className="form-group">
                        <input
                            onChange={
                                (e) => {
                                    const copy = { ...city }
                                    copy.cityName = e.target.value
                                    setCity(copy)
                                }}
                            required autoFocus
                            type="text"
                            className="form-control"
                            placeholder="city"
                        />
                    </div>
                </fieldset>
                <fieldset>
                    <div className="form-group">
                        <input
                            onChange={
                                (e) => {
                                    const copy = { ...city }
                                    copy.state = e.target.value
                                    setCity(copy)
                                }}
                            required autoFocus
                            type="text"
                            className="form-control"
                            placeholder="state/province"
                        />
                    </div>
                </fieldset>
                <fieldset>
                    <div className="form-group">
                        <input
                            onChange={
                                (e) => {
                                    const copy = { ...city }
                                    copy.country = e.target.value
                                    setCity(copy)
                                }}
                            required autoFocus
                            type="text"
                            className="form-control"
                            placeholder="country"
                        />
                    </div>
                </fieldset>

                <section className='city-btn'>
                    <button className="save-city-btn" onClick={saveCity}>
                        Save
                    </button>&nbsp;
                    <button className="cancel-city-btn" onClick={cancelForm}>
                        Cancel
                    </button>
                </section>
            </form>
        </div>
    )
}