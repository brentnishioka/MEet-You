import React from 'react';
import ProfilePic from '../Components/UserProfileDashboard/ProfilePic';
import UserInfo from '../Components/UserProfileDashboard/UserInfo';


function Userpd() {
    return (
        <div>
            <h1>hello world </h1>
            <h2>raymond</h2>
            <ProfilePic profileImage="https://cdn.survivetheark.com/uploads/monthly_2020_08/MONKE.jpg.bbd7c9dd8e82715c52d14f2ec3fd524b.jpg" />
            <UserInfo firstName={"raymond"} lastName={"guevara"} userName={"raymondgggg"} email={"email@mail.com"} />
        </div>);
}

export default Userpd;