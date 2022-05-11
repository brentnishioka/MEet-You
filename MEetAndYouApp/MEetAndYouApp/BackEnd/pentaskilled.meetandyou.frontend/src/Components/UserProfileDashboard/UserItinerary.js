import React, { useEffect, useState } from "react";
import EventCard from "../EventCard";
import NoteComponent from "../NoteComponent/NoteComponent";

function Itinerary({id}) {
    const [itinInfo, setInfo] = useState(null)
    
    const getData = async () => {
        var itinRequestOptions = {
            method: "GET",
            headers: {
                'Content-Type': 'application/json',
                'Access-Control-Allow-Origin': '*',
                'Access-Control-Allow-Credentials': true
            },
            mode: 'cors'
        };


        try {
            const res = await fetch("https://localhost:9000/GetUPDData?id=" + id, itinRequestOptions);
            const apiRes = await res.json();

            var itin = apiRes.itineraries[0]
            setInfo(itin)


        } catch (error) {
            console.log("Error in")
        }
    }

    useEffect(() => {
        getData();
        
    }, [itinInfo]);

    const displayEvents = itinInfo && itinInfo.events.map((event) =>
        <div>
            <EventCard
                event={event}
                itineraryID={itinInfo.itineraryId}
            />
        </div>
    )


    return (
        <div >
            <button>
                <div >
                    <h1>{itinInfo && itinInfo.itineraryName}</h1>
                    {displayEvents}
                </div>
            </button>
        </div>
    );
}

export default Itinerary;