import React, {useEffect, useState} from "react";
import { set } from "react-hook-form";

/**
 * Component that will be used to display basic information pertaining to the account 
 * @param {*} param0 
 * @returns JSX table displaying the user account information
 */
function UserInfo() {
    const [response, setResponse] = useState(null)
    const [email, setEmail] = useState(null)
    const [phone, setPhone] = useState(0)

    const getData = async () => {
        var itinRequestOptions = {
            method: "GET",
            headers: {
                'Content-Type': 'application/json',
                'Access-Control-Allow-Origin': '*',
                'Access-Control-Allow-Credentials': true
            },
            mode: 'cors'
        };

        try{
            const res = await fetch("https://localhost:9000/GetUPDData?id=5", itinRequestOptions);
            const apiRes = await res.json();
            setResponse(apiRes._userAccount);
            setEmail(apiRes._userAccount.data.userEmail)
            setPhone(apiRes._userAccount.data.userPhoneNum)
            //console.log(response.data);
        } catch (error){
            console.log("Error")
        }
    }

    useEffect (() => {
        getData();
    }, []);


    return (
        <div>
            <table>
                <tr>
                    <td>Email: </td>
                    <td>{email} </td>

                    <td>Phone number: </td>
                    <td>{phone} </td>
                </tr>
            </table>
        </div>
    );
}

export default UserInfo;
