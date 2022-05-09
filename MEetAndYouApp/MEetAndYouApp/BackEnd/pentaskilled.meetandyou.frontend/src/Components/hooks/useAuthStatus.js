import useToken from "./useToken";
import { NavBtnLink } from "../Navbar/NavbarElements";
import { useNavigate } from "react-router-dom";

function useAuthStatus() {
    const { token } = useToken();
    const navigate = useNavigate();

    const handleLogoutClick = async () => {
        // var userID = sessionStorage.getItem('userID')

        // var requestOptions = {
        //     method: "DELETE",
        //     headers: {
        //         'Content-type': 'application/json',
        //         'Accept': 'application/json, text/plain, */*'
        //     },
        //     mode: 'cors'
        // };

        // var res = await fetch('https://localhost:9000/Login/SignOut?userID=' + userID, requestOptions);
        // var logoutResponse = await res.json()

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