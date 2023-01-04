import React, {useState, useEffect} from "react";
import { Link } from 'react-router-dom';
import Moment from "moment";
import { getAllShows } from "../api/UserData";
import { getAllGroups } from "../api/UserData";
import { ShowFilter } from "../Filters/ShowFilter";
import { MdOutlineAddBox } from "react-icons/md";
import "../styles/shows.css";

export default function Shows({ currentUser }) {
    const [shows, setShows] = useState([]);
    const [groups, setGroups] = useState([]);
    const [selctedGroup, setSelectedGroup] = useState([]);
    const [filter, setFilter] = useState('');
    const [dateFilter, setDateFilter] = useState('');

    
    const filterShows = () => {
        if (filter > 0) {
            return shows.filter(s => s.groupId === filter)
        } else {
            return shows
        }
    }
    console.log(filterShows())
    
    useEffect(() => {
        if(currentUser?.hasOwnProperty("id")) { 
            getAllShows(currentUser).then((res) => {
                setShows(res);
            })};
        }, [])
        

    useEffect(() => {
        if(currentUser?.hasOwnProperty("id")) { 
         getAllGroups(currentUser).then((res) => {
         setGroups(res);
        })};
    }, [])

 console.log(shows)
    
     return (
        <>
            <h1>Shows</h1>
            <ShowFilter className="show-filter" groups={groups} value={filter} setFilter={setFilter} />
            {filterShows().map((show) => {
                return (<li key={show.id} className='listed-show'>
                        <Link className='show-link' to={`/show/${show.id}`}>{Moment(show.showDate).format('MM-DD-YYYY')} - {show.venue} - {show.cityName}, {show.state} {show.country}</Link>
                       </li>)
            })} 

            <Link className='add-show-link' to='/show/add'><MdOutlineAddBox></MdOutlineAddBox>add show</Link>
            
        </>
    )
}