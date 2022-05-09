import React from "react";
import { SettingsPane, SettingsPage, SettingsContent, SettingsMenu } from 'react-settings-pane'

function SettingsIcon(){


    let settings = {
        'mysettings.general.name': 'Dennis Stücken',
        'mysettings.general.color-theme': 'purple',
        'mysettings.general.email': 'dstuecken@react-settings-pane.com',
        'mysettings.general.picture': 'earth',
        'mysettings.profile.firstname': 'Dennis',
        'mysettings.profile.lastname': 'Stücken',
    };

    // Define your menu
    const menu = [
        {
            title: 'General',          // Title that is displayed as text in the menu
            url: '/settings/general'  // Identifier (url-slug)
        },
        {
            title: 'Profile',
            url: '/settings/profile'
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
            // do something with the settings, e.g. save via ajax.
            console.log("hello")
        }
    };

    const settingsChanged = (changedSettings) => {
        console.log("hello")
    };

    // Return your Settings Pane
    return (
        <SettingsPane items={menu} index="/" settings={settings} onPaneLeave={leavePaneHandler}>
            <SettingsMenu headline="General Settings" />
            <SettingsContent closeButtonClass="secondary" saveButtonClass="primary" header={true}>
                <SettingsPage handler="../index">
                    <fieldset className="form-group">
                        <label for="profileName">Name: </label>
                        <input type="text" className="form-control" name="mysettings.general.name" placeholder="Name" id="general.ame" onChange={settingsChanged} defaultValue={settings['mysettings.general.name']} />
                    </fieldset>
                    <fieldset className="form-group">
                        <label for="profileColor">Color-Theme: </label>
                        <select name="mysettings.general.color-theme" id="profileColor" className="form-control" defaultValue={settings['mysettings.general.color-theme']}>
                            <option value="blue">Blue</option>
                            <option value="red">Red</option>
                            <option value="purple">Purple</option>
                            <option value="orange">Orange</option>
                        </select>
                    </fieldset>
                </SettingsPage>
                <SettingsPage handler="../index" options={dynamicOptionsForProfilePage} />
            </SettingsContent>
        </SettingsPane>
    );
}


export default SettingsIcon;