import React, { useState, Component, useEffect } from 'react';
import Calendar from 'react-calendar';
import 'react-calendar/dist/Calendar.css';
//import './pages/react'

//import DateTime from 'react-datetime';
//import "react-datetime/css/react-datetime.css";


function MyCalendar() {
    const [date, setDate] = useState(new Date());
    const [itinerary, setItinerary] = useState([]);

    const fetchItineraryAndDisplay = async () => { 
        const dateString = date.getFullYear() + "-" + date.getMonth() + "-" + date.getDate();

        var id = 9
        var requestOptions = {
            method: "POST",
            headerss: {
                'Content-type': 'application/json',
                'Accept': 'application/json, text/plain, */*'
            },
            mode: 'cors'
        };

        await fetch(`https://localhost:9000/Calendar?userID=` + id + "&date=" + dateString, requestOptions)
            .then(response => response.json())
            .then(response => setItinerary(response.data))         
    }

    const displayItinerary = () => {
        console.log("Test")
        if (itinerary.length !== 0) {
           console.log("itineraries found") 
        }
        else {
            console.log("no itineraries found");
        }
        //console.log("length", itinerary.length)
        console.log("itinerary", itinerary);
        //console.log("itinerary[0]", itinerary[0]);
        //console.log("itinerary[0].events: ", itinerary[0].events)
    }

    useEffect(() => {
        displayItinerary();
    }, [itinerary]);


    return (
        <div>
            <Calendar onChange={setDate} value={date} />
            <button type="button" onClick={fetchItineraryAndDisplay}>See Itineraries</button>           
        </div>
    );
}

export default MyCalendar;