import React, { useState, useEffect } from "react";
import { useParams, Link } from 'react-router-dom';
import Moment from 'moment';
import { BiEdit } from "react-icons/bi";
import { MdOutlineArrowBack } from "react-icons/md";
import "../styles/show.css";

export default function Show() {
    const [show, setShow] = useState({});
    const { showId } = useParams()

    useEffect(() => {
        fetch(`https://localhost:7108/api/Show/GetShowById/${showId}`)
            .then(response => response.json())
            .then((data) => {
                setShow(data)
            })
    }, [showId])

    const formatDate = Moment(show.showDate).format('MM-DD-YYYY')

    console.log(show)

    return (
        <div className="show-body">
            <h1 className='show-headline'>{formatDate} - {show.venue} - {show.cityName}, {show.state} {show.country}</h1>
            <Link className='edit-show-link' to={`/show/edit/${showId}`}><BiEdit></BiEdit>edit</Link>
            <section className="show-section-body">
                <div>
                    <h5 className='show-section-title'>setlist:</h5>
                    <p className='show-section-content'>{show.setList}</p>
                </div>
                <div>
                    <h5 className='show-section-title'>show notes:</h5>
                    <p className='show-section-content'>{show.showNotes}</p>
                </div>
                <div>
                    <h5 className='show-section-title'>merch sales:</h5>
                    <p className='show-section-content'>${show.merchSales}</p>
                </div>
                <div>
                    <h5 className='show-section-title'>payout:</h5>
                    <p className='show-section-content'>${show.payout}</p>
                </div>
            </section>
            <Link className="back-to-shows" to="/shows"><MdOutlineArrowBack></MdOutlineArrowBack>back</Link>
        </div>
    )

}