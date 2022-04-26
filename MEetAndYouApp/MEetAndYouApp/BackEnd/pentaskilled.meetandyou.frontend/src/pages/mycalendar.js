import React, { useState } from 'react';
import Calendar from 'react-calendar';
import 'react-calendar/dist/Calendar.css';

function MyCalendar() {
    const [value, onChange] = useState(new Date());

    return (
        <div>
            {<Calendar onChange={onChange} value={value} />}
            ASJKDFHASDJKLFHASKLFHKLSDHAFSLDJK   
        </div>
    );
}

export default MyCalendar;