
import React, { useEffect, useState } from "react";
// import Header from "components/Header";
import LocationPin from "../Components/LocationPin";
// import ExternalInfo from "components/ExternalInfo";

function Rating() {
    const [userRating, setUserRating] = useState(null);

    const createUserEventRating = async () => {
        // const formData = new FormData()
        // formData.append("eventID", 4) // hard coded for now
        // formData.append("itineraryID", 7) // hard coded for now
        // formData.append("userRating", userRating)

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
        createUserEventRating();
    })

    return (
        <div className="row">
            <div className="col text-center">
                <h2>Rate an Event</h2>
                <LocationPin rating={userRating} onRating={(userRating) => setUserRating(userRating)} />
                <p>The rating is {userRating}.</p>
            </div>
        </div>
    );
};
//color={{filled: "rgb(136 87 25)", unfilled: "rgb(214 184 147)"}}
//count={10}
export default Rating;