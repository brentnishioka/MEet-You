import React from 'react';
import ItineraryComponent from '../Components/ItineraryComponent';
import ProfilePic from '../Components/UserProfileDashboard/ProfilePic';
import UserInfo from '../Components/UserProfileDashboard/UserInfo';
import Itineraries from '../Components/UserProfileDashboard/UserItineraries';
import ProfileItinerary from './profileItinerary';

/**
 * 
 * @returns JSX code the combines the individual components that were defined for the User Profile Dashboard
 */
function Userpd() {
    return (
        <div>
            
            <UserInfo />
            <Itineraries />
        </div>);
}

export default Userpd;