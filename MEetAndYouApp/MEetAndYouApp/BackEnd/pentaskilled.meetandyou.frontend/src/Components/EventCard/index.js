import React, { useEffect, useState } from "react";
import LocationPin from "../LocationPin";
import DisplayLocationPin from "../LocationPin/DisplayLocationPin"

function EventCard({ event, itineraryID }) {
    const [userRating, setUserRating] = useState(null);
    const [respMessage, setRespMessage] = useState(null);
    const [fetchedEventRatings, setFetchedEventRatings] = useState(null);
    const [currentEventID] = useState(event.eventId);
    const [currentItineraryID] = useState(itineraryID);

    const fetchUserEventRating = async () => {
        var ratingRequestURL = `https://localhost:9000/api/Rating/GetUserEventRatings?itineraryID=${encodeURIComponent(currentItineraryID)}`

        var ratingRequestOptions = {
            method: "GET",
            headers: {
                'Content-Type': 'application/json',
                'Access-Control-Allow-Origin': '*',
                'Access-Control-Allow-Credentials': true
            },
            mode: 'cors'
        };

        try {
            const rateRes = await fetch(ratingRequestURL, ratingRequestOptions)
            const ratingResponse = await rateRes.json()
            setFetchedEventRatings(ratingResponse.data);
            setRespMessage(ratingResponse.message);
        }
        catch (error) {
            console.log('error');
        }
    }

    const getCurrentEventRating = fetchedEventRatings && fetchedEventRatings.find((rating) => {
        if (rating.eventId === currentEventID)
        return(rating.userRating)
    })

    const createUserEventRating = async () => {
        var requestURL = 'https://localhost:9000/api/Rating/PostRatingCreation'

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
            await fetch(requestURL, requestOptions).then(
                response => console.log("System response: ", response.json())
            )
        }
        catch (error) {
            console.log('error');
        }
        
    }

    const modifyUserEventRating = async () => {
        var requestURL = 'https://localhost:9000/api/Rating/PutRatingModification'

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
            await fetch(requestURL, requestOptions).then(
                response => console.log("System response: ", response.json())
            )
        }
        catch (error) {
            console.log('error');
        }
    }

    useEffect(() => {
        fetchUserEventRating();
    }, [])

    useEffect(() => {
        if (getCurrentEventRating === undefined) {
            createUserEventRating();
        }
        else {
            modifyUserEventRating();
        }
    }, [userRating])

    return (
        <div>
            <h4>Event Name: {event.eventName}</h4>
            <DisplayLocationPin eventRating={getCurrentEventRating} />
            <LocationPin rating={userRating} onRating={(userRating) => setUserRating(userRating)} />
            <p>Address: {event.address}</p>
            <p>Description: {event.description}</p>
            <p>Date: {event.eventDate}</p>
            <p>Price: ${event.price}</p>
        </div>
    );
}

export default EventCard;