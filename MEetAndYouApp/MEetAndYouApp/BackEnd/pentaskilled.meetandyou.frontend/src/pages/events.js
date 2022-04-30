import React, { useState, Component, useEffect } from 'react';
import { useLocation } from 'react-router-dom';




export default function Events() {
    const city = sessionStorage.getItem("city");
    const date = sessionStorage.getItem("date");
    const state = sessionStorage.getItem("state");
    const location = useLocation();
    const category = location.state.categories[0].label;

    
    const [eventSuggestions, setSuggestions] = useState([]);

    console.log(location.state.categories[0].label)


    const fetchSuggestions = async (category) => {
        await fetch("https://localhost:9000/GetEvent?category=" + location.state.categories[0].label + "&location=" + city + " " + state + "&date= " + date)
            .then(response => setSuggestions(response.json()))
            .then(body => console.log(body))
    }
    

    useEffect(() => {
        fetchSuggestions();
    }, [])

    console.log("TEST LONG: ", eventSuggestions)
    const { data } = eventSuggestions;
    return (
        <div>
            {/*{data.map((item) => (
                <div>
                    <h1>{item.eventName}</h1>
                    <p>{item.description}</p>
                    <p>{item.address}</p>
                    <p>{item.eventDate}</p>
                </div>
            ))}*/}
            {eventSuggestions.data}
            
        </div>
    );
}