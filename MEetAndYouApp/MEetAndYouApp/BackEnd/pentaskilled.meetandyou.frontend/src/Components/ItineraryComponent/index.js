import React, { useEffect, useState } from "react";
import EventCard from "../EventCard";

function ItineraryComponent({ itineraryID }) {
    const [userItinerary, setUserItinerary] = useState(null);
    // const [userInputItinID] = useState(itineraryID);

    const fetchItinerary = async () => {

        var requestOptions = {
            method: "GET",
            headers: {
                'Content-Type': 'application/json',
                'Access-Control-Allow-Origin': '*',
                'Access-Control-Allow-Credentials': true
            },
            mode: 'cors'
        };

        try {
            const res = await fetch(`https://localhost:9000/api/Rating/GetUserItinerary?userID=5&itineraryID=7`, requestOptions)
            const itineraryResponse = await res.json()
            setUserItinerary(itineraryResponse.data);
        }
        catch (error) {
            console.log('error');
        }
    }

    useEffect(() => {
        fetchItinerary();
    }, [])

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
                {displayEvents}
            </div>
        </>
    );
}

export default ItineraryComponent;