import React, { useState } from 'react'
import { useNavigate } from "react-router-dom";
import 'react-phone-number-input/style.css';
import 'bootstrap/dist/css/bootstrap.min.css';

export default function Login() {
    //useStates for login
    const [email, setEmail] = useState("");
    const [password, setPassword] = useState("");


    //useStates for account registration
    const [signUpEmail, setSignUpEmail] = useState("");
    const [signUpPassword, setSignUpPassword] = useState("");
    const [signUpNumber, setSignUpNumber] = useState("");
    const [firstName, setFirstName] = useState("");
    const [lastName, setLastName] = useState("");

    
    const navigate = useNavigate();


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
        await fetch("https://meetandyou.me:8001/Login/SignIn?userEmail=" + email + "&userPassword=" + password, requestOptions)
            .then(response => {
                response.text()
                //sessionStorage.setItem('userID', response.data.userID)
            })
            .then(body => console.log(body))
       
        navigate("/");
    }

    const handleSignUp = async (e) => {

    }
 

    
    //className="login-wrapper"
    return (
        <div className="login-wrapper">
            <div className = "login">
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

            <div className="line"></div>

            <div className = "sign-up">
                <h1>Create An Account</h1>
                <form>
                    <label>
                        <p>First Name</p>
                        <input type="text" placeholder="Enter first name" onChange={e => setFirstName(e.target.value)} />
                    </label>

                    <label>
                        <p>Last Name</p>
                        <input type="text" placeholder="Enter last name" onChange={e => setLastName(e.target.value)} />
                    </label>

                    <label>
                        <p>Email</p>
                        <input type="text" placeholder="Enter email" onChange={e => setSignUpEmail(e.target.value)} />                      
                    </label>

                    <label>
                        <p>Password</p>
                        <input type="text" placeholder="Enter password" onChange={e => setSignUpPassword(e.target.value)} />
                    </label>

                    <label>
                        <p>Phone Number</p>
                        <input type="text" placeholder="Enter phone number" onChange={e => setSignUpNumber(e.target.value)} />
                    </label>
                    <div>
                        <button type="submit" onClick={handleSignUp}>Sign Up</button>
                    </div>
                </form>
            </div>
        </div>
    )
}