import React, { useState, Component, useEffect } from 'react';
import { useLocation } from 'react-router-dom';
import useSessionData from "../Components/hooks/useSessionData"

function Getrandomsuggestion(){
    const [data, setData] = useState([]);
    const { userID, token, roles } = useSessionData();
    
    // const getData = async () => {
    //     try {
    //         const res = await fetch('https://meetandyou.me:8001/GetRandomEvent');
    //         const suggestionResponse = await res.json();
    //         setData(suggestionResponse.data);
    //         console.log(suggestionResponse);
    //     }
    //     catch (error) {
    //         console.log('error');
    //     }
    // }

    // Makes an HTTP Get request to retrieve the random suggestions.
    const getData = async () => {
        const requestURL = `https://localhost:9000/GetRandomEvent`

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
            console.log('Fetch random suggestion failed');
        }
    }

    // Get random data
    useEffect(() => {
        getData();
    }, []);

    // construct a table for random events suggestions
    let counter = -1;
    const rows = data.map(item => (
        <tr>
            <td align='center'>{counter = counter + 1}</td>
            <td align='center'>{item.eventName}</td>
            <td align='center'>{item.address}</td>
            <td align='center'>{item.eventDate}</td>
        </tr>
    ));

    //return a table of randomevents
    return (
        <div>
            <h1>Random events</h1>
            <table
                style={{ "borderCollapse": "collapse", "padding": "5px", "width": "100%", "border": "1px solid black" }}
                className="table table-hover2">
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
        </div>

    )
}
export default Getrandomsuggestion;