import React, { useState } from 'react'
import { useNavigate } from "react-router-dom";

export default function Login() {
    const [email, setEmail] = useState("");
    const [password, setPassword] = useState("");
    const navigate = useNavigate();

    /*const handleSubmit = (e) => {
        e.preventDefault();
        const requestOptions = {
            method: 'POST',
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify({ email, password })
        };
        fetch('https://localhost:9000/Login/SignIn', requestOptions)
            .then(response => response.text)
            .then(body => console.log(body))
    };*/


    const handleSubmit = async (e) => {
        e.preventDefault();
        var requestOptions = {

            method: "POST",
            headers: {
                'Content-type': 'application/json',
                'Accept': 'application/json, text/plain, */*'
            },
            mode: 'cors'
        };
        await fetch("https://localhost:9000/Login/SignIn?userEmail=" + email + "&userPassword=" + password, requestOptions)
            .then(response => {
                response.text()
                //sessionStorage.setItem('userID', response.data.userID)
            })
            .then(body => console.log(body))
       
        navigate("/");
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
                    <button type="submit" onClick={handleSubmit}>Login</button>
                </div>
            </form>
        </div>
    )
}