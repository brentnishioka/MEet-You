import React from "react";

function EventCard(props) {
    
    return (
        <div>
            <h4>Event Name: {props.event.eventName}</h4>
            <p>Address: {props.event.address}</p>
            <p>Description: {props.event.description}</p>
            <p>Date: {props.event.eventDate}</p>
            <p>Price: ${props.event.price}</p>
        </div>
    );
}

export default EventCard;