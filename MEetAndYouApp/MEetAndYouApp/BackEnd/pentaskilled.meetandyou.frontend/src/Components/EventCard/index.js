import React, { useEffect, useState } from "react";
import LocationPin from "../LocationPin";
import DisplayLocationPin from "../LocationPin/DisplayLocationPin"

function EventCard({ event, itineraryID }) {
    const [userRating, setUserRating] = useState(null);
    const [fetchedEventRatings, setFetchedEventRatings] = useState(null);
    const [currentEventRating, setCurrentEventRating] = useState(null);
    const [currentEventID] = useState(event.eventId);
    const [currentItineraryID] = useState(itineraryID);

    const fetchUserEventRating = async () => {

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
            const rateRes = await fetch(`https://localhost:9000/api/Rating/GetUserEventRatings?itineraryID=${encodeURIComponent(currentItineraryID)}`, ratingRequestOptions)
            const ratingResponse = await rateRes.json()
            setFetchedEventRatings(ratingResponse.data);
            setCurrentEventRating(ratingResponse.data.userRating)
        }
        catch (error) {
            console.log('error');
        }
    }

    // console.log(fetchedEventRatings && fetchedEventRatings[0].userRating)
    
    // const updateEventRatings = fetchedEventRatings && fetchedEventRatings.map((rating) => {
    //     if(currentEventID === rating.eventId && currentItineraryID === rating.itineraryId)
    //     setCurrentEventRating(rating.userRating);
    // })

    const getCurrentEventRating = fetchedEventRatings && fetchedEventRatings.indexOf(currentEventRating)


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
        fetchUserEventRating();
        // createUserEventRating();
        // modifyUserEventRating();
    }, [])

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