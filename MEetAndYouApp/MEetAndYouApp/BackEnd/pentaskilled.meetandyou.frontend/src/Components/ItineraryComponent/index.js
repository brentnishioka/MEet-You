import React, { useEffect, useState } from "react";
import EventCard from "../EventCard";

function ItineraryComponent({ inputtedItinID }) {
    const [userItinerary, setUserItinerary] = useState(null);
    const [eventRatings, setEventRatings] = useState(null);

    const fetchItinerary = async () => {

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
            const itinRes = await fetch(`https://localhost:9000/api/Rating/GetUserItinerary?userID=5&itineraryID=${encodeURIComponent(inputtedItinID)}`, itinRequestOptions)
            const itineraryResponse = await itinRes.json()
            setUserItinerary(itineraryResponse.data);
        }
        catch (error) {
            console.log('error');
        }
    }

    const fetchUserEventRatings = async () => {

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
            const rateRes = await fetch(`https://localhost:9000/api/Rating/GetUserEventRatings?itineraryID=${encodeURIComponent(inputtedItinID)}`, ratingRequestOptions)
            const ratingResponse = await rateRes.json()
            console.log(ratingResponse)
            setEventRatings(ratingResponse.data);
        }
        catch (error) {
            console.log('error');
        }
    }

    useEffect(() => {
        // setUserInputItinID(inputtedItinID)
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
                {displayEvents}
            </div>
        </>
    );
}

export default ItineraryComponent;