import React, { useEffect, useRef, useState } from "react";
import useSessionData from "../hooks/useSessionData";

function NoteComponent({ itineraryID }) {
    const [note, setNote] = useState(null);
    const [fetchedNoteContent, setFetchedNoteContent] = useState(null);
    const [isNoteLengthValid, setIsNoteLengthValid] = useState(true);
    const [noteResponseLength, setNoteResponseLength] = useState(0);
    const [respMessage, setRespMessage] = useState('');
    const [isSuccessful, setIsSuccessful] = useState(false);
    const noteInputBox = useRef(null);
    const { userID, token, roles } = useSessionData();

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
        const requestURL = `https://meetandyou.me:8001/api/Rating/GetUserNote?itineraryID=${encodeURIComponent(isValidItineraryID && itineraryID)}`

        var noteRequestOptions = {
            method: "GET",
            headers: {
                'Content-Type': 'application/json',
                'Access-Control-Allow-Origin': '*',
                'Access-Control-Allow-Credentials': true,
                'userID': userID,
                'token': token,
                'roles': roles
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

    // Makes an HTTP Post request to post the user's inputted note.
    const postUserNote = async () => {
        const requestURL = 'https://meetandyou.me:8001/api/Rating/PostNoteCreaton'

        var postNoteRequestOptions = {
            method: "POST",
            headers: {
                'Content-Type': 'application/json',
                'Access-Control-Allow-Origin': '*',
                'Access-Control-Allow-Credentials': true,
                'userID': userID,
                'token': token,
                'roles': roles
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
            setRespMessage(noteResponse.message)
            setIsSuccessful(noteResponse.isSuccessful)
        }
        catch (error) {
            console.log(error)
        }
    }

    // Makes an HTTP Put request to update the user's inputted note.
    const putUserNote = async () => {
        const requestURL = 'https://meetandyou.me:8001/api/Rating/PutNoteModification'

        var putNoteRequestOptions = {
            method: "PUT",
            headers: {
                'Content-Type': 'application/json',
                'Access-Control-Allow-Origin': '*',
                'Access-Control-Allow-Credentials': true,
                'userID': userID,
                'token': token,
                'roles': roles
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
            setRespMessage(noteResponse.message)
            setIsSuccessful(noteResponse.isSuccessful)
        }
        catch (error) {
            console.log(error)
        }
    }

    useEffect(() => {
        fetchUserNote();
    }, [])

    useEffect(() => {
        // if (noteResponseLength === 0) {
        //     postUserNote();
        // }
        // else {
        //     putUserNote();
        // }
        postUserNote();
    }, [note, respMessage])

    if (isNoteLengthValid) {
        return (
            <>
                <div>
                    <h5>Current Note Content:</h5>
                    <p>{getCurrentNoteContent}</p>
                </div>
                <div>
                    <textarea ref={noteInputBox} name="paragraph_text" cols="50" rows="10" maxLength="300" placeholder="There is a 300 maximum character limit on your note." />
                </div>
                <div style={{ padding: 15 }}>
                    <button onClick={e => handleClick(e)}>Submit Note</button>
                </div>
                <p style={isSuccessful ? { color: "green" } : { color: "red" }}>{respMessage}</p>
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