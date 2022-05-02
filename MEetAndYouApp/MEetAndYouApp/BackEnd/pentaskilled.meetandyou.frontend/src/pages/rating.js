
import React, { useEffect, useState } from "react";
// import Header from "components/Header";
import LocationPin from "../Components/LocationPin";
// import ExternalInfo from "components/ExternalInfo";

function Rating() {
    const [userRating, setUserRating] = useState(null);
    const [itinerary, setItinerary] = useState([]);

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

        await fetch('https://localhost:9000/api/Rating/GetUserItinerary?userID=5&itineraryID=7', requestOptions).then(
            response => console.log("System response: ", response.json())
        )
    }


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
        fetchItinerary();
        // createUserEventRating();
    })

    return (
        <div className="col text-center">
            <h2>Rate an Event</h2>
            <LocationPin rating={userRating} onRating={(userRating) => setUserRating(userRating)} />
            <p>The rating is {userRating}.</p>
        </div>
    );
};
//color={{filled: "rgb(136 87 25)", unfilled: "rgb(214 184 147)"}}
//count={10}
export default Rating;