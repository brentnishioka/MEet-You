import React, { useState, Component } from 'react';
import Calendar from 'react-calendar';
import 'react-calendar/dist/Calendar.css';

function MyCalendar() {
    const [value, onChange] = useState(new Date());
    console.log(value.getDate())
    console.log("test: ", value.get)

    function getEvents() {
        fetch("https://localhost:9000")
    }

    return (
        <div>
            {<Calendar onClickDay={onChange} value={value}                
            />}
            
        </div>
    );

    


}

export default MyCalendar;