import { useEffect, useState } from "react";
import { Navigate, Outlet, useLocation } from "react-router-dom"
import useSessionData from "./hooks/useSessionData";

const RequireAuth = () => {
    const location = useLocation();
    const { token, userID, roles } = useSessionData();

    const isValidSessionData = async () => {
        if (roles?.includes('Admin')) {
            // Fetch auth credentials for admin
            try {
                var requestOptions = {
                    method: "GET",
                    headers: {
                        'Content-type': 'application/json',
                        'Accept': 'application/json, text/plain, */*',
                        'userID': userID,
                        'token': token,
                        'roles': roles[0]
                    },
                    mode: 'cors'
                };
            }
            catch (error) {
                console.log(error);
            }
        }
        else {
            // Fetch auth credentials for user
            try {
                var requestOptions = {
                    method: "GET",
                    headers: {
                        'Content-type': 'application/json',
                        'Accept': 'application/json, text/plain, */*',
                        'userID': userID,
                        'token': token,
                        'roles': roles[0]
                    },
                    mode: 'cors'
                };
    
                var res = await fetch('https://localhost:9000/api/Authorization/ValidateUserCredentials', requestOptions);
                var authorizationResponse = await res.json();
                setIsUserAuthorized(authorizationResponse);
            }
            catch (error) {
                console.log(error);
            }
        }
    }

    const [isUserAuthorized, setIsUserAuthorized] = useState(isValidSessionData());

    return isUserAuthorized
        ? <Outlet /> 
        : token
            ? <Navigate to="/unauthorized" state={{ from: location }} replace />
            : <Navigate to="/login" state={{ from: location }} replace />
}

export default RequireAuth;