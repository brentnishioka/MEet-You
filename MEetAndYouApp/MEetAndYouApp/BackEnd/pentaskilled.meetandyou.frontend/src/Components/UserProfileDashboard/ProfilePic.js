import React from "react";


function ProfilePic({ profileImage }) {
    return (
        <div>
            <img src={profileImage} alt="Profile Image"></img>
        </div>
    )
}

export default ProfilePic;