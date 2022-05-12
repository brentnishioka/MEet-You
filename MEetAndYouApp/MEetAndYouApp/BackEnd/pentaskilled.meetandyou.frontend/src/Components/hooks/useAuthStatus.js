import useSessionData from "./useSessionData";
import { NavBtnLink } from "../Navbar/NavbarElements";
import { useNavigate } from "react-router-dom";

function useAuthStatus() {
    const { token, userID } = useSessionData();
    const navigate = useNavigate();

    const handleLogoutClick = async () => {
        var currentUserID = userID;

        var requestOptions = {
            method: "DELETE",
            headers: {
                'Content-type': 'application/json',
                'Accept': 'application/json, text/plain, */*'
            },
            mode: 'cors'
        };

        var res = await fetch('https://meetandyou.me:8001/Login/SignOut?userID=' + currentUserID, requestOptions);
        var logoutResponse = await res.json()

        sessionStorage.clear()
        navigate('/')
        window.location.reload()
    }

    const renderLogoutButton = () => (
        <div className='LoggedInButton'>
            <NavBtnLink to='/' onClick={handleLogoutClick}>Logout</NavBtnLink>
        </div>
    );

    const renderLoginButton = () => (
        <div className='NotLoggedInButton'>
            <NavBtnLink to='/login'>Login</NavBtnLink>
        </div>
    );

    return {
        isLoggedIn: token,
        loggedInButtonState: renderLoginButton,
        loggedOutButtonState: renderLogoutButton
    };
}

export default useAuthStatus