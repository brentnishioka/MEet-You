import React from "react";

/**
 * Component used to represent the profile picture of the user
 * @param {*} param0 takes in path to image
 * @returns JSX div of the profile picture.
 */
function ProfilePic({ profileImage }) {
    return (
        <div>
            <img src={profileImage} alt="Profile Image"></img>
        </div>
    )
}

export default ProfilePic;