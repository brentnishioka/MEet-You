// import "../Components/UserProfileDashboard/settings.css"
// import Sidebar from "../Components/UserProfileDashboard/sidebar"
import { SettingsPane, SettingsPage, SettingsContent, SettingsMenu } from 'react-settings-pane'
import "../Components/UserProfileDashboard/settingsStyle.css"
import React, {useState} from "react";

export default function Settings() {
    



    // But here is an example of how it should look like:
    let settings = {
        'mysettings.general.name': 'raymond guevara',
        'mysettings.general.email': 'dstuecken@react-settings-pane.com',
        'mysettings.general.picture': 'earth',
        'mysettings.profile.firstname': 'Raymond',
        'mysettings.profile.lastname': 'Guevara',
    };

    // Define your menu
    const menu = [
        {
            title: 'Profile Info',          // Title that is displayed as text in the menu
            url: '/settings/general'  // Identifier (url-slug)
        },
        {
            title: 'Disable Account',
            url: '/settings/profile' // change url 
        },
        {
            title: 'Delete Account',
            url: 'setttings/delete'
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

    const updateUserPassword = async () => {
        var requesURL = "https://localhost:9000/UpdateUserPassword?userId=5&password=3arcunlock";

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

    const updateUserEmail = async () => {
        var requesURL = "https://localhost:9000/UpdateUserEmail?userId=5&email=rayray%40rayray.edu";

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

    const updateUserPhone = async () => {
        var requesURL = "https://localhost:9000/UpdateUserPhone?userId=5&phone=7438383822";

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


    // Return Settings Pane
    return (
        <SettingsPane items={menu} index="/settings/general" settings={settings} onPaneLeave={leavePaneHandler}>
            <SettingsMenu headline="Account Setttings" />
            <SettingsContent closeButtonClass="secondary" saveButtonClass="primary" header={true}>
                <SettingsPage handler="/settings/general">
                    <fieldset className="form-group">
                        <label for="profileName">Name: </label>
                        <input type="text" className="form-control" name="mysettings.general.name" placeholder="Name" id="general.ame" onChange={settingsChanged} defaultValue={settings['mysettings.general.name']} />
                    </fieldset>
                    <fieldset className="form-group">
                        <label for="ChangeNumber">Phone Number: </label>
                        <input type="text" className="form-control" name="mysettings.general.name" placeholder="Phone Number" id="general.ame" onChange={settingsChanged} defaultValue={settings['mysettings.general.name']} />
                    </fieldset>
                    <fieldset className="form-group">
                        <label for="Change Password">Password: </label>
                        <input type="password" className="form-control" name="mysettings.general.name" placeholder="Name" id="general.ame" onChange={settingsChanged} defaultValue={settings['mysettings.general.name']} />
                    </fieldset>
                </SettingsPage>
                <SettingsPage handler="insert disable functionality" options={dynamicOptionsForProfilePage} />
            </SettingsContent>
        </SettingsPane>
    )
    
}