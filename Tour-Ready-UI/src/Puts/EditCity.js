import React, { useState, useEffect } from "react";
import { useHistory, useParams } from 'react-router-dom';
import { Modal, Button } from 'react-bootstrap';
import "../styles/editCity.css";

export default function EditCity() {
    const [city, setCity] = useState({});
    const [showModal, setShowModal] = useState(false);

    const handleDelete = () => {
        setShowModal(true);
    };

    const handleClose = () => {
        setShowModal(false);
    };

    const handleConfirmDelete = () => {
        Delete();

    };

    const { cityId } = useParams();

    const history = useHistory();

    const cancelForm = () => {
        history.push(`/city/${cityId}`)
    }

    useEffect(() => {
        fetch(`https://localhost:7108/api/City/GetCityById/${cityId}`)
            .then(response => response.json())
            .then((data) => {
                setCity(data)
            })
    }, [cityId])

    const Delete = () => {
        fetch(`https://localhost:7108/api/City/${cityId}`, {
            method: "DELETE"
        })
            .then(history.push("/cities"))
            .then(history.go("/cities"))
    };



    const UpdateCity = async (e) => {
        e.preventDefault();
        const newCity = {
            id: city.id,
            userId: city.userId,
            cityName: city.cityName,
            state: city.state,
            country: city.country,
        };

        try {
            await fetch(`https://localhost:7108/api/City/${cityId}`, {
                method: "PUT",
                headers: {
                    "Access-Control-Allow-Origin": "https://localhost:7108",
                    "Content-Type": "application/json",
                },
                body: JSON.stringify(newCity),
            });
            history.push(`/city/${cityId}`);
        } catch (error) {
            console.error("There was a problem with the fetch operation:", error);
        }
    };



    return (
        <div className="edit-city-body">
            <form className="edit-city-form">
                <h2 className="edit-city-title">edit a city</h2>
                <fieldset>
                    <div className="form-group">city
                        <input
                            onChange={
                                (e) => {
                                    const copy = { ...city }
                                    copy.cityName = e.target.value
                                    setCity(copy)
                                }}
                            required autoFocus
                            type="text"
                            className="form-control"
                            value={city.cityName}
                        />
                    </div>
                </fieldset>
                <fieldset>
                    <div className="form-group">state/province
                        <input
                            onChange={
                                (e) => {
                                    const copy = { ...city }
                                    copy.state = e.target.value
                                    setCity(copy)
                                }}
                            required autoFocus
                            type="text"
                            className="form-control"
                            value={city.state}
                        />
                    </div>
                </fieldset>
                <fieldset>
                    <div className="form-group">country
                        <input
                            onChange={
                                (e) => {
                                    const copy = { ...city }
                                    copy.country = e.target.value
                                    setCity(copy)
                                }}
                            required autoFocus
                            type="text"
                            className="form-control"
                            value={city.country}
                        />
                    </div>
                </fieldset>
                <section className='city-btn'>
                    <button className="save-city-btn" onClick={UpdateCity}>
                        Save
                    </button>&nbsp;
                    <button className="cancel-city-btn" onClick={cancelForm}>
                        Cancel
                    </button>&nbsp;
                    <button className="delete-city-btn" onClick={Delete}>
                        Delete
                    </button>
                </section>
            </form>

            <Modal show={showModal} onHide={handleClose}>
                <Modal.Header closeButton>
                    <Modal.Title>Confirm Delete</Modal.Title>
                </Modal.Header>
                <Modal.Body>Are you sure you want to delete this city?</Modal.Body>
                <Modal.Footer>
                    <Button variant="secondary" onClick={handleClose}>
                        Cancel
                    </Button>
                    <Button variant="danger" onClick={handleConfirmDelete}>
                        Delete
                    </Button>
                </Modal.Footer>
            </Modal>
        </div>
    )

}