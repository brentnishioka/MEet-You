import React, { useState } from "react";
import ItineraryComponent from "../Components/ItineraryComponent";

function Rating() {
    const [userInputItinID, setUserInputItinID] = useState(null);

    function handleSubmit(e) {
        e.preventDefault();
    }

    return (
        <>
            <div className="formDiv">
                <form onSubmit={handleSubmit}>
                    <fieldset>
                        <legend>Enter an itinerary ID</legend>
                        <input onChange={e => setUserInputItinID(e.target.value)} placeholder="ID Number" />
                        <button>Get Itinerary</button>
                    </fieldset>
                </form>
            </div>
            <div className="itineraryDiv">
                <ItineraryComponent itineraryID={userInputItinID}/>
            </div>
        </>
    );
};

export default Rating;