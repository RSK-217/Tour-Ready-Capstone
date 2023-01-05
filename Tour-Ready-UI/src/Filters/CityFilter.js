import React from 'react'
import "../styles/cities.css";

const CityFilter = ({value, setFilter}) => {

    const handleChange = (e) => {
        const filteredCity = e.target.value
        setFilter(filteredCity)
    }

  return (
    <select className='city-filter' value={value} onChange={handleChange}>
        <option value=''>sort by</option>
        <option value='city'>City</option>
        <option value='state'>State/Province</option>
        <option value='country'>Country</option>
    </select>
  )
}

export default CityFilter