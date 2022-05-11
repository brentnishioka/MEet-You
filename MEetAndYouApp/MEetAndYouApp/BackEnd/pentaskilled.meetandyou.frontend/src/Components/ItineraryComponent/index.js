import React, { useEffect, useState } from "react";
import EventCard from "../EventCard";
import NoteComponent from "../NoteComponent/NoteComponent";
import useSessionData from "../hooks/useSessionData"

function ItineraryComponent({ inputtedItinID, isInputValid }) {
    const [userItinerary, setUserItinerary] = useState(null);
    const [isLengthValid, setIsLengthValid] = useState(null);
    const { userID, token, roles } = useSessionData();

    let currentUserId = userID;

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
        if (currentUserId > 0) {
            return true;
        }
        else {
            return false;
        }
    }

    // Makes an HTTP Get request to retrieve the user's itineraries.
    const fetchItinerary = async () => {
        const requestURL = `https://localhost:9000/api/Rating/GetUserItinerary?itineraryID=${encodeURIComponent(inputtedItinID)}`

        var itinRequestOptions = {
            method: "GET",
            headers: {
                'Content-Type': 'application/json',
                'Access-Control-Allow-Origin': '*',
                'Access-Control-Allow-Credentials': true,
                'userID': currentUserId,
                'token': token,
                'roles': roles
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
        <div style={{padding: 5, margin: 10}}>
            <span style={{backgroundColor: "white"}}>
                <EventCard
                    event={event}
                    itineraryID={userItinerary[0].itineraryId}
                />
            </span>
        </div>
    )

    if (isLengthValid) {
        return (
            <>
                <div>
                    <h2 style={{padding: 15, margin: 10, textAlign: "center"}}>Itinerary: {userItinerary[0].itineraryName}</h2>
                    <NoteComponent itineraryID={userItinerary[0].itineraryId} />
                    <div>
                        {displayEvents}
                    </div>
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