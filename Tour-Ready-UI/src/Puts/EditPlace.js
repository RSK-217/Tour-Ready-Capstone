import React, {useState} from 'react'
import { MdCancel, MdCheck, MdDelete } from 'react-icons/md'

export default function EditPlace ({setEdit, setPlaceDidUpdate, aPlace}) {
  const [placeToEdit, setPlaceToEdit] = useState({
    id: aPlace.id,
    placeName: aPlace.placeName,
    cityId: aPlace.cityId
  });

  const Cancel = () => {
        setEdit(false)
      }
  
      const DeletePlace = () => {
        fetch(`https://localhost:7108/api/Place/${aPlace.id}`, {
            method: "DELETE"
        })
            .then(
                setEdit(false),
                setPlaceDidUpdate(true))
            
    }    
    const UpdatePlace = async (e) => {
        e.preventDefault();
        const newPlace = {
          id: placeToEdit.id,
          placeName: placeToEdit.placeName,
          cityId: placeToEdit.cityId,
        };
      
        try {
          await fetch(`https://localhost:7108/api/Place/${aPlace.id}`, {
            method: "PUT",
            headers: {
              "Access-Control-Allow-Origin": "https://localhost:7108",
              "Content-Type": "application/json",
            },
            body: JSON.stringify(newPlace),
          });
          setEdit(false);
          setPlaceDidUpdate(true);
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
                                    const copy = { ...placeToEdit }
                                    copy.placeName = e.target.value
                                    setPlaceToEdit(copy)
                                }}
                            type="text"
                            className="form-control"
                            value={placeToEdit.placeName}
                        />
                    </div>
                </fieldset>
    </form>
    <MdCheck onClick={UpdatePlace}></MdCheck>
    <MdDelete onClick={DeletePlace}></MdDelete>
    <MdCancel onClick={Cancel}></MdCancel>
    </div>
  )
}
