import React, { useState } from "react";
import { MdCancel, MdDelete, MdCheck } from "react-icons/md";

export default function EditGroup( {setEditClick, setGroupsDidUpdate, group} ) {
    const [groupToEdit, setGroupToEdit] = useState({
        id: group.id,
        groupName: group.groupName,
        userId: group.userId
      });
    
      const Cancel = () => {
            setEditClick(false)
          }
      
          const DeleteGroup = () => {
            fetch(`https://localhost:7108/api/Group/${group.id}`, {
                method: "DELETE"
            })
                .then(
                    setEditClick(false),
                    setGroupsDidUpdate(true))
                
        }    
    
        const UpdateGroup = async (e) => {
          e.preventDefault();
          const newGroup = {
            id: groupToEdit.id,
            groupName: groupToEdit.groupName,
            userId: groupToEdit.userId,
            image: ""
          };
        
          try {
            await fetch(`https://localhost:7108/api/Group/${group.id}`, {
              method: "PUT",
              headers: {
                "Access-Control-Allow-Origin": "https://localhost:7108",
                "Content-Type": "application/json",
              },
              body: JSON.stringify(newGroup),
            });
            setEditClick(false);
            setGroupsDidUpdate(true);
          } catch (error) {
            console.error("There was a problem with the fetch operation:", error);
          }
        };
        
        
      return (
        <div>
        <form className="edit-city-form">
                    <fieldset>
                        <div className="form-group">
                            <input
                                onChange={
                                    (e) => {
                                        const copy = { ...groupToEdit }
                                        copy.groupName = e.target.value
                                        setGroupToEdit(copy)
                                    }}
                                type="text"
                                className="form-control"
                                value={groupToEdit.groupName}
                            />
                        </div>
                    </fieldset>
        </form>
        <MdCheck onClick={UpdateGroup}></MdCheck>
        <MdDelete onClick={DeleteGroup}></MdDelete>
        <MdCancel onClick={Cancel}></MdCancel>
        </div>
      )
    }
    
    