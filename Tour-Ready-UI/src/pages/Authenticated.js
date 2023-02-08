import React, { useState, useEffect } from 'react';
import { Link } from 'react-router-dom';
import { getAllGroups } from "../api/UserData";
import Groups from "./Groups";
import "../styles/home.css";


export default function Authenticated({ user, currentUser }) {
  const [groups, setGroups] = useState([]);
  const [groupsDidUpdate, setGroupsDidUpdate] = useState(false);

  useEffect(() => {
    if (currentUser?.hasOwnProperty("id")) {
      getAllGroups(currentUser).then((res) => {
        setGroups(res);
      });
    }
  }, []);

  useEffect(() => {
    if(groupsDidUpdate) {
    fetch(`https://localhost:7108/api/Group/GetAllGroupsByUserId/${currentUser.id}`)
    .then(response => response.json())
    .then((data) => {
      setGroups(data);
      setGroupsDidUpdate(false);
    });}
}, [groupsDidUpdate])

console.log(groupsDidUpdate)

  return (
    <div className='home-page-body'>
      <div className="text-left mt-10">
        <h1 className='home-title'>start your journey with tour <i>ready</i></h1>
        <section className='home-content'>
        <div>
        <img
          className='user-icon'
          referrerPolicy="no-referrer"
          src={user.photoURL}
          alt={user.displayName}
        />
        <h1 className='user-name'>{currentUser.name}</h1>
        <h5>{currentUser.email}</h5>
        </div>
        <div className='home-groups-section'>
          <Groups className="groups-display" currentUser={currentUser} setGroupsDidUpdate={setGroupsDidUpdate} setGroups={setGroups} groups={groups} />
        </div>
        
        </section>
      </div>
    </div>
  );
}
