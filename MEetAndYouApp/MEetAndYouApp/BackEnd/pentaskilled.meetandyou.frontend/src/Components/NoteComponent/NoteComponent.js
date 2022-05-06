import React, { useEffect, useRef, useState } from "react";

function NoteComponent({ itineraryID }) {
    const [note, setNote] = useState(null);
    const [fetchedNoteContent, setFetchedNoteContent] = useState(null);
    const noteInputBox = useRef(null);

    const handleClick = (e) => {
        e.preventDefault();
        const input = noteInputBox;
        setNote(input.current.value);
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

    // console.log('getCurrentNoteContent: ', getCurrentNoteContent)

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
            // console.log(noteResponse.data)
        }
        catch (error) {
            console.log(error)
        }
    }

    useEffect(() => {
        fetchUserNote();
    }, [])

    useEffect(() => {
        if (getCurrentNoteContent === undefined) {
            postUserNote();
        }
        else {
            putUserNote();
        }
    }, [note])

    return (
        <>
            <div>
                <h5>Current Note Content:</h5>
                <p>{getCurrentNoteContent}</p>
            </div>
            <div>
                <textarea ref={noteInputBox} name="paragraph_text" cols="50" rows="10" />
            </div>
            <div>
                <button onClick={e => handleClick(e)}>Submit Note</button>
            </div>
        </>
    );
}

export default NoteComponent;