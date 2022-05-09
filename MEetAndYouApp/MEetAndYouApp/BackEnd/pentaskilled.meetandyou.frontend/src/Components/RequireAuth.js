import { Navigate, Outlet, useLocation } from "react-router-dom"
import useToken from "./hooks/useToken";

const RequireAuth = () => {
    const location = useLocation();
    const { token } = useToken();

    return token ? <Outlet /> : <Navigate to="/login" state={{ from: location }} replace />
}

export default RequireAuth;