import React, { useState, Component, useEffect } from 'react';
import Calendar from 'react-calendar';
import 'react-calendar/dist/Calendar.css';

function MyCalendar() {
    const [value, onChange] = useState(new Date());
    const [itinerary, setItinerary] = useState([]);
    console.log("test: ", value.getDate())
    //var user = sessionStorage.getItem("userID")

    const fetchItinerary = async () => {
        const formData = new FormData()
        formData.append("userID", 3)

        var requestOptions = {
            method: "POST",
            headers: {
                'Content-type': 'application/json',
            },
            mode: 'cors'
        };

        await fetch(`https://localhost:9000/api/Calendar/GetItineraries/3`, requestOptions).then(
            response => console.log("Response: ", response)
        )
    }

    useEffect(() => {
        fetchItinerary();
    }, [])

    return (
        <div>
            {<Calendar onClickDay={onChange} value={value}                
            />}
            
        </div>
    );
}

export default MyCalendar;