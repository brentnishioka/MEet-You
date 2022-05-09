import { useState } from "react";
import { Navigate, Outlet, useLocation } from "react-router-dom"
import useSessionData from "./hooks/useSessionData";

const RequireAuth = () => {
    const location = useLocation();
    const { token, userID, roles } = useSessionData();
    const [isUserAuthorized, setIsUserAuthorized] = useState(false);

    function isContains(json, value) {
        let contains = false;
        Object.keys(json).some(key => {
            contains = typeof json[key] === 'object' ? 
            isContains(json[key], value) : json[key] === value;
            return contains;
        });
        return contains;
    }

    const isValidSessionData = async () => {
        if (isContains(roles, 'Admin')) {
            // Fetch auth credentials for admin
        }
        else {
            // Fetch auth credentials for user~
            // var requestOptions = {
            //     method: "GET",
            //     headers: {
            //         'Content-type': 'application/json',
            //         'Accept': 'application/json, text/plain, */*'
            //     },
            //     mode: 'cors'
            // };

            // var res = await fetch('https://localhost:9000/api/Authorization/')
        }
    }

    return token ? <Outlet /> : <Navigate to="/login" state={{ from: location }} replace />
}

export default RequireAuth;