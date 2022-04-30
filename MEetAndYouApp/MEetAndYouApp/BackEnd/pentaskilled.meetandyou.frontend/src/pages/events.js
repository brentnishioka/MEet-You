import React from 'react';
import { useLocation } from 'react-router-dom';



export default function Events() {
    const location = useLocation();

    console.log("location test in events: ", location.state.location)
    console.log("category test in events: ", location.state.category)



    return (
        < div >
            LONGLONGLONG
        </div>
    )
}