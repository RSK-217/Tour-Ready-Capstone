import React, { useState } from "react";
import { useHistory } from 'react-router-dom';

export default function AddGroup({currentUser, setClicked}) {
    const [group, setGroup] = useState();
    
    const history = useHistory();

    const cancelForm = () => {
        setClicked(false)
    }

    const saveGroup = async (e) => {
        e.preventDefault()
        const newGroup = {
            userId: currentUser.id,
            groupName: group.groupName,
            image: ""
        }

        const fetchOptions = {
            method: "POST",
            headers: {
                'Access-Control-Allow-Origin': 'https://localhost:7108',
                "Content-Type": "application/json"
            },
            body: JSON.stringify(newGroup)
        }

        const response = await fetch('https://localhost:7108/api/Group', fetchOptions);
            await response.json();
            history.push('/')
    }

    return (
        <form className="add-group-form">
            <fieldset>
                <div className="form-group">
                    <input
                        onChange={
                            (e) => {
                                const copy = { ...group }
                                copy.groupName = e.target.value
                                setGroup(copy)
                            }}
                        autoFocus
                        type="text"
                        className="form-control"
                        placeholder="group name"
                    />
                </div>
            </fieldset>
            <section className='add-group-btn'>
                <button className="group-btn" onClick={saveGroup}>
                    Save
                </button>&nbsp;
                <button className="group-btn" onClick={cancelForm}>
                    Cancel
                </button>
            </section>
        </form>
    )
}