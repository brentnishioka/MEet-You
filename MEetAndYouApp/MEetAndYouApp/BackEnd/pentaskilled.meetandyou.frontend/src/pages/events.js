import React, { useState, Component, useEffect } from 'react';
import { useLocation } from 'react-router-dom';
import '../apicall/site.js';

function Events() {
    const [data, setData] = useState([]);
    const [eventID, seteventID] = useState(0);
    const [itinID, setitintID] = useState(5);
    const [userID, setuserID] = useState("");
    const [postRes, setpostRes] = useState();

    const saveEvent = async (request, itinID, userID) => {
        try {
          const res = await fetch('https://localhost:9000/SaveEvent?itinID=' + itinID + '&userID=' + userID, request)
          const saveEventRes = await res.json();
          setpostRes(saveEventRes)
          console.log(saveEventRes)
        }
        catch (error) {
            console.log('error');
        }
      }

    const getData = async () => {
        try {
            const res = await fetch('https://localhost:9000/GetRandomEvent');
            const suggestionResponse = await res.json();
            setData(suggestionResponse.data);
            console.log(suggestionResponse);
        }
        catch (error) {
            console.log('error');
        }
    }

    useEffect(() => {
        getData();
    }, []);

    let counter = -1;
    const rows = data.map(item => (
        <tr>
            <td align='center'>{counter = counter + 1}</td>
            <td align='center'>{item.eventName}</td>
            <td align='center'>{item.address}</td>
            <td align='center'>{item.eventDate}</td>
            {/* <td align='center'>
          {item.link === 'TBD' ? <p>TBD</p> : 
          <button
          type="button"
          onClick={(e) => {
          e.preventDefault();
          window.open(item.link,"_blank");
        }}
          > Link to Tournament</button>}
          </td> */}
        </tr>
    ));

    const requestOptions = {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify({ title: 'React POST Request Example' })
      }

    // A method to create the event object
    const selectedEvent = (eventName) => {
        data.map(item => {
            if (item.eventName === eventName){
                console.log("Selected Object: ", item);
                return item;  
            }
            else {
                console.log("Selected Object: ")
                return null;
                
            }
        })
    }

    // Method to create event object using number
    const createEvent = () => {
        console.log("event num:", eventID);
        //const eIndex = parseInt(eventNumber)
        //console.log("index: ", eIndex)
        console.log(data[eventID]);
        return data[eventID];
    }

    const AddEvent = () => {
        const selectedEvent = createEvent();
        const requestOptions = {
            method: 'POST',
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify(selectedEvent)
          }

        const res = saveEvent(requestOptions, itinID, userID);
        console.log(res);
        return res;
    }

    //Function to submit data to the back end
    const addEvent = (userID, itinID, ) => {

    }

    return (
        <div>
            <table
                style={{ "borderCollapse": "collapse", "padding": "5px", "width": "100%", "border": "1px solid black" }}
                className="table table-hover">
                <thead style={{ "borderCollapse": "collapse", "padding": "5px", "width": "100%", "border": "1px solid black" }}>
                    <tr style={{ "borderCollapse": "collapse", "padding": "5px", "width": "100%", "border": "1px solid black" }}>
                        <th> No. </th>
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
            <label>
                <p>Enter an event ID </p>
                <input type="text" placeholder="ex: 5" maxlength="10" onChange={e => seteventID(e.target.value)} />
                {console.log("The value of eventID", eventID)}
            </label>
            <label>
                <p>Enter an itinerary ID </p>
                <input type="text" placeholder="ex: 2" maxlength="10" onChange={e => setitintID(e.target.value)} />
                {console.log("The value of itinID", itinID)}
            </label>
            <label>
                <p>Enter an your userID </p>
                <input type="text" placeholder="ex: 2" maxlength="10" onChange={e => setuserID(e.target.value)} />
                {console.log("The value of itinID", userID)}
            </label>
            <button type="button" id="SaveEvent" onClick={createEvent}> Save Event</button>
        </div>

    )
}
export default Events;