import React, { useState, useRef, useEffect } from "react";
import ItineraryComponent from "../ItineraryComponent";
import Itinerary from "./UserItinerary";

function Itineraries(){
    const [itineraries, setItineraries] = useState(null);
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
            const res = await fetch("https://localhost:9000/GetUPDData?id=5", itinRequestOptions);
            const apiRes = await res.json();
            setItineraries(apiRes);
            
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



    useEffect(() => {
        getData();
        
        
    },[]);



    // console.log(itineraries.itineraries)
    

   
    return (
        <>
            <Itinerary data={itineraries} />
        </>
    );
};

export default Itineraries;