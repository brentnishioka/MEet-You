import React, { useState, Component, useEffect } from 'react';
import { useLocation } from 'react-router-dom';
import '../apicall/site.js';
import useSessionData from "../Components/hooks/useSessionData"

function CreateItinerary(){
    const [itineraryOwner, setItineraryOwner] = useState();
    const [itineraryName, setItineraryName] = useState();
    const [rating, setRating] = useState();
    const [postMsg, setpostMsg] = useState();
    const [postRes, setpostRes] = useState();
    const { userID, token, roles } = useSessionData();

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
            setpostMsg( <p>Save selected itinerary failed, please try again</p>)
        }
        else {
            setpostMsg( <p>Save selected itinerary was successful</p>)
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
                itineraryOwner: userID,
            }
        ]
        // Send an array of json to the back end
        console.log("Event to be sent to the backend:", selectedItinerary)


        const requestOptions = {
            method: 'POST',
            headers: { 
                'Content-Type': 'application/json',
                'Access-Control-Allow-Origin': '*',
                'Access-Control-Allow-Credentials': true,
                'userID': userID,
                'token': token,
                'roles': roles
        },
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
        {/* <label>
            <p>Enter your ID create Itinerary </p>
            <input type="text" placeholder="ex: 5" maxlength="10" onChange={e => setItineraryOwner(e.target.value)} />
            {console.log("The value of ownerID", itineraryOwner)}
        </label> */}
        <label>
            <p>Enter an itinerary name </p>
            <input type="text" placeholder="ex: 2" maxlength="35" onChange={e => setItineraryName(e.target.value)} />
            {console.log("The value of itinName", itineraryName)}
        </label>
        <label>
            <p>Enter the rating for the itinerary</p>
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