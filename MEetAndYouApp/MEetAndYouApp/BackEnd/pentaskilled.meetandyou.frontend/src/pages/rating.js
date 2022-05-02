
import React, { useEffect, useState } from "react";
import LocationPin from "../Components/LocationPin";
import ItineraryComponent from "../Components/ItineraryComponent";

function Rating() {
    const [userRating, setUserRating] = useState(null);
    // const [userItinerary, setUserItinerary] = useState([]);

    const createUserEventRating = async () => {

        var requestOptions = {
            method: "POST",
            headers: {
                'Content-Type': 'application/json',
                'Access-Control-Allow-Origin': '*',
                'Access-Control-Allow-Credentials': true
            },
            body: JSON.stringify({
                eventID: 4,
                itineraryID: 7,
                userRating: userRating
            }),
            mode: 'cors'
        };

        await fetch('https://localhost:9000/api/Rating/PostRatingCreation', requestOptions).then(
            response => console.log("System response: ", response.json())
        )
    }

    useEffect(() => {
        // fetchItinerary();
        // createUserEventRating();
    })

    return (
        <>
            {/* <h2>Itinerary Name</h2> */}
            <div className="itineraryDiv">
                <ItineraryComponent />
            </div>
            <div className="col text-center">
                <h2>Rate an Event</h2>
                <LocationPin rating={userRating} onRating={(userRating) => setUserRating(userRating)} />
                <p>The rating is {userRating}.</p>
            </div>
        </>
    );
};

export default Rating;