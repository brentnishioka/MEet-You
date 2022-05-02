import React, { useState, Component, useEffect } from 'react';
import { useLocation } from 'react-router-dom';
import '../apicall/site.js';

function CreateItinerary(){
    const [itineraryOwner, setItineraryOwner] = useState();
    const [itineraryName, setItineraryName] = useState();
    const [rating, setRating] = useState();
    const [postMsg, setpostMsg] = useState();
    const [postRes, setpostRes] = useState();

    // Post method to save the itinerary
    const saveItinerary = async (request) => {
        try {
            const res = await fetch('https://localhost:9000/AddItinerary', request)
            const saveItinRes = await res.json();
            setpostRes(saveItinRes)
            console.log(saveItinRes)
        }
        catch (error) {
            console.log('error');
        }
    }

    function displayPostResponse() {
        if (postRes.isSuccessful === false){
            setpostMsg( <p>Save selected event failed, please try again</p>)
        }
        else {
            setpostMsg( <p>Save selected event was successful</p>)
        }
    }

    //Function to create an Itinerary object

    //Function to Add itinerary on click
    const AddItinerary = () => {
        const selectedItinerary = 
        [
            {
                itineraryName: itineraryName,
                rating: rating,
                itineraryOwner: itineraryOwner,
            }
        ]
        // Send an array of json to the back end
        console.log("Event to be sent to the backend:", selectedItinerary)


        const requestOptions = {
            method: 'POST',
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify(selectedItinerary)
        }

        const res = saveItinerary(requestOptions);
        setpostRes(res);
        console.log(res);
        displayPostResponse();
        console.log("The message: ", postMsg)
        return res;
    }

return (
    <div>
        <h1>Create your Itinerary</h1>
        <label>
            <p>Enter your ID create Itinerary </p>
            <input type="text" placeholder="ex: 5" maxlength="10" onChange={e => setItineraryOwner(e.target.value)} />
            {console.log("The value of ownerID", itineraryOwner)}
        </label>
        <label>
            <p>Enter an itinerary name </p>
            <input type="text" placeholder="ex: 2" maxlength="35" onChange={e => setItineraryName(e.target.value)} />
            {console.log("The value of itinName", itineraryName)}
        </label>
        <label>
            <p>Enter the rating for he itinerary</p>
            <input type="text" placeholder="ex: 2" maxlength="10" onChange={e => setRating(e.target.value)} />
            {console.log("The value of rating", rating)}
        </label>
        <button type="button" id="SaveEvent" onClick={AddItinerary}> Save Event</button>

        <p>Saving itinerary response: </p>
        {postMsg}
    </div>
)
}
export default CreateItinerary;