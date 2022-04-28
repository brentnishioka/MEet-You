import React, { useState, Component, useEffect } from 'react';
import Calendar from 'react-calendar';
import 'react-calendar/dist/Calendar.css';
//import DateTime from 'react-datetime';
//import "react-datetime/css/react-datetime.css";


function MyCalendar() {
    const [value, onChange] = useState(new Date());
    const [itinerary, setItinerary] = useState([]);
    const [date, makeDate] = useState("");

    function createDateString() {
        let dateString = value.getFullYear() + "-" + value.getMonth() + "-" + value.getDate();
        makeDate(dateString);
        console.log("DATE STRING:", dateString);
    }

    
    //console.log("date: ",  value.)


    //var user = sessionStorage.getItem("userID")

    const fetchItinerary = async () => {
        const formData = new FormData()

        var requestOptions = {
            method: "POST",
            headers: {
                'Content-type': 'application/json',
            },
            mode: 'cors'
        };

        await fetch(`https://localhost:9000/Calendar?userID=3`, requestOptions).then(response => response.text())
            .then(body => console.log(body))
    }

    useEffect(() => {
        createDateString();
    }, [])

    return (
        <div style={{ justifyContent: 'center', backgroundColor: 'red', alignItems: 'center' }}>
            {<Calendar onClickDay={onChange} value={value}                
            />}
            
        </div>
    );
}

export default MyCalendar;