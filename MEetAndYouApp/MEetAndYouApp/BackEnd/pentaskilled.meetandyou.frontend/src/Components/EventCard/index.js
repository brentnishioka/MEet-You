import React, { useEffect, useState } from "react";
import LocationPin from "../LocationPin";

function EventCard(props) {
    const [userRating, setUserRating] = useState(null);
    const [currentEventID, setCurrentEventID] = useState(props.event.eventId);
    
    const createUserEventRating = async () => {

        var requestOptions = {
            method: "POST",
            headers: {
                'Content-Type': 'application/json',
                'Access-Control-Allow-Origin': '*',
                'Access-Control-Allow-Credentials': true
            },
            body: JSON.stringify({
                eventID: currentEventID,
                itineraryID: 7,
                userRating: userRating
            }),
            mode: 'cors'
        };

        await fetch('https://localhost:9000/api/Rating/PostRatingCreation', requestOptions).then(
            response => console.log("System response: ", response.json())
        )
    }

    useEffect(() => {
        createUserEventRating();
    })

    return (
        <div>
            <h4>Event Name: {props.event.eventName}</h4>
            <LocationPin rating={userRating} onRating={(userRating) => setUserRating(userRating)} />
            <p>Event ID: {props.event.eventID}</p>
            <p>Address: {props.event.address}</p>
            <p>Description: {props.event.description}</p>
            <p>Date: {props.event.eventDate}</p>
            <p>Price: ${props.event.price}</p>
        </div>
    );
}

export default EventCard;