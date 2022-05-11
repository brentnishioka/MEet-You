// import "../Components/UserProfileDashboard/settings.css"
// import Sidebar from "../Components/UserProfileDashboard/sidebar"
import { SettingsPane, SettingsPage, SettingsContent, SettingsMenu } from 'react-settings-pane'
import "../Components/UserProfileDashboard/settingsStyle.css"
import React, {useState, useEffect} from "react";
import Button from '../Components/UserProfileDashboard/Button';

export default function Settings() {
    const [email, setEmail] = useState(null)
    const [phone, setPhone] = useState(null)
    const [password, setPassword] = useState(null)
    const [response, setResponse] = useState(null)

    // But here is an example of how it should look like:

    const getData = async () => {
        var itinRequestOptions = {
            method: "GET",
            headers: {
                'Content-Type': 'application/json',
                'Access-Control-Allow-Origin': '*',
                'Access-Control-Allow-Credentials': true
            },
            mode: 'cors'
        };

        try {
            const res = await fetch("https://localhost:9000/GetUPDData?id=5", itinRequestOptions);
            const apiRes = await res.json();
            setResponse(apiRes._userAccount);
            setEmail(apiRes._userAccount.data.userEmail)
            setPhone(apiRes._userAccount.data.userPhoneNum)
            
            console.log(apiRes._userAccount.data.userEmail);
        } catch (error) {
            console.log("Error in settings")
        }
    }





    // Define your menu
    const menu = [
        {
            title: 'Profile Info',          // Title that is displayed as text in the menu
            url: '/settings/profile_info'  // Identifier (url-slug)
        },
        {
            title: 'Disable Account',
            url: '/settings/disable_account' // change url 
        },
        {
            title: 'Delete Account',
            url: '/setttings/delete_account'
        }
    ];

    // Define one of your Settings pages
    const dynamicOptionsForProfilePage = [
        {
            key: 'mysettings.general.email',
            label: 'E-Mail address',
            type: 'text',
        },
        {
            key: 'mysettings.general.password',
            label: 'Password',
            type: 'password',
        }
    ];

    // Save settings after close
    const leavePaneHandler = (wasSaved, newSettings, oldSettings) => {
        // "wasSaved" indicates wheather the pane was just closed or the save button was clicked.

        if (wasSaved && newSettings !== oldSettings) {

        }
    };

    const settingsChanged = (changedSettings) => {
        // this is triggered onChange of the inputs
    };

    const enableUserAccount = async () => {
        var requesURL = "https://localhost:9000/EnableUserAccount?userId=5";
        
        var requestOptions = {
            method: "POST",
            headers: {
                "Content-Type": "application/json",
                "Access-Control-Allow-Origin": "*",
                "Access-Control-Allow-Credentials": true
            },
            mode: "cors"
        };

        try {
            await fetch(requesURL, requestOptions).then(response => console.log(response.json()))
        } 
        catch (error){
            console.log(error);
        }
    }

    const disableUserAccount = async () => {
        var requesURL = "https://localhost:9000/DisableUserAccount?userId=5";

        var requestOptions = {
            method: "POST",
            headers: {
                "Content-Type": "application/json",
                "Access-Control-Allow-Origin": "*",
                "Access-Control-Allow-Credentials": true
            },
            mode: "cors"
        };

        try {
            await fetch(requesURL, requestOptions).then(response => console.log(response.json()))
        }
        catch (error) {
            console.log(error);
        }
    }

    const deleteUserAccount = async () => {
        var requesURL = "https://localhost:9000/DeleteUserAccount?userId=5";

        var requestOptions = {
            method: "POST",
            headers: {
                "Content-Type": "application/json",
                "Access-Control-Allow-Origin": "*",
                "Access-Control-Allow-Credentials": true
            },
            mode: "cors"
        };

        try {
            await fetch(requesURL, requestOptions).then(response => console.log(response.json()))
        }
        catch (error) {
            console.log(error);
        }
    }

    const updateUserPassword = async (pass) => {
        var requesURL = "https://localhost:9000/UpdateUserPassword?userId=5&password=" + pass;

        var requestOptions = {
            method: "POST",
            headers: {
                "Content-Type": "application/json",
                "Access-Control-Allow-Origin": "*",
                "Access-Control-Allow-Credentials": true
            },
            mode: "cors"
        };

        try {
            await fetch(requesURL, requestOptions).then(response => console.log(response.json()))
        }
        catch (error) {
            console.log(error);
        }


    }

    const updateUserEmail = async (email) => {
        var requesURL = "https://localhost:9000/UpdateUserEmail?userId=5&email=" + email;

        var requestOptions = {
            method: "POST",
            headers: {
                "Content-Type": "application/json",
                "Access-Control-Allow-Origin": "*",
                "Access-Control-Allow-Credentials": true
            },
            mode: "cors"
        };

        try {
            await fetch(requesURL, requestOptions).then(response => console.log(response.json()))
        }
        catch (error) {
            console.log(error);
        }

    }

    const updateUserPhone = async (phone) => {
        var requesURL = "https://localhost:9000/UpdateUserPhone?userId=5&phone=" + phone;

        var requestOptions = {
            method: "POST",
            headers: {
                "Content-Type": "application/json",
                "Access-Control-Allow-Origin": "*",
                "Access-Control-Allow-Credentials": true
            },
            mode: "cors"
        };

        try {
            await fetch(requesURL, requestOptions).then(response => console.log(response.json()))
        }
        catch (error) {
            console.log(error);
        }
    }

    useEffect(() => {
        getData();

    }, [email]);

    // Return Settings Pane
    return (
        <SettingsPane items={menu} index="/settings/profile_info"  onPaneLeave={leavePaneHandler}>
            <SettingsMenu headline="Account Setttings" />
            <SettingsContent closeButtonClass="secondary" saveButtonClass="primary" header={true}>
                <SettingsPage handler="/settings/profile_info">
                    <fieldset className="form-group">
                        <label for="profileName">Email: </label>
                        <input type="text" className="form-control" name="mysettings.general.name" placeholder="Name" id="general.ame" onChange={settingsChanged} defaultValue={email} />
                        <Button onChange={updateUserEmail(email)}>Change</Button>
                    </fieldset>
                    <fieldset className="form-group">
                        <label for="ChangeNumber">Phone Number: </label>
                        <input type="text" className="form-control" name="mysettings.general.name" placeholder="Phone Number" id="general.ame" onChange={settingsChanged} defaultValue={phone} />
                        <Button onChange={updateUserPhone(phone)}>Change</Button>
                    </fieldset>
                    <fieldset className="form-group">
                        <label for="Change Password">Password: </label>
                        <input type="password" className="form-control" name="mysettings.general.name" placeholder="password" id="general.ame" onChange={settingsChanged} defaultValue={password} />
                        <Button onChange={updateUserPassword(password)}>Change</Button>
                    </fieldset>
                </SettingsPage>
                <SettingsPage handler="/settings/disable_account">
                    <Button onChange={disableUserAccount()}>Disable Account</Button>
                </SettingsPage>
                <SettingsPage handler="/setttings/delete_account">
                    <Button onChange={deleteUserAccount()}>Delete Account</Button>
                </SettingsPage>
            </SettingsContent>
        </SettingsPane>
    )
}