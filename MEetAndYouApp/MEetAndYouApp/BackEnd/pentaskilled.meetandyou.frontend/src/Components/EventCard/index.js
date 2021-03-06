import React, { useEffect, useState } from "react";
import LocationPin from "../LocationPin";
import DisplayLocationPin from "../LocationPin/DisplayLocationPin"
import useSessionData from "../hooks/useSessionData";

function EventCard({ event, itineraryID }) {
    const [userRating, setUserRating] = useState(null);
    const [respMessage, setRespMessage] = useState('');
    const [isSuccessful, setIsSuccessful] = useState(false);
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
        var ratingRequestURL = `https://meetandyou.me:8001/api/Rating/GetUserEventRatings?itineraryID=${encodeURIComponent(isValidItineraryID && currentItineraryID)}`

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
        var requestURL = 'https://meetandyou.me:8001/api/Rating/PostRatingCreation'

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
            const res = await fetch(requestURL, requestOptions);
            const ratingResponse = await res.json();
            setRespMessage(ratingResponse.message);
            setIsSuccessful(ratingResponse.isSuccessful);
        }
        catch (error) {
            console.log('error');
        }
        
    }

    // Makes an HTTP Put request to update the user's rating for an event.
    const modifyUserEventRating = async () => {
        var requestURL = 'https://meetandyou.me:8001/api/Rating/PutRatingModification'

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
            const res = await fetch(requestURL, requestOptions);
            const ratingResponse = await res.json();
            setRespMessage(ratingResponse.message);
            setIsSuccessful(ratingResponse.isSuccessful);
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
    }, [userRating, respMessage])

    return (
        <div>
            <h4 style={{margin: 10, textAlign: "center"}}>Event Name: {event.eventName}</h4>
            <DisplayLocationPin eventRating={getCurrentEventRating} />
            <LocationPin rating={userRating} onRating={(userRating) => setUserRating(userRating)} />
            <p style={isSuccessful ? { color: "green" } : { color: "red" }}>{respMessage}</p>
            <p style={{margin: 15}}>Address: {event.address}</p>
            <p style={{margin: 15}}>Description: {event.description}</p>
            <p style={{margin: 15}}>Date: {new Date(event.eventDate).toLocaleString('en-US', {hour12: false})}</p>
            <p style={{margin: 15}}>Price: ${event.price === null ? '0' : event.price}</p>
        </div>
    );
}

export default EventCard;