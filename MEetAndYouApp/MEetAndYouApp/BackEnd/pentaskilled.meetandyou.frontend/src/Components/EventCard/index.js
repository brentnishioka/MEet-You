import React, { useEffect, useState } from "react";
import LocationPin from "../LocationPin";

function EventCard({ event, itineraryID }) {
    const [userRating, setUserRating] = useState(null);
    const [currentEventID] = useState(event.eventId);
    const [currentItineraryID] = useState(itineraryID);
    
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
                itineraryID: currentItineraryID,
                userRating: userRating
            }),
            mode: 'cors'
        };

        try {
            await fetch('https://localhost:9000/api/Rating/PostRatingCreation', requestOptions).then(
                response => console.log("System response: ", response.json())
            )
        }
        catch (error) {
            console.log('error');
        }
        
    }

    const modifyUserEventRating = async () => {

        var requestOptions = {
            method: "PUT",
            headers: {
                'Content-Type': 'application/json',
                'Access-Control-Allow-Origin': '*',
                'Access-Control-Allow-Credentials': true
            },
            body: JSON.stringify({
                eventID: currentEventID,
                itineraryID: currentItineraryID,
                userRating: userRating
            }),
            mode: 'cors'
        };

        try {
            await fetch('https://localhost:9000/api/Rating/PutRatingModification', requestOptions).then(
                response => console.log("System response: ", response.json())
            )
        }
        catch (error) {
            console.log('error');
        }
    }

    useEffect(() => {
        // createUserEventRating();
        modifyUserEventRating();
    })

    return (
        <div>
            <h4>Event Name: {event.eventName}</h4>
            <LocationPin rating={userRating} onRating={(userRating) => setUserRating(userRating)} />
            <p>Address: {event.address}</p>
            <p>Description: {event.description}</p>
            <p>Date: {event.eventDate}</p>
            <p>Price: ${event.price}</p>
        </div>
    );
}

export default EventCard;