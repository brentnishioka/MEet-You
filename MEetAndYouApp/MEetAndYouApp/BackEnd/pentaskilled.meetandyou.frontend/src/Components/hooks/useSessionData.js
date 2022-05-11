import { useState } from 'react';

export default function useSessionData() {
    const getToken = () => {
        const tokenString = sessionStorage.getItem('token');
        return tokenString;
    };

    const saveToken = userToken => {
        if (userToken !== null) {
            sessionStorage.setItem('token', userToken);
            setToken(userToken.token);
        }
    };

    const getUserID = () => {
        const userIDString = sessionStorage.getItem('userID');
        return userIDString;
    }

    const saveUserID = currentUserID => {
        if (currentUserID !== null && currentUserID !== 0) {
            sessionStorage.setItem('userID', currentUserID);
            setUserID(currentUserID.userID);
        }
    }

    const getUserRoles = () => {
        const rolesString = JSON.parse(sessionStorage.getItem('roles'));
        return rolesString;
    }

    const saveUserRoles = currentRoles => {
        if (currentRoles !== null) {
            sessionStorage.setItem('roles', JSON.stringify(currentRoles));
            setRoles(JSON.stringify(currentRoles));
        }
    }

    const [token, setToken] = useState(getToken());
    const [userID, setUserID] = useState(getUserID());
    const [roles, setRoles] = useState(getUserRoles());

    return {
        setToken: saveToken,
        token,
        setUserID: saveUserID,
        userID,
        setRoles: saveUserRoles,
        roles
    }
}