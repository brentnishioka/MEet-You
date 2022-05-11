import React, { useState, Component, useEffect } from 'react';
import { useLocation } from 'react-router-dom';
import '../apicall/site.js';
import useSessionData from "../Components/hooks/useSessionData"

function Events() {
    const city = sessionStorage.getItem("city");
    const date = sessionStorage.getItem("date");
    const state = sessionStorage.getItem("state");
    const location = useLocation();
    
    const [data, setData] = useState([]);
    const [eventID, seteventID] = useState(0);
    const [itinID, setitintID] = useState(5);
    //const [userID, setuserID] = useState("");
    const [postRes, setpostRes] = useState();
    const [postMsg, setpostMsg] = useState();
    const { userID, token, roles } = useSessionData();

    const saveEvent = async (request, itinID, userID) => {
        try {
            const res = await fetch('https://localhost:9000/SaveEvent?itinID=' + itinID + '&userID=' + userID, request)
            const saveEventRes = await res.json();
            setpostRes(saveEventRes)
            console.log(saveEventRes)
        }
        catch (error) {
            console.log('error when saving events');
        }
    }

    // const getData = async () => {
    //     try {
    //         const res = await fetch('https://localhost:9000/GetRandomEvent');
    //         const suggestionResponse = await res.json();
    //         setData(suggestionResponse.data);
    //         console.log(suggestionResponse);
    //     }
    //     catch (error) {
    //         console.log('error');
    //     }
    // }

    // const getData = async () => {
    //     try {
    //         console.log("Category: ", location.state.categories.label)
    //         const res = await fetch("https://meetandyou.me:8001/GetEvent?category=" + location.state.categories.label + "&location=" + city + " " + state + "&date= " + date);
    //         const suggestionResponse = await res.json();
    //         setData(suggestionResponse.data);
    //         console.log(suggestionResponse);
    //     }
    //     catch (error) {
    //         console.log('error when getting suggestion', error.message);
    //     }
    // }

        // Makes an HTTP Get request to retrieve the user's itineraries.
        const getData = async () => {
            const requestURL = "https://localhost:9000/GetEvent?category=" + location.state.categories.label + "&location=" + city + " " + state + "&date= " + date
    
            var eventSugOptions = {
                method: "GET",
                headers: {
                    'Content-Type': 'application/json',
                    'Access-Control-Allow-Origin': '*',
                    'Access-Control-Allow-Credentials': true,
                    'userID': userID,
                    'token': token,
                    'roles': roles
                },
                mode: 'cors'
            };
    
            try {
                const res = await fetch(requestURL, eventSugOptions);
                const suggestionResponse = await res.json();
                setData(suggestionResponse.data);
                console.log(suggestionResponse);
    
                // Input validation for the data's length
                // isValidDataLength(itineraryResponse.data.length)
                // setUserItinerary(itineraryResponse.data);
            }
            catch (error) {
                console.log('Fetch user suggestion failed');
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
            if (item.eventName === eventName) {
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

    //Function to print response message
    function displayPostResponse() {
        if (postRes.isSuccessful === false) {
            setpostMsg(<p>Save selected event failed, please try again</p>)
        }
        else {
            setpostMsg(<p>Save selected event was successful</p>)
        }
    }

    const AddEvent = () => {
        const selectedEvent = createEvent();
        // Send an array of json to the back end
        const eventToSend =
            [
                {
                    eventName: selectedEvent.eventName,
                    description: selectedEvent.description,
                    address: selectedEvent.address,
                    price: selectedEvent.price,
                    eventDate: selectedEvent.eventDate,
                    categoryNames: [
                        {
                            categoryName: "Workshop"
                        }
                    ]
                }
            ]
        console.log("Event to be sent to the backend:", eventToSend)


        const requestOptions = {
            method: 'POST',
            headers: { 
                'Content-Type': 'application/json',
                'Access-Control-Allow-Origin': '*',
                'Access-Control-Allow-Credentials': true,
                'userID': userID,
                'token': token,
                'roles': roles
            },
            body: JSON.stringify(eventToSend)
        }

        const res = saveEvent(requestOptions, itinID, userID);
        setpostRes(res);
        console.log(res);
        displayPostResponse();
        console.log("The message: ", postMsg)
        return res;
    }


    //Function to submit data to the back end
    const addEvent = (userID, itinID,) => {

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
                <input type="text" placeholder="ex: 5" maxLength="10" onChange={e => seteventID(e.target.value)} />
                {console.log("The value of eventID", eventID)}
            </label>
            <label>
                <p>Enter an itinerary ID </p>
                <input type="text" placeholder="ex: 2" maxLength="10" onChange={e => setitintID(e.target.value)} />
                {console.log("The value of itinID", itinID)}
            </label>
            {/* <label>
                <p>Enter an your userID </p>
                <input type="text" placeholder="ex: 2" maxLength="10" onChange={e => setuserID(e.target.value)} />
                {console.log("The value of itinID", userID)}
            </label> */}
            <button type="button" id="SaveEvent" onClick={AddEvent}> Save Event</button>

            <p>Saving event response: </p>
            {/* {postMsg} */}
        </div>

    )
}
export default Events;