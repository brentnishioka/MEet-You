import React, { useState, Component, useEffect } from 'react';
import Calendar from 'react-calendar';
import 'react-calendar/dist/Calendar.css';
//import './pages/react'

//import DateTime from 'react-datetime';
//import "react-datetime/css/react-datetime.css";


function MyCalendar() {
    const [date, setDate] = useState(new Date());
    const [itinerary, setItinerary] = useState([]);

    const fetchItineraryAndDisplay = async () => { //want to call this function everytime the user clicks a new day (convert to dateString first)
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
            //.then(response => response.text())
            //.then(body => console.log(body))
            .then(response => response.json())
            .then(response => setItinerary(response.data))
            
        
        console.log(itinerary);

        const rows = itinerary.map(item => (
            <tr>
                <td align='center'>{item.eventName}</td>
                <td align='center'>{item.address}</td>
                <td align='center'>{item.eventDate}</td>
            </tr>
        ));

        console.log("hi")
        return (
            <table
                style={{ "borderCollapse": "collapse", "padding": "5px", "width": "100%", "border": "1px solid black" }}
                className="table table-hover">
                <thead style={{ "borderCollapse": "collapse", "padding": "5px", "width": "100%", "border": "1px solid black" }}>
                    <tr style={{ "borderCollapse": "collapse", "padding": "5px", "width": "100%", "border": "1px solid black" }}>
                        <th> Event name</th>
                        <th> Address </th>
                        <th> Event Date </th>
                        {/* <th>Link to Sign-up</th> */}
                    </tr>
                </thead>
                <tbody>
                    {rows}
                </tbody>
            </table>
        )
    }

    return (
        <div>
            <Calendar onChange={setDate} value={date} />
            <button type="button" onClick={fetchItineraryAndDisplay}>See Itineraries</button>           
        </div>
    );
}

export default MyCalendar;