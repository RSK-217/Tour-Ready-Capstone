import React, {useState, useEffect} from 'react';
import {Link} from 'react-router-dom';
import { getAllGroups } from "../api/UserData";
import Groups from "./Groups";

export default function Authenticated({ user, currentUser }) {
  const [groups, setGroups] = useState([]);

    useEffect(() => {
        if(currentUser?.hasOwnProperty("id")) { 
         getAllGroups(currentUser).then((res) => {
         setGroups(res);
        })};
 }, [])
  
  return (
    <div className="text-center mt-5">
      <h1>Tour Ready</h1>
      <img
        referrerPolicy="no-referrer"
        src={user.photoURL}
        alt={user.displayName}
        />
        <h1>{currentUser.name}</h1>
        <h5>{currentUser.email}</h5>
      <div>
        <Groups className="groups-display" currentUser={currentUser} groups={groups} />
      </div>
          
    </div>
  );
}
