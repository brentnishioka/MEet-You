import React, { useState, Component, useEffect } from 'react';
import Calendar from 'react-calendar';
import 'react-calendar/dist/Calendar.css';
import ICalendarLink from "react-icalendar-link";
import '../images/calendarStyling.css'
import Select from 'react-select';

//import Button from 'react-bootstrap/Button';
//import 'bootstrap/dist/css/bootstrap.min.css';
//import 'bootstrap/dist/css/bootstrap-grid.min.css';

function MyCalendar() {
    const [date, setDate] = useState(new Date());
    const [itinerary, setItinerary] = useState([]);
    const [test, setTest] = useState();
    const [event, setEvent] = useState();
    const [eventCal, setEventCal] = useState()

    const fetchItinerary = async () => {
        console.log("MONTH", date.getMonth());
        //const dateString = date.getFullYear() + "-" + (date.getMonth() + 1) + "-" + date.getDate();
        const dateString = date.getFullYear() + "-" + (date.getMonth() + 1) + "-" + date.getDate();
        console.log("datestring: ", dateString)

<<<<<<< HEAD
        var id = 3;
=======
        var id = 9;
>>>>>>> 2c047419d934f1c6d5002e8dd1fb0541121c21c1
        //var id = sessionStorage.getItem("userID")
        var requestOptions = {
            method: "POST",
            headerss: {
                'Content-type': 'application/json',
                'Accept': 'application/json, text/plain, */*'
            },
            mode: 'cors'
        };
        console.log(date);
        await fetch(`https://meetandyou.me:8001/Calendar?userID=` + id + "&date=" + dateString, requestOptions)
            .then(response => response.json())
            .then(response => setItinerary(response.data))
    }

    const handleChange = (e) => {
        setEvent(e);
        setEventCal(addToItin(e));
    }

    function addToItin(e) {
        var index = e.value;
        var allEvents = [];
        for (let i = 0; i < itinerary.length; i++) {
            for (let j = 0; j < itinerary[i].events.length; j++) {
                allEvents.push(
                    [itinerary[i].events[j].eventName,
                    itinerary[i].events[j].address,
                        date.getFullYear() + "-" + (date.getMonth() + 1) + "-" + date.getDate(),
                    itinerary[i].events[j].description]
                )
            }
        }

        var test = allEvents[index]

        const eventtest = {
            title: test[0],
            description: test[3],
            startTime: test[2],
            endTime: test[2],
            location:  test[1],
            attendees: [
                "No other attendees",
            ]
        }    

        return (
            <div className = "add-calendar">             
                <ICalendarLink event={eventtest}> Add to calendar </ICalendarLink>
            </div>
        )       
    }
    

    function eventOptions() {
        var eventNames = [];
        let counter = 0;
        for (let i = 0; i < itinerary.length; i++) {
            for (let j = 0; j < itinerary[i].events.length; j++) {
                eventNames.push({
                    label: itinerary[i].events[j].eventName, value: counter++
                })
            }
        }
            
        return (
            <div className = "select">
                <p></p>
                Which event would you like export as an ics file ?
                <Select
                   
                    options={eventNames}
                    closeMenuOnSelect={true}
                    onChange={handleChange} />
            </div>       
        )
    }


    function showItineraries() {
        var rows = [];
        for (let i = 0; i < itinerary.length; i++) {            
            for (let j = 0; j < itinerary[i].events.length; j++) {                
                rows.push(
                    <tr>
                        <td align='center'>{itinerary[i].events[j].eventName}</td>
                        <td align='center'>{itinerary[i].events[j].address}</td>
                        <td align='center'>{date.getFullYear() + "-" + (date.getMonth() + 1) + "-" + date.getDate()}</td>
                        <td align='center'>{itinerary[i].events[j].description}</td>
                    </tr> 
                )        
            }
        }

        return (
            <div>
            <table
                style={{ "borderCollapse": "collapse", "padding": "5px", "width": "60%", "border": "1px solid black" }}
                className="table-hover">
                <thead style={{ "borderCollapse": "collapse", "padding": "5px", "width": "60%", "border": "1px solid black" }}>
                    <tr style={{ "borderCollapse": "collapse", "padding": "5px", "width": "60%", "border": "1px solid black" }}>
                    
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
            {eventOptions()}                
            </div>
            
        )
    }

    function showNoItineraries() {
        setEventCal(null);
        return (
            <div className="no-events">
                You do not have any events planned for this day.             
            </div>
        )
    }

    const displayItinerary = () => {
        if (itinerary.length !== 0) {
            setTest(showItineraries());
            
        }
        else {
            setTest(showNoItineraries());
        }
    }

    useEffect(() => {
        displayItinerary();
    }, [itinerary]);


    return (
        <div>    
            <Calendar onChange={setDate} value={date} />         
            <div className="see-events-button" style={{ float: 'left' }}  >
                <button type="button" onClick={fetchItinerary}>See Events Planned For This Day</button>
            </div>
            {test}
            {eventCal}
        </div>
    );
}

export default MyCalendar;