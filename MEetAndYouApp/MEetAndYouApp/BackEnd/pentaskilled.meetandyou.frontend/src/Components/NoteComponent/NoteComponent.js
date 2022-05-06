import React, { useEffect, useRef, useState } from "react";

function NoteComponent({ itineraryID }) {
    const [note, setNote] = useState(null);
    const [fetchedNoteContent, setFetchedNoteContent] = useState(null);
    const [isNoteLengthValid, setIsNoteLengthValid] = useState(true);
    const noteInputBox = useRef(null);

    const checkNoteLength = (length) => {
        if (length <= 300) {
            setIsNoteLengthValid(true);
        }
        else {
            setIsNoteLengthValid(false);
        }
    }

    const handleClick = (e) => {
        e.preventDefault();
        const input = noteInputBox;
        const currentTextInput = input.current.value;
        checkNoteLength(currentTextInput.length);
        isNoteLengthValid && setNote(input.current.value);
    }

    const fetchUserNote = async () => {
        const requestURL = `https://localhost:9000/api/Rating/GetUserNote?itineraryID=${encodeURIComponent(itineraryID)}`

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
        }
        catch (error) {
            console.log(error)
        }
    }

    const getCurrentNoteContent = fetchedNoteContent && fetchedNoteContent.map((content) => {
        if (content.itineraryId === itineraryID)
        return(content.noteContent)
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
                itineraryID: itineraryID,
                noteContent: note
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
                itineraryID: itineraryID,
                noteContent: note
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
        if (isNoteLengthValid && getCurrentNoteContent === undefined) {
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
            <>Invalid note length.</>
        );
    }
}


export default NoteComponent;