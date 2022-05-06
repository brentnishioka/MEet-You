import React, { useEffect, useState } from "react";
import EventCard from "../EventCard";
import NoteComponent from "../NoteComponent/NoteComponent";

function ItineraryComponent({ inputtedItinID }) {
    const [userItinerary, setUserItinerary] = useState(null);

    let userId = 5;

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
            setUserItinerary(itineraryResponse.data);
        }
        catch (error) {
            console.log('error');
        }
    }

    useEffect(() => {
        fetchItinerary();
    }, [inputtedItinID])

    if (!userItinerary) {
        return <>Loading Itinerary...</>;
    }

    const displayEvents = userItinerary[0].events.map((event) =>
        <div>
            <EventCard
                event={event}
                itineraryID={userItinerary[0].itineraryId}
            />
        </div>
    )

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

export default ItineraryComponent;