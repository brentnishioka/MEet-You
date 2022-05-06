import React, { useEffect, useState } from "react";
import EventCard from "../EventCard";
import NoteComponent from "../NoteComponent/NoteComponent";

function ItineraryComponent({ inputtedItinID, isInputValid }) {
    const [userItinerary, setUserItinerary] = useState(null);
    const [isLengthValid, setIsLengthValid] = useState(null);

    let userId = 5;

    const validateLength = (length) => {
        if (length > 0) {
            setIsLengthValid(true);
        }
        else {
            setIsLengthValid(false);
        }
    }

    const fetchItinerary = async () => {
        const requestURL = `https://localhost:9000/api/Rating/GetUserItinerary?userID=${encodeURIComponent(userId)}&itineraryID=${encodeURIComponent(inputtedItinID)}`

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
            validateLength(itineraryResponse.data.length)
            setUserItinerary(itineraryResponse.data);
        }
        catch (error) {
            console.log('error');
        }
    }

    useEffect(() => {
        isInputValid && fetchItinerary();
    }, [inputtedItinID])

    if (!userItinerary) {
        return <>Loading Itinerary...</>;
    }

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