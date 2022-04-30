import React, { useState, Component, useEffect } from 'react';
import { useLocation } from 'react-router-dom';




export default function Events() {
    const city = sessionStorage.getItem("city");
    const date = sessionStorage.getItem("date");
    const state = sessionStorage.getItem("state");
    const location = useLocation();
    const category = location.state.categories[0].label;

    
    const [eventSuggestions, getSuggestions] = useState([]);

    console.log(location.state.categories[0].label)


    const fetchSuggestions = async (category) => {
        await fetch("https://localhost:9000/GetEvent?category=" + location.state.categories[0].label + "&location=" + city + " " + state + "&date= " + date)
            .then(response => response.text())
            .then(body => console.log(body))
    }

    useEffect(() => {
        fetchSuggestions();
    }, [])

    return (
        < div >
            LONGLONGLONG DISPLAY YOUR SUGGESTIONS HERE
        </div>
    )
}