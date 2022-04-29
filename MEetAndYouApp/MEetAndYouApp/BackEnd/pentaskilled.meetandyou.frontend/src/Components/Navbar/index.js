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
                    <NavLink to='/' activeStyle>
                        Home
                    </NavLink>
                    <NavLink to='/calendar' activeStyle>
                        Calendar
                    </NavLink>

                    <NavLink to='/memoryalbumlist' activeStyle>
                        Memory Album
                    </NavLink>

                    {/* Second Nav */}
                    {/* <NavBtnLink to='/sign-in'>Sign In</NavBtnLink> */}
                </NavMenu>
                <NavBtn>
                    <NavBtnLink to='/signup'>Sign up</NavBtnLink>
                </NavBtn>
            </Nav>
        </>
    );
};
export default Navbar;