import { useEffect, useState } from "react";
import { Navigate, Outlet, useLocation } from "react-router-dom"
import useSessionData from "./hooks/useSessionData";

const RequireAuth = () => {
    const location = useLocation();
    const { token, userID, roles } = useSessionData();

    const isValidSessionData = async () => {
        if (roles?.includes('Admin')) {
            // Fetch auth credentials for admin
            // try {
            //     var requestOptions = {
            //         method: "GET",
            //         headers: {
            //             'Content-type': 'application/json',
            //             'Accept': 'application/json, text/plain, */*',
            //             'userID': userID,
            //             'token': token,
            //             'roles': roles
            //         },
            //         mode: 'cors'
            //     };
            // }
            // catch (error) {
            //     console.log(error);
            // }
        }
        else {
            // Fetch auth credentials for user
            var requestURL = 'https://localhost:9000/api/Authorization/ValidateUserCredentials'

            try {
                var requestOptions = {
                    method: "GET",
                    headers: {
                        'Content-type': 'application/json',
                        'Accept': 'application/json, text/plain, */*',
                        'userID': userID,
                        'token': token,
                        'roles': roles
                    },
                    mode: 'cors'
                };
    
                var res = await fetch(requestURL, requestOptions);
                var authorizationResponse = await res.json();
                setIsUserAuthorized(authorizationResponse);
            }
            catch (error) {
                console.log(error);
            }
        }
    }

    const [isUserAuthorized, setIsUserAuthorized] = useState(isValidSessionData());
    // console.log(isUserAuthorized)

    return isUserAuthorized && userID && token && roles
        ? <Outlet /> 
        : token
            ? <Navigate to="/unauthorized" state={{ from: location }} replace />
            : <Navigate to="/login" state={{ from: location }} replace />
}

export default RequireAuth;