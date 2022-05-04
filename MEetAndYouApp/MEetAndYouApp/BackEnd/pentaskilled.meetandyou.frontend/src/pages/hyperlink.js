import React, { useState } from 'react';

function Hyperlink() {
    const [data, setData] = useState([]);
    const [emailData, setEmailData] = useState([]);
    const [userID, setUserID] = useState();
    const [itinID, setItinID] = useState();
    const [email, setEmail] = useState("");
    const [permission, setPermission] = useState("")
    const [message, setMessage] = useState("");

    const AddUser = async (request) => {
        const requestOptions = {
            method: 'POST',
            headers: { 'Content-Type': 'application/json' }
        }

        console.log("User ID:", userID)
        console.log("Itinerary ID:", itinID)
        console.log("Email:", email)
        console.log("Permission:", permission)

        try {
            const encodedEmail = encodeURIComponent(email);
            const res = await fetch('https://localhost:9000/AddUser?userID=' + userID + '&itineraryID=' + itinID + '&email=' + encodedEmail + '&permission=' + permission, requestOptions);
            const AddUserRes = await res.json();
            setData(AddUserRes.data)
            setEmailData(AddUserRes.emails)
            setMessage(AddUserRes.message)
            console.log(AddUserRes)
        }
        catch (error) {
            console.log('error');
        }
    }

    const RemoveUser = async (request) => {
        const requestOptions = {
            method: 'DELETE',
            headers: { 'Content-Type': 'application/json' }
        }

        console.log("User ID:", userID)
        console.log("Itinerary ID:", itinID)
        console.log("Email:", email)
        console.log("Permission:", permission)

        try {
            const encodedEmail = encodeURIComponent(email);
            const res = await fetch('https://meetandyou.me:8001/RemoveUser?userID=' + userID + '&itineraryID=' + itinID + '&email=' + encodedEmail + '&permission=' + permission, requestOptions);
            const DeleteUserRes = await res.json();
            setData(DeleteUserRes.data)
            setEmailData(DeleteUserRes.emails)
            setMessage(DeleteUserRes.message)
            console.log(DeleteUserRes)
        }
        catch (error) {
            console.log('error');
        }
    }

    let index = -1;
    const rows = data.map(item => (
        <tr>
            <td align='center'>{item.itineraryId}</td>
            <td align='center'>{item.userId}</td>
            <td align='center'>{emailData[index = index + 1]}</td>
            <td align='center'>{item.permissionName}</td>
        </tr>
    ));

    return (
        <div>
            <div>
            <table
                style={{ "borderCollapse": "collapse", "padding": "5px", "width": "100%", "border": "1px solid black" }}
                className="table table-hover">
                <thead style={{ "borderCollapse": "collapse", "padding": "5px", "width": "100%", "border": "1px solid black" }}>
                    <tr style={{ "borderCollapse": "collapse", "padding": "5px", "width": "100%", "border": "1px solid black" }}>
                        <th> Itinerary ID </th>
                        <th> User ID </th>
                        <th> Email </th>
                        <th> Permission </th>
                    </tr>
                </thead>
                <tbody>
                    {rows}
                </tbody>
                </table>
            </div>
            <label>
                <p>Enter your user ID:</p>
                <input type="number" placeholder="User ID" onChange={e => setUserID(e.target.value)} />
            </label>
            <label>
                <p>Enter an itinerary ID: </p>
                <input type="number" placeholder="Itinerary ID" maxLength="50" onChange={e => setItinID(e.target.value)} />
            </label>
            <label>
                <p>Enter an email: </p>
                <input type="text" placeholder="Email" maxLength="30" onChange={e => setEmail(e.target.value)} />
            </label>
            <label>
                <p>Enter a permission: (View/Edit) </p>
                <input type="text" placeholder="Permission" maxLength="30" onChange={e => setPermission(e.target.value)} />
            </label>

            <p></p>

            <div>
                <button type="button" id="AddUser" onClick={AddUser}> Add</button> &nbsp;&nbsp;&nbsp;
                <button type="button" id="RemoveUser" onClick={RemoveUser}> Remove</button>
            </div>
            
            <p>Modifying user itinerary response: </p>
            {message}
        </div>

    )
}
export default Hyperlink;