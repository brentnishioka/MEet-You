import React, { useState, Component, useEffect } from 'react';
import Calendar from 'react-calendar';
import 'react-calendar/dist/Calendar.css';



function MyCalendar() {
    const [date, setDate] = useState(new Date());
    const [itinerary, setItinerary] = useState([]);
    const [test, setTest] = useState(); 

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


    function showItineraries() {
        var rows = [];

        for (let i = 0; i < itinerary.length; i++) {
            
            for (let j = 0; j < itinerary[i].events.length; j++) {                
                rows.push(
                    <tr>
                        <td align='center'>{itinerary[i].events[j].eventName}</td>
                        <td align='center'>{itinerary[i].events[j].address}</td>
                        <td align='center'>{date.getFullYear() + "-" + date.getMonth() + "-" + date.getDate()}</td>
                        <td align='center'>{itinerary[i].events[j].description}</td>
                    </tr> 
                )        
            }
        }

        return (
            <table
                style={{ "borderCollapse": "collapse", "padding": "5px", "width": "50%", "border": "1px solid black" }}
                className="table table-hover">
                <thead style={{ "borderCollapse": "collapse", "padding": "5px", "width": "50%", "border": "1px solid black" }}>
                    <tr style={{ "borderCollapse": "collapse", "padding": "5px", "width": "50%", "border": "1px solid black" }}>
                        <th> Event</th>
                        <th> Address </th>
                        <th> Date </th>
                        <th> Description </th>
                    </tr>
                </thead>
                <tbody>
                    {rows}
                </tbody>
            </table>
        )
    }

    function showNoItineraries() {
        return (
            <div>
                You do not have any events planned for this day.
            </div>
        )
    }

    const displayItinerary = () => {
        if (itinerary.length !== 0) {
            console.log(itinerary);
            setTest(showItineraries());
            
        }
        else {
            console.log("no itineraries")
            setTest(showNoItineraries());
        }
    }

    useEffect(() => {
        displayItinerary();
    }, [itinerary]);

    return (
        <div>
            <Calendar onChange={setDate} value={date} /> 
            <button type="button" onClick={fetchItineraryAndDisplay}>See Events Planned For This Day</button> 

            {test}
        </div>
    );
}

export default MyCalendar;