import React, {useState, useEffect} from "react";
import AddGroup from "../Posts/AddGroup";
import { Link } from "react-router-dom";
import { BiEdit } from "react-icons/bi";
import EditGroup from "../Puts/EditGroup";

export default function Groups({ groups, currentUser }) {
    const [clicked, setClicked] = useState(false)
    const [clickEdit, setClickEdit] = useState(false)
    const [reset, setReset] = useState([])

    const handleChange = () => {
        setClicked(true)
    }

    // const editChange = (e) => {
    //    e.target.value === 1 ? setClickEdit(true) : null
    // }
  
     return (
        <>
            <h1>Groups</h1> 
            <button className="add-group-btn" type="button" onClick={handleChange} >Add Group</button>
            {clicked === true ? <AddGroup currentUser={currentUser} setClicked={setClicked}/> : null}
            {groups ? groups.map((group) => {
                return (
                <div key={group.id}>
                    <h6>* {group.groupName}</h6>
                    
                    {/* <button value={group.id} key={group.id} className="edit-group-btn" type="button" onClick={editChange}>Edit Group</button> */}
                    
                    <Link className="edit-group-link" to={`/group/edit/${group.id}`}><BiEdit></BiEdit></Link>
                </div>
            )}) : null }
            
            
        </>
    )
}