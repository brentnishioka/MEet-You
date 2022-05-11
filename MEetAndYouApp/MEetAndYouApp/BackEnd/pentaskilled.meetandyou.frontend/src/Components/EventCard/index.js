import React, { useEffect, useState } from "react";
import LocationPin from "../LocationPin";
import DisplayLocationPin from "../LocationPin/DisplayLocationPin"
import useSessionData from "../hooks/useSessionData";

function EventCard({ event, itineraryID }) {
    const [userRating, setUserRating] = useState(null);
    const [respMessage, setRespMessage] = useState(null);
    const [fetchedEventRatings, setFetchedEventRatings] = useState(null);
    const [currentEventID] = useState(event.eventId);
    const [currentItineraryID] = useState(itineraryID);
    const { userID, token, roles } = useSessionData();

    // Validates the event ID.
    const isValidEventID = () => {
        if (currentEventID > 0) {
            // Validates the length of the data returned from fetching the user's itinerari
            return true;
        }
        else {
            return false;
        }
    }

    // Validates the itinerary ID.
    const isValidItineraryID = () => {
        if (itineraryID > 0) {
            return true;
        }
        else {
            return false;
        }
    }

    // Validates the event's rating.
    const isValidEventRating = () => {
        if (userRating >= 1 && userRating <= 5) {
            return true;
        }
        else {
            return false;
        }
    }

    // Makes an HTTP Get request to retrieve the user's event ratings.
    const fetchUserEventRating = async () => {
        var ratingRequestURL = `https://localhost:9000/api/Rating/GetUserEventRatings?itineraryID=${encodeURIComponent(isValidItineraryID && currentItineraryID)}`

        var ratingRequestOptions = {
            method: "GET",
            headers: {
                'Content-Type': 'application/json',
                'Access-Control-Allow-Origin': '*',
                'Access-Control-Allow-Credentials': true,
                'userID': userID,
                'token': token,
                'roles': roles
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

    const convertDate = () => {
        return(event.eventDate);
    }

    // Makes an HTTP Post request to post the user's rating for an event.
    const createUserEventRating = async () => {
        var requestURL = 'https://localhost:9000/api/Rating/PostRatingCreation'

        var requestOptions = {
            method: "POST",
            headers: {
                'Content-Type': 'application/json',
                'Access-Control-Allow-Origin': '*',
                'Access-Control-Allow-Credentials': true,
                'userID': userID,
                'token': token,
                'roles': roles
            },
            body: JSON.stringify({
                eventID: isValidEventID && currentEventID,
                itineraryID: isValidItineraryID && currentItineraryID,
                userRating: isValidEventRating && userRating
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

    // Makes an HTTP Put request to update the user's rating for an event.
    const modifyUserEventRating = async () => {
        var requestURL = 'https://localhost:9000/api/Rating/PutRatingModification'

        var requestOptions = {
            method: "PUT",
            headers: {
                'Content-Type': 'application/json',
                'Access-Control-Allow-Origin': '*',
                'Access-Control-Allow-Credentials': true,
                'userID': userID,
                'token': token,
                'roles': roles
            },
            body: JSON.stringify({
                eventID: isValidEventID && currentEventID,
                itineraryID: isValidItineraryID && currentItineraryID,
                userRating: isValidEventRating && userRating
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
            <p>Date: {new Date(event.eventDate).toLocaleString('en-US', {hour12: false})}</p>
            <p>Price: ${event.price === null ? '0' : event.price}</p>
        </div>
    );
}

export default EventCard;