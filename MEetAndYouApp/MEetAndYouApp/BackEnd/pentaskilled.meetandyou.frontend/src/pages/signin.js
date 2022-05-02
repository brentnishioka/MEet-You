import React, { useState } from 'react'
import { useNavigate } from "react-router-dom";

export default function Login() {
    const [email, setEmail] = useState("");
    const [password, setPassword] = useState("");
    const navigate = useNavigate();
    const [isLoggedIn, setIsLoggedIn] = useState();
    


    // Post method to save the itinerary
    const saveItinerary = async (request) => {
        try {
            const res = await fetch("https://localhost:9000/Login/SignIn?userEmail=" + email + "&userPassword=" + password, request)
            const saveItinRes = await res.json();
            sessionStorage.setItem("token", saveItinRes.token);
            sessionStorage.setItem("userID", saveItinRes.userID);
            sessionStorage.setItem("roles", saveItinRes.roles);
            setIsLoggedIn(true);
            console.log(saveItinRes)
        }
        catch (error) {
            console.log('error');
        }
        navigate("/");
    }


    //Function to create an Itinerary object

    //Function to Add itinerary on click
    const handleLoginSubmit = (e) => {
        e.preventDefault();
        
        const requestOptions = {
            method: 'POST',
            headers: { 'Content-Type': 'application/json' },
        }

        const res = saveItinerary(requestOptions);
        console.log(res);
    }

    return (
        <div className="login-wrapper">
            <h1>Welcome Back!</h1>
            <form>
                <label>
                    <p>Email</p>
                    <input type="text" placeholder="Enter email" onChange={e => setEmail(e.target.value)} />
                </label>
                <label>
                    <p>Password</p>
                    <input type="password" placeholder="Enter password" onChange={e => setPassword(e.target.value)} />
                </label>
                <div>
                    <button type="submit" onClick={handleLoginSubmit}>Login</button>
                </div>
            </form>
        </div>
    )
}