import React from 'react';
import MemoryAlbumList from '../../pages/memoryalbumlist';

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
                    <NavLink to='/Settings' activeStyle={{ color: 'black' }}>
                        Settings
                    </NavLink>

                    <NavLink to='/share' activeStyle>
                        Share
                    </NavLink>
                    <NavLink to='/memoryalbumlist' activeStyle>
                        Memory Album
                    </NavLink>

                    {/* Second Nav */}
                    {/* <NavBtnLink to='/sign-in'>Sign In</NavBtnLink> */}
                </NavMenu>
                <NavBtn>
                    <NavBtnLink to='/login'>Login</NavBtnLink>
                </NavBtn>
            </Nav>
        </>
    );
};
export default Navbar;