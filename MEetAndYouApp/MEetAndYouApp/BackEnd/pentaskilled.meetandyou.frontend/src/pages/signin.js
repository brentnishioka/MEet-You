import React, { useState, useEffect } from 'react'
import { useNavigate } from "react-router-dom";
import 'react-phone-number-input/style.css';
import 'bootstrap/dist/css/bootstrap.min.css';
import useSessionData from '../Components/hooks/useSessionData';

export default function Login() {
    //useStates for login
    const [email, setEmail] = useState("");
    const [password, setPassword] = useState("");
    const {setToken, setUserID, setRoles} = useSessionData();



    //useStates for account registration
    const [signUpEmail, setSignUpEmail] = useState("");
    const [signUpPassword, setSignUpPassword] = useState("");
    const [signUpNumber, setSignUpNumber] = useState("");
    const [signUpConfirmPass, setSignUpConfirmPass] = useState("");
    const [passwordMatch, setPasswordMatch] = useState();
    const [invalidPassword, setInvalidPassword] = useState();
    const [invalidEmail, setInvalidEmail] = useState();
    const [invalidNum, setInvalidNum] = useState();


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
        var res = await fetch("https://meetandyou.me:8001/Login/SignIn?userEmail=" + email + "&userPassword=" + password, requestOptions)
        var loginResponse = await res.json();
        setUserID(loginResponse && loginResponse.userID);
        setToken(loginResponse && loginResponse.token);
        setRoles(loginResponse && loginResponse.roles);

        if (loginResponse.userID && loginResponse.token && loginResponse.roles) {
            navigate("/");
            window.location.reload()
        }
    }

    const showPassDoesNotMatch = () => {
        return (
            <div>
                <p style={{ color: 'red', margin: "0px" }}>Passwords do not match</p>
            </div>
        )
    }

    const showInvalidPassword = (invalid) => {
        return (
            <div>
                <p style={{ color: 'red', margin: "0px" }}>Invalid Password</p>
                <p style={{ color: 'red', margin: "0px" }}>Password must contain: 8-16 characters, a-z, A-Z, 0-9</p>
            </div>
        )
    }

    const showInvalidEmail = (invalid) => {
        console.log("email: ", signUpEmail)
        return (
            <div>
                <p style={{ color: 'red', margin: "0px" }}>Invalid Email</p>
            </div>
        )
    }

    const showInvalidNum = (invalid) => {
        console.log("phne num: ", signUpNumber)

        return (
            <div>
                <p style={{ color: 'red', margin: "0px" }}>Invalid phone number</p>
            </div>
        )
    }

   


    const handleSignUp = async (e) => {
        e.preventDefault();
        var splitString = [];

        if (signUpPassword !== signUpConfirmPass) {
            setPasswordMatch(showPassDoesNotMatch)
        }

        else {
            var requestOptions = {
                method: "POST",
                headers: {
                    'Content-type': 'application/json',
                    'Accept': 'application/json, text/plain, */*'
                },
                mode: 'cors'
            };
            var res = await fetch("https://meetandyou.me:8001/PostAccount?email=" + signUpEmail + "&password=" + signUpPassword + "&phoneNumber=" + signUpNumber, requestOptions)
            var signUpResponse = await res.json();

            if (signUpResponse.message.includes("Invalid parameter(s):")) {
                splitString = signUpResponse.message.split(": ");
                if (splitString[1].includes("password")) {
                    setInvalidPassword(showInvalidPassword());
                }

                if (splitString[1].includes("email")) {
                    setInvalidEmail(showInvalidEmail());
                }

                if (splitString[1].includes("phone number")) {
                    setInvalidNum(showInvalidNum());
                }
            }
            else {
                navigate("/")
            }
        }
    }

    useEffect(() => {
        showInvalidPassword();
        showInvalidNum();
        showInvalidEmail();
        showPassDoesNotMatch();
    }, [passwordMatch, invalidPassword, invalidEmail, invalidNum]);


    //className="login-wrapper"
    return (
        <div className="login-wrapper">
            <div className="login">
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

            <div className="sign-up">
                <h1>Create An Account</h1>
                <form>
                    {/*<label>
                        <p>First Name</p>
                        <input type="text" placeholder="Enter first name" onChange={e => setFirstName(e.target.value)} />
                    </label>
                    s
                    <label>
                        <p>Last Name</p>
                        <input type="text" placeholder="Enter last name" onChange={e => setLastName(e.target.value)} />
                    </label>*/}

                    <label>
                        <p>Email</p>
                        <input type="text" placeholder="Enter email" onChange={e => setSignUpEmail(e.target.value)} />
                    </label>

                    <label>
                        <p>Phone Number</p>
                        <input type="text" placeholder="Enter phone number" onChange={e => setSignUpNumber(e.target.value)} />
                    </label>



                    <label>
                        <p>Password</p>
                        <input type="password" placeholder="Enter password" onChange={e => setSignUpPassword(e.target.value)} />
                    </label>

                    <p></p>

                    <label>
                        <p>Confirm Password</p>
                        <input type="password" placeholder="Re-enter password" onChange={e => setSignUpConfirmPass(e.target.value)} />
                    </label>

                    <div>
                        <button type="submit" onClick={handleSignUp}>Sign Up</button>
                    </div>
                    {passwordMatch}
                    {invalidPassword}
                    {invalidEmail}
                    {invalidNum}
                </form>
            </div>
        </div>
    )
}