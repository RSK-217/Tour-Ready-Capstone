import React, {useState, useEffect} from "react";
import { Link } from 'react-router-dom'
import { getAllCities } from "../api/UserData";

export default function Cities({ currentUser }) {
   const [cities, setCities] = useState([]);
   
   useEffect(() => {
       if(currentUser?.hasOwnProperty("id")) { 
        getAllCities(currentUser).then((res) => {
        setCities(res);
       })};
}, [])

console.log({currentUser});
console.log(cities);

return (
    <>
            <h1>Cities</h1>
            {cities ? cities.map((city) => {
                return (<li key={city.id} className='listed-city'>
                        <Link className='city-link' to={`/city/${city.id}`}>{city.cityName}, {city.state} - {city.country}</Link>
                       </li>)
            }) : null } 

            <Link className='add-city-link' to='/city/add'>+ add a city</Link>
        </>
    )
}


// const getAllCitiesByUserId = async () => {
//     const result = await fetch(`https://localhost:7108/api/City/GetAllCitiesByUserId/${user.id}`)
//     const jsonResult = result.json();

//     setCities(jsonResult)
// }

// getAllCitiesByUserId();