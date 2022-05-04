import React, { useState, useRef } from "react";
import ItineraryComponent from "../Components/ItineraryComponent";

function Rating() {
    const [userInputItinID, setUserInputItinID] = useState(null);
    const idInputForm = useRef(null);

    function validateID(id) {
        try {
            Number.isInteger(id);
        }
        catch(error) {
            console.log('The inputted value is not a valid id.')
        }
    }

    const handleClick = (e) => {
        e.preventDefault();
        const input = idInputForm;
        setUserInputItinID(input.current.value);
    }

    return (
        <>
            <div className="formDiv">
                <form>
                    <fieldset>
                        <legend>Enter an itinerary ID</legend>
                        <input ref={idInputForm} placeholder="ID Number" />
                        <button onClick={e => handleClick(e)}>Get Itinerary</button>
                    </fieldset>
                </form>
            </div>
            <div className="itineraryDiv">
                <ItineraryComponent inputtedItinID={userInputItinID} />
            </div>
        </>
    );
};

export default Rating;