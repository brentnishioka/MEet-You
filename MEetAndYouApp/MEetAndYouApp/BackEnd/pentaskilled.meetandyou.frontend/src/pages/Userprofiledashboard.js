import React from 'react';
import ProfilePic from '../Components/UserProfileDashboard/ProfilePic';
import UserInfo from '../Components/UserProfileDashboard/UserInfo';
import ProfileItinerary from './profileItinerary';

/**
 * 
 * @returns JSX code the combines the individual components that were defined for the User Profile Dashboard
 */
function Userpd() {
    return (
        <div>
            <ProfilePic profileImage="" />
            <UserInfo firstName={"raymond"} lastName={"guevara"} userName={"raymondgggg"} email={"email@mail.com"} />
            {/* <ProfileItinerary /> */}

        </div>);
}

export default Userpd;