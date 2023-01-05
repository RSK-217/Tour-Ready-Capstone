import React, { useState, useEffect } from "react";
import { useHistory } from 'react-router-dom'
import { getAllGroups } from "../api/UserData";
import { useParams } from "react-router-dom";
import { Modal, Button } from 'react-bootstrap';
import "../styles/editShow.css";

export default function EditShow({ currentUser }) {
    const [show, setShow] = useState({});
    const [groups, setGroups] = useState([]);
    const [currentGroup, setCurrentGroup] = useState({});
    const [showModal, setShowModal] = useState(false);
    const handleClose = () => setShowModal(false);
    const handleShow = () => setShowModal(true);
    const { showId } = useParams()

    const history = useHistory()

    const CancelForm = () => {
        history.push(`/show/${showId}`)
    }

    const handleChange = (e) => {
        const int = parseInt(e.target.value)
        groups.map((group) => {
            group.id === int ? setCurrentGroup(group) : null
        })
        return currentGroup
    }

    useEffect(() => {
        groups.map((group) => {
            group.id === show.groupId ? setCurrentGroup(group) : null
        })
    }, [show])

    console.log(currentGroup)

    useEffect(() => {
        if (currentUser?.hasOwnProperty("id")) {
            getAllGroups(currentUser).then((res) => {
                setGroups(res);
            })
        };
    }, [])


    useEffect(() => {
        fetch(`https://localhost:7108/api/Show/GetShowById/${showId}`)
            .then(response => response.json())
            .then((data) => {
                setShow(data)
            })
    }, [showId])

    const Delete = () => {
        fetch(`https://localhost:7108/api/Show/${showId}`, {
            method: "DELETE"
        })
            .then(history.push("/shows"))
            .then(history.go())
    }

    console.log(show)

    const UpdateShow = (e) => {
        e.preventDefault()
        const newShow = {
            id: show.id,
            userId: show.userId,
            groupId: currentGroup.id,
            groupName: currentGroup.groupName,
            venue: show.venue,
            showDate: show.showDate,
            cityName: show.cityName,
            state: show.state,
            country: show.country,
            setList: show.setList,
            showNotes: show.showNotes,
            merchSales: show.merchSales,
            payout: show.payout,
            isFavorite: show.isFavorite
        }

        fetch(`https://localhost:7108/api/Show/${showId}`, {
            method: "PUT",
            headers: {
                'Access-Control-Allow-Origin': 'https://localhost:7108',
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(newShow)
        })
            .then(() => {
                history.push(`/show/${showId}`)
            })
    }


    return (
        <div className="edit-show-body">
            <form className="edit-show-form">
                <h2 className="edit-show-title">edit a show</h2>
                <fieldset>
                    <div className="form-group-select">
                        <select onChange={handleChange}>
                            <option value={currentGroup.groupName}>{currentGroup.groupName}</option>
                            {groups.map((group) => {
                                return (
                                    <>
                                        {group.id !== currentGroup.id ?
                                            <option key={group.id} value={group.id}>{group.groupName}</option> : null}
                                    </>
                                )
                            })}
                        </select>
                    </div>
                </fieldset>
                <fieldset>
                    <div className="form-group">venue
                        <input
                            onChange={
                                (e) => {
                                    const copy = { ...show }
                                    copy.venue = e.target.value
                                    setShow(copy)
                                }}
                            autoFocus
                            type="text"
                            className="form-control"
                            value={show.venue || ""}
                        />
                    </div>
                </fieldset>
                <fieldset>
                    <div className="form-group">date
                        <input
                            onChange={
                                (e) => {
                                    const copy = { ...show }
                                    copy.showDate = e.target.value
                                    setShow(copy)
                                }}
                            autoFocus
                            type="date"
                            className="form-control"
                            value={show.showDate}
                        />
                    </div>
                </fieldset>
                <fieldset>
                    <div className="form-group">city
                        <input
                            onChange={
                                (e) => {
                                    const copy = { ...show }
                                    copy.cityName = e.target.value
                                    setShow(copy)
                                }}
                            autoFocus
                            type="text"
                            className="form-control"
                            value={show.cityName}
                        />
                    </div>
                </fieldset>
                <fieldset>
                    <div className="form-group">state/province
                        <input
                            onChange={
                                (e) => {
                                    const copy = { ...show }
                                    copy.state = e.target.value
                                    setShow(copy)
                                }}
                            autoFocus
                            type="text"
                            className="form-control"
                            value={show.state}
                        />
                    </div>
                </fieldset>
                <fieldset>
                    <div className="form-group">country
                        <input
                            onChange={
                                (e) => {
                                    const copy = { ...show }
                                    copy.country = e.target.value
                                    setShow(copy)
                                }}
                            autoFocus
                            type="text"
                            className="form-control"
                            value={show.country}
                        />
                    </div>
                </fieldset>
                <fieldset>
                    <div className="form-group">setlist
                        <textarea
                            onChange={
                                (e) => {
                                    const copy = { ...show }
                                    copy.setList = e.target.value
                                    setShow(copy)
                                }}
                            autoFocus
                            type="text"
                            className="form-control"
                            placeholder="setlist"
                            value={show.setList || ""}
                        />
                    </div>
                </fieldset>
                <fieldset>
                    <div className="form-group">notes
                        <textarea
                            onChange={
                                (e) => {
                                    const copy = { ...show }
                                    copy.showNotes = e.target.value
                                    setShow(copy)
                                }}
                            autoFocus
                            type="text"
                            className="form-control"
                            placeholder="show notes"
                            value={show.showNotes || ""}
                        />
                    </div>
                </fieldset>
                <fieldset>
                    <div className="form-group">merch sales
                        <input
                            onChange={
                                (e) => {
                                    const copy = { ...show }
                                    copy.merchSales = e.target.value
                                    setShow(copy)
                                }}
                            autoFocus
                            type="text"
                            className="form-control"
                            placeholder="merch sales"
                            value={show.merchSales}
                        />
                    </div>
                </fieldset>
                <fieldset>
                    <div className="form-group">payout
                        <input
                            onChange={
                                (e) => {
                                    const copy = { ...show }
                                    copy.payout = e.target.value
                                    setShow(copy)
                                }}
                            autoFocus
                            type="text"
                            className="form-control"
                            placeholder="payout"
                            value={show.payout}
                        />
                    </div>
                </fieldset>
                <section className='show-btn'>
                    <button className="save-show-btn" onClick={UpdateShow}>
                        Save
                    </button>&nbsp;
                    <button className="cancel-show-btn" onClick={CancelForm}>
                        Cancel
                    </button>&nbsp;
                    <button className="delete-show-btn" onClick={Delete}>
                        Delete
                    </button>
                </section>
            </form>
            <Modal show={showModal} onHide={handleClose}>
                <Modal.Header closeButton>
                    <Modal.Title>Delete Show</Modal.Title>
                </Modal.Header>
                <Modal.Body>Are you sure you want to delete this show?</Modal.Body>
                <Modal.Footer>
                    <Button variant="secondary" onClick={handleClose}>
                        Close
                    </Button>
                    <Button variant="primary" onClick={Delete}>
                        Delete
                    </Button>
                </Modal.Footer>
            </Modal>

        </div>
    )
}
