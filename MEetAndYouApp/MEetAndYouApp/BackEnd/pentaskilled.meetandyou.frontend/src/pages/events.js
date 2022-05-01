import React, { useState, Component, useEffect } from 'react';
import { useLocation } from 'react-router-dom';

function Events( ){
    const [data, setData] = useState([]);

    const getData = async () => {
        try{
            const res = await fetch('https://localhost:9000/GetRandomEvent');
            const suggestionResponse = await res.json();
            setData(suggestionResponse.data);
        }
        catch(error){
            console.log('error');
        }
    }

    useEffect(() => {
        getData();
      }, []);

      const rows = data.map(item => (
        <tr>
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

      return (
        <table
        style={{"borderCollapse": "collapse", "padding": "5px", "width": "100%", "border": "1px solid black"}}
       className="table table-hover">
        <thead style={{"borderCollapse": "collapse", "padding": "5px", "width": "100%", "border": "1px solid black"}}>
          <tr style={{"borderCollapse": "collapse", "padding": "5px", "width": "100%", "border": "1px solid black"}}>
            <th>Event name</th>
            <th> Address </th>
            <th> Event Date </th>
            {/* <th>Link to Sign-up</th> */}
          </tr>
        </thead>
          <tbody>
          {rows}
          </tbody>
      </table>
      )
}
export default Events;