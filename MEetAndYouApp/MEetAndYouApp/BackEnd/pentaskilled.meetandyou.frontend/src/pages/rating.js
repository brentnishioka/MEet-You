import React, { useState, useRef } from "react";
import ItineraryComponent from "../Components/ItineraryComponent";

function Rating() {
    const [userInputItinID, setUserInputItinID] = useState(null);
    const [isInputValid, setIsInputValid] = useState(true);
    const idInputForm = useRef(null);

    // Validates the itinerary ID.
    const isValidItineraryID = (id) => {
        if (!isNaN(id) && id > 0) {
            setIsInputValid(true);
        }
        else {
            setIsInputValid(false);
        }
    }

    const handleClick = (e) => {
        e.preventDefault();
        const input = idInputForm;
        const currentInput = input.current.value;
        isValidItineraryID(currentInput);
        isInputValid && setUserInputItinID(currentInput);
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
                <ItineraryComponent inputtedItinID={userInputItinID} isInputValid={isInputValid} />
            </div>
        </>
    );
};

export default Rating;