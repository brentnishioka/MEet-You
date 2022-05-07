import React, { useEffect, useRef, useState } from "react";

function NoteComponent({ itineraryID }) {
    const [note, setNote] = useState(null);
    const [fetchedNoteContent, setFetchedNoteContent] = useState(null);
    const [isNoteLengthValid, setIsNoteLengthValid] = useState(true);
    const [noteResponseLength, setNoteResponseLength] = useState(0);
    const noteInputBox = useRef(null);

    // Validates the length of the note text box.
    const isValidNoteLength = (length) => {
        if (length <= 300) {
            setIsNoteLengthValid(true);
        }
        else {
            setIsNoteLengthValid(false);
        }
    }

    // Validates the itinerary ID.
    const isValidItineraryID = () => {
        if (itineraryID > 0) {
            return true;
        }
        else {
            return false;
        }
    }

    const handleClick = (e) => {
        e.preventDefault();
        const input = noteInputBox;
        const currentTextInput = input.current.value;
        isValidNoteLength(currentTextInput.length);
        isNoteLengthValid && setNote(input.current.value);
    }

    // Makes an HTTP Get request to retrieve the user's notes.
    const fetchUserNote = async () => {
        const requestURL = `https://localhost:9000/api/Rating/GetUserNote?itineraryID=${encodeURIComponent(isValidItineraryID && itineraryID)}`

        var noteRequestOptions = {
            method: "GET",
            headers: {
                'Content-Type': 'application/json',
                'Access-Control-Allow-Origin': '*',
                'Access-Control-Allow-Credentials': true
            },
            mode: 'cors'
        };

        try {
            const res = await fetch(requestURL, noteRequestOptions)
            const noteResponse = await res.json();
            setFetchedNoteContent(noteResponse.data);

            // Input validation for the note's length
            setNoteResponseLength(noteResponse.data.length)
        }
        catch (error) {
            console.log(error)
        }
    }

    // Helper method to get the current itinerary's note content.
    const getCurrentNoteContent = fetchedNoteContent && fetchedNoteContent.map((content) => {
        if (content.itineraryId === itineraryID)
            return (content.noteContent)
    })

    const postUserNote = async () => {
        const requestURL = 'https://localhost:9000/api/Rating/PostNoteCreaton'

        var postNoteRequestOptions = {
            method: "POST",
            headers: {
                'Content-Type': 'application/json',
                'Access-Control-Allow-Origin': '*',
                'Access-Control-Allow-Credentials': true
            },
            body: JSON.stringify({
                itineraryID: isValidItineraryID && itineraryID,
                noteContent: isValidNoteLength && note
            }),
            mode: 'cors'
        };

        try {
            const res = await fetch(requestURL, postNoteRequestOptions)
            const noteResponse = await res.json()
            // console.log(noteResponse.data.message)
        }
        catch (error) {
            console.log(error)
        }
    }

    const putUserNote = async () => {
        const requestURL = 'https://localhost:9000/api/Rating/PutNoteModification'

        var putNoteRequestOptions = {
            method: "PUT",
            headers: {
                'Content-Type': 'application/json',
                'Access-Control-Allow-Origin': '*',
                'Access-Control-Allow-Credentials': true
            },
            body: JSON.stringify({
                itineraryID: isValidItineraryID && itineraryID,
                noteContent: isValidNoteLength && note
            }),
            mode: 'cors'
        };

        try {
            const res = await fetch(requestURL, putNoteRequestOptions)
            const noteResponse = await res.json()
        }
        catch (error) {
            console.log(error)
        }
    }

    useEffect(() => {
        fetchUserNote();
    }, [])

    useEffect(() => {
        if (noteResponseLength === 0) {
            postUserNote();
        }
        else {
            putUserNote();
        }
    }, [note])

    if (isNoteLengthValid) {
        return (
            <>
                <div>
                    <h5>Current Note Content:</h5>
                    <p>{getCurrentNoteContent}</p>
                </div>
                <div>
                    <textarea ref={noteInputBox} name="paragraph_text" cols="50" rows="10" maxLength="300" />
                </div>
                <div>
                    <button onClick={e => handleClick(e)}>Submit Note</button>
                </div>
            </>
        );
    }
    else {
        return (
            <>
                <div>
                    <h5>Current Note Content:</h5>
                </div>
                <p>Invalid note length.</p>
            </>
        );
    }
}


export default NoteComponent;