import React, { useState, useEffect } from "react";
import { Link } from 'react-router-dom'
import { getAllCities } from "../api/UserData";
import { MdOutlineAddBox } from "react-icons/md";
import "../styles/cities.css";
import CityFilter from "../Filters/CityFilter";

export default function Cities({ currentUser }) {
    const [cities, setCities] = useState([]);
    const [filter, setFilter] = useState({});

    const filterCities = () => {
       if (filter === 'city') {
            return cities.sort(function (a, b) {
                var nameA = a.cityName.toUpperCase(); 
                var nameB = b.cityName.toUpperCase(); 
                if (nameA < nameB) {
                    return -1; 
                }
                if (nameA > nameB) {
                    return 1; 
                }
                return 0; 
            });
        }
        else if (filter === 'state') {
            return cities.sort(function (a, b) {
                var nameA = a.state.toUpperCase();
                var nameB = b.state.toUpperCase();
                if (nameA < nameB) {
                    return -1; 
                }
                if (nameA > nameB) {
                    return 1; 
                }
                return 0; 
            });
        }
        else if (filter === 'country') {
            return cities.sort(function (a, b) {
                var nameA = a.country.toUpperCase(); 
                var nameB = b.country.toUpperCase(); 
                if (nameA < nameB) {
                    return -1; 
                }
                if (nameA > nameB) {
                    return 1; 
                }
                return 0;
            });
        }
        return cities
    }

    useEffect(() => {
        if (currentUser?.hasOwnProperty("id")) {
            getAllCities(currentUser).then((res) => {
                setCities(res);
            })
        };
    }, [])

    return (
        <div className="city-body">
            <h1 className="city-title">Cities</h1>
            <CityFilter value={filter} setFilter={setFilter} />
            {filterCities().map((city) => {
                return (<li key={city.id} className='listed-city'>
                    <Link className='city-link' to={`/city/${city.id}`}>{city.cityName}, {city.state} - {city.country}</Link>
                </li>)
            })}

            <Link className='add-city-link' to='/city/add'><MdOutlineAddBox></MdOutlineAddBox>add</Link>
        </div>
    )
}
