import { Navigate, Outlet, useLocation } from "react-router-dom"

const RequireAuth = () => {
    const location = useLocation();

    if (sessionStorage.getItem['token'] !== null) {
        return ( 
            <Outlet />
        );
    }
    else {
        return (
            <Navigate to="/login" state={{ from: location }} replace />
        );
    }
}

export default RequireAuth;