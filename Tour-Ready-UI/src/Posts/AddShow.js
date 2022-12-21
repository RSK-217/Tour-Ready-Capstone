import React, {useState, useEffect} from "react";
import { useHistory } from 'react-router-dom'
import { getAllGroups } from "../api/UserData";

export default function AddShow({currentUser}) {
    const [show, setShow] = useState({});
    const [groups, setGroups] = useState([]);
    const [selectedGroupId, setSelectedGroupId] = useState({});
    const [selectedGroupName, setSelectedGroupName] = useState('');
    
    const history = useHistory()
    
    const cancelForm = () => {
        history.push('/shows')
    }
    
    const handleChange = (e) => {
        const int = parseInt(e.target.value)
        setSelectedGroupId(int)   
    }
    
    const getGroupName = () => {
        groups.map((group) => {
            selectedGroupId === group.id ? setSelectedGroupName(group.groupName) : ''
        })
    }
    
    useEffect(() => {
        getGroupName()
    }, [selectedGroupId])
    
    
    useEffect(() => {
        if(currentUser?.hasOwnProperty("id")) { 
            getAllGroups(currentUser).then((res) => {
                setGroups(res);
            })};
        }, [])
        

        const saveShow = async (e) => {
        e.preventDefault()
        const newShow = {
            userId: currentUser.id,
            groupId: selectedGroupId,
            groupName: selectedGroupName,
            venue: show.venue,
            showDate: show.showDate,
            cityName: show.cityName,
            state: show.state,
            country: show.country,
            setList: '',
            showNotes: '',
            merchSales: 0,
            payout: 0,
            isFavorite: false
        }

        const fetchOptions = {
            method: "POST",
            headers: {
                'Access-Control-Allow-Origin': 'https://localhost:7108',
                "Content-Type": "application/json"
            },
            body: JSON.stringify(newShow)
        }

        const response = await fetch('https://localhost:7108/api/Show', fetchOptions);
            await response.json();
            history.push('/shows');
    }

    return (
        <form className="add-show-form">
            <h2 className="add-show-title">add a show</h2>
            <fieldset>
                <div className="form-group">
                <select value={selectedGroupId} onChange={handleChange}>
                            <option value=''>select a group</option>
                    {groups.map((group) => {
                        return (
                            <option key={group.id} value={group.id}>{group.groupName}</option>
                        )
                    })}
                </select>
                </div>
            </fieldset>
            <fieldset>
                <div className="form-group">
                    <input
                        onChange={
                            (e) => {
                                const copy = { ...show }
                                copy.venue = e.target.value
                                setShow(copy)
                            }}
                        required autoFocus
                        type="text"
                        className="form-control"
                        placeholder="venue"
                    />
                </div>
            </fieldset>
            <fieldset>
                <div className="form-group">
                    <input
                        onChange={
                            (e) => {
                                const copy = { ...show }
                                copy.showDate = e.target.value
                                setShow(copy)
                            }}
                        required autoFocus
                        type="date"
                        className="form-control"
                        placeholder="YYYY/MM/DD"
                    />
                </div>
            </fieldset>
            <fieldset>
                <div className="form-group">
                    <input
                        onChange={
                            (e) => {
                                const copy = { ...show }
                                copy.cityName = e.target.value
                                setShow(copy)
                            }}
                        required autoFocus
                        type="text"
                        className="form-control"
                        placeholder="city"
                    />
                </div>
            </fieldset>
            <fieldset>
                <div className="form-group">
                    <input
                        onChange={
                            (e) => {
                                const copy = { ...show }
                                copy.state = e.target.value
                                setShow(copy)
                            }}
                        required autoFocus
                        type="text"
                        className="form-control"
                        placeholder="state/province"
                    />
                </div>
            </fieldset>
            <fieldset>
                <div className="form-group">
                    <input
                        onChange={
                            (e) => {
                                const copy = { ...show }
                                copy.country = e.target.value
                                setShow(copy)
                            }}
                        required autoFocus
                        type="text"
                        className="form-control"
                        placeholder="country"
                    />
                </div>
            </fieldset>
            <section className='add-show-btn'>
                <button className="show-btn" onClick={saveShow}>
                    Save
                </button>&nbsp;
                <button className="show-btn" onClick={cancelForm}>
                    Cancel
                </button>
            </section>
        </form>
    )
}