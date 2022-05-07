import React, { useEffect, useState } from "react";
import EventCard from "../EventCard";
import NoteComponent from "../NoteComponent/NoteComponent";

function ItineraryComponent({ inputtedItinID, isInputValid }) {
    const [userItinerary, setUserItinerary] = useState(null);
    const [isLengthValid, setIsLengthValid] = useState(null);

    let userId = 5;

    // Validates the length of the data returned from fetching the user's itineraries.
    const isValidDataLength = (length) => {
        if (length > 0) {
            setIsLengthValid(true);
        }
        else {
            setIsLengthValid(false);
        }
    }

    // Validates the user's ID.
    const isUserIDValid = () => {
        if (userId > 0) {
            return true;
        }
        else {
            return false;
        }
    }

    // Makes an HTTP Get request to retrieve the user's itineraries.
    const fetchItinerary = async () => {
        const requestURL = `https://meetandyou.me:8001/api/Rating/GetUserItinerary?userID=${encodeURIComponent(isUserIDValid && userId)}&itineraryID=${encodeURIComponent(inputtedItinID)}`

        var itinRequestOptions = {
            method: "GET",
            headers: {
                'Content-Type': 'application/json',
                'Access-Control-Allow-Origin': '*',
                'Access-Control-Allow-Credentials': true
            },
            mode: 'cors'
        };

        try {
            const itinRes = await fetch(requestURL, itinRequestOptions)
            const itineraryResponse = await itinRes.json()

            // Input validation for the data's length
            isValidDataLength(itineraryResponse.data.length)
            setUserItinerary(itineraryResponse.data);
        }
        catch (error) {
            console.log('error');
        }
    }

    useEffect(() => {
        isInputValid && fetchItinerary();
    }, [inputtedItinID])

    // Default content upon page load, changes once valid itinerary ID is provided.
    if (!userItinerary) {
        return <>Loading Itinerary...</>;
    }

    // Displays all the events on an itinerary.
    const displayEvents = isLengthValid && userItinerary[0].events.map((event) =>
        <div>
            <EventCard
                event={event}
                itineraryID={userItinerary[0].itineraryId}
            />
        </div>
    )

    if (isLengthValid) {
        return (
            <>
                <div>
                    <h2>Itinerary: {userItinerary[0].itineraryName}</h2>
                    <NoteComponent itineraryID={userItinerary[0].itineraryId}/>
                    {displayEvents}
                </div>
            </>
        );
    }
    else {
        return (
            <>Could not retrieve itinerary.</>
        );
    }

}

export default ItineraryComponent;