import React, { useState, Component, useEffect } from 'react';

function Hyperlink() {
    const [data, setData] = useState([]);
    const [userID, setUserID] = useState();
    const [itinID, setItinID] = useState();
    const [email, setEmail] = useState("");
    const [permission, setPermission] = useState("")
    const [response, setResponse] = useState();

    const AddUser = async (request) => {
        try {
            const encodedEmail = encodeURIComponent(email);
            const res = await fetch('https://localhost:9000/AddUser?userID=' + userID + '&itineraryID=' + itinID + '&email=' + encodedEmail + '&permission=' + permission, requestOptions);
            const AddUserRes = await res.json();
            setResponse(AddUserRes)
            console.log(AddUserRes)
        }
        catch (error) {
            console.log('error');
        }
    }

    let counter = -1;
    const rows = data.map(item => (
        <tr>
            <td align='center'>{counter = counter + 1}</td>
            <td align='center'>{item.eventName}</td>
            <td align='center'>{item.address}</td>
            <td align='center'>{item.eventDate}</td>
        </tr>
    ));

    const requestOptions = {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' }
    }

    //Function to submit data to the back end
    //const AddUser = (itinID, userID, email, permission) => {

    //}

    return (
        <div>
            <table
                style={{ "borderCollapse": "collapse", "padding": "5px", "width": "100%", "border": "1px solid black" }}
                className="table table-hover">
                <thead style={{ "borderCollapse": "collapse", "padding": "5px", "width": "100%", "border": "1px solid black" }}>
                    <tr style={{ "borderCollapse": "collapse", "padding": "5px", "width": "100%", "border": "1px solid black" }}>
                        <th> Itinerary ID </th>
                        <th> User ID </th>
                        <th> Permission </th>
                    </tr>
                </thead>
                <tbody>
                    {rows}
                </tbody>
            </table>
            <label>
                <p>Enter your user ID</p>
                <input type="text" placeholder="User ID" maxLength="1" onChange={e => setUserID(e.target.value)} />
                {console.log("User ID:", userID)}
            </label>
            <label>
                <p>Enter an itinerary ID </p>
                <input type="text" placeholder="Itinerary ID" maxLength="1" onChange={e => setItinID(e.target.value)} />
                {console.log("Itinerary ID:", itinID)}
            </label>
            <label>
                <p>Enter an email </p>
                <input type="text" placeholder="Email" maxLength="30" onChange={e => setEmail(e.target.value)} />
                {console.log("Email:", email)}
            </label>
            <label>
                <p>Enter a permission (View/Edit) </p>
                <input type="text" placeholder="Permission" maxLength="30" onChange={e => setPermission(e.target.value)} />
                {console.log("Permission:", permission)}
            </label>

            <div>
                <button type="button" id="AddUser" onClick={AddUser}> Submit</button>
            </div>
        </div>

    )
}
export default Hyperlink;