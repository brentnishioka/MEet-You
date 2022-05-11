import React, { useEffect } from 'react';
import useAuthStatus from '../hooks/useAuthStatus';

import {
    Nav,
    NavLogo,
    NavLink,
    Bars,
    NavMenu,
    NavBtn,
    NavBtnLink,
} from "./NavbarElements";

const Navbar = () => {
    const { isLoggedIn, loggedInButtonState, loggedOutButtonState } = useAuthStatus();

    return (
        <>
            <Nav>
                <Bars />
                <NavMenu>
                    <NavLink to='/' activeStyle={{ color: 'black' }}>
                        Home
                    </NavLink>
                    <NavLink to='/calendar' activeStyle={{ color: 'black' }}>
                        Calendar
                    </NavLink>
                    <NavLink to='/rating' activeStyle={{ color: 'black' }}>
                        Event Rating
                    </NavLink>
                    <NavLink to='/getrandomsuggestion' activeStyle={{ color: 'black' }}>
                        RandomSuggestion
                    </NavLink>
                    <NavLink to='/itinerary' activeStyle={{ color: 'black' }}>
                        Create Itinerary
                    </NavLink>
                    <NavLink to='/userprofiledashboard' activeStyle={{ color: 'black' }}>
                        User Profile Dashboard
                    </NavLink>
                    <NavLink to='/settings' activeStyle={{ color: 'black' }}>
                        Settings
                    </NavLink>

                    <NavLink to='/share' activeStyle>
                        Share
                    </NavLink>
                    <NavLink to='/memoryalbumlist' activeStyle>
                        Memory Album
                    </NavLink>                  
                </NavMenu>
                <NavBtn>
                    {isLoggedIn ? loggedOutButtonState() : loggedInButtonState()}
                </NavBtn>
            </Nav>
        </>
    );
};
export default Navbar;