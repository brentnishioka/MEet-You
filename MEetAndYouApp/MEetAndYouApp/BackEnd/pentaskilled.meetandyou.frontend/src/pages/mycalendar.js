import React, { useState, Component, useEffect } from 'react';
import Calendar from 'react-calendar';
import 'react-calendar/dist/Calendar.css';
//import './pages/react'

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
    console.log("value test: ", value)

    //var user = sessionStorage.getItem("userID")

    const fetchItinerary = async () => {
        makeDate(value)
        console.log("DATE TEST", date);
        //const formData = new FormData()
        var id = 3
        var requestOptions = {
            method: "POST",
            headerss: {
                'Content-type': 'application/json',
                'Accept': 'application/json, text/plain, */*'
            },
            //body: JSON.stringify({userID: 3}),
            mode: 'cors'
        };

        //.https://localhost:9000/Calendar?userID=9&date=2022-04-28
        await fetch(`https://localhost:9000/Calendar?userID=`+id + "&date=" + date, requestOptions).then(response => response.text())
            .then(body => console.log(body))
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