import React, { useState, useEffect } from "react";
import AddGroup from "../Posts/AddGroup";
import { Link } from "react-router-dom";
import { BiEdit } from "react-icons/bi";
import { MdCancel } from "react-icons/md";
import EditGroup from "../Puts/EditGroup";
import "../styles/home.css";

export default function Groups({ setGroupsDidUpdate, groups, currentUser }) {
    const [clicked, setClicked] = useState(false);
    const [editClick, setEditClick] = useState(false);
    const [selectedValue, setSelectedValue] = useState(null);

    const handleChange = () => {
        setClicked(true)
    }

    const valueChange = (e) => {
        setSelectedValue(e.target.value);
      };

    const editForm = () => {
        setEditClick(true)
    }
    
    return (
        <>
            <h1 className="group-title">Groups</h1>
            <div className="city-section-box">
                    {groups ? groups.map((group) => {
                        return (
                            <div className="city-section-content" key={group.id}>
                            <input
                                type="radio"
                                value={group.groupName}
                                onChange={valueChange}
                                checked={selectedValue === group.GroupName}
                            />
                            {selectedValue === group.groupName && editClick === true ? 
                                <EditGroup setEditClick={setEditClick} setGroupsDidUpdate={setGroupsDidUpdate} group={group}/> : <p className="city-text">{group.groupName}</p>}
                            
                            {selectedValue === group.groupName && editClick === false ? 
                                <div className="city-icons"><BiEdit onClick={editForm}></BiEdit>&nbsp;
                                    <MdCancel onClick={() => setSelectedValue(null)}></MdCancel></div> : null}
                                </div>
                        )
                    }) : null}
                    
                </div> 
            <button className="add-group-btn" type="button" onClick={handleChange} >Add Group</button>
            {clicked === true ? <AddGroup currentUser={currentUser} setGroupsDidUpdate={setGroupsDidUpdate} setClicked={setClicked} /> : null}


        </>
    )
}