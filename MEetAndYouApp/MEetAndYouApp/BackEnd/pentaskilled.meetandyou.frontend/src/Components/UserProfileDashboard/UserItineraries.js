import React, { useState, useRef, useEffect } from "react";
import ItineraryComponent from "../ItineraryComponent";
import Itinerary from "./UserItinerary";

function Itineraries(){
    
    const [itineraryIDs,setIDs] = useState(null)

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
            const res = await fetch("https://meetandyou.me:8001/GetUPDData?id=5", itinRequestOptions);
            const apiRes = await res.json();
            
            
            var itin = apiRes.itineraries
            
            var ids = []
            for (let i = 0; i < itin.length; i++){
                ids.push(itin[i].itineraryId)
            }   
            setIDs(ids)

        } catch (error) {
            console.log("Error")
        }
    }

    const userItineraries = itineraryIDs && itineraryIDs.map((id) =>
        <Itinerary id={id} />

    )


    useEffect(() => {
        getData();
        
    },[itineraryIDs]);

    

    

    const itineraryStyle = {
        color: 'blue',
        padding: '0px 20px'
    }


    return (
        <div className={itineraryStyle}>
            <Itinerary id={5} />
            {/* {userItineraries} */}
        </div>
    );
};

export default Itineraries;