import React, { useState, useEffect }from "react";
import { useParams, useHistory } from "react-router-dom";

export default function EditGroup() {
    const [group, setGroup] = useState({})

    const { groupId } = useParams();

    const history = useHistory();

    const cancelForm = () => {
        history.push("/")
    }

    useEffect(() => {
        fetch(`https://localhost:7108/api/Group/GetGroupById/${groupId}`)
        .then(response => response.json())
        .then((data) => {
            setGroup(data)
        })
 }, [groupId])

    const Delete = () => {
        fetch(`https://localhost:7108/api/Group/${groupId}`, {
        method: "DELETE"
        })
        .then(history.push("/"))
        .then(history.go())
    }


 const UpdateGroup = (e) => {
    e.preventDefault()
    const newGroup = {
        id: group.id,
        userId: group.userId,
        groupName: group.groupName,
        image: group.image
        
    }
    
    fetch(`https://localhost:7108/api/Group/${groupId}`, {
    method: "PUT",
    headers: {
        'Access-Control-Allow-Origin': 'https://localhost:7108',
        'Content-Type': 'application/json'
    },
    body: JSON.stringify(newGroup)
})
    .then(() => {
        history.push(`/`)
    })
}


    return (
        <form className="edit-group-form">
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
                        value={group.groupName}
                    />
                </div>
            </fieldset>
            <section className='add-group-btn'>
                <button className="group-btn" onClick={UpdateGroup}>
                    Save
                </button>&nbsp;
                <button className="group-btn" onClick={cancelForm}>
                    Cancel
                </button>&nbsp;
                <button className="group-btn" onClick={Delete}>
                    Delete
                </button>
            </section>
        </form>
    )
}