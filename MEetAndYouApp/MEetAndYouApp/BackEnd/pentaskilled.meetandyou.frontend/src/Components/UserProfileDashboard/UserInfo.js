import React from "react";

function UserInfo({ firstName, lastName, userName, email }) {
    return (
        <div>
            <table>
                <tr>
                    <td>First Name: </td>
                    <td>{firstName} </td>

                    <td>Last Name: </td>
                    <td>{lastName} </td>
                </tr>
                <tr>
                    <td>Username: </td>
                    <td>{userName} </td>

                    <td>Email: </td>
                    <td>{email} </td>
                </tr>
            </table>
        </div>
    );
}

export default UserInfo;
