import React, { useState, useEffect} from 'react'
import { useNavigate } from "react-router-dom";

export default function GetSuggestion()
{
    const [location, setLocation] = useState("");
    const [category, setCategory] = useState("");
    const [result, setResult] = useState([]);
    const [date, setDate] = useState("");
    const [suggestions, setSuggestions] = useState([])


    // const suggestion = () => {
    //     //console.log("it's working")
    //     fetch("https://meetandyou.me:8001/GetEvent?category=food%20and%20drink&location=Long%20Beach&date=May%204")
    //     .then(response => console.log(response.json()))
    //     .catch(error => console.error('Unable to get items.', error));
    //     console.log(result)
    // }

    useEffect( () => {
        fetch("https://meetandyou.me:8001/GetEvent?category=food%20and%20drink&location=Long%20Beach&date=May%204")
        .then(response => setResult(response.json()))
        .catch(error => console.error('Unable to get items.', error));
        console.log("THE RESULTS ARE", result)
    }, [])

    function getSuggestion(){
        const responseData =
        fetch("https://meetandyou.me:8001/GetEvent?category=food%20and%20drink&location=Long%20Beach&date=May%204")
        .then(response => response.json())
        .catch(error => console.error('Unable to get items.', error));

        return responseData;
    }

    function displaySuggestion(reponseData){
        var data = reponseData.data;
        const eventObject = data[0];

        return(
            <div>
                <h1>eventObject</h1>
            </div>
        )
    }

    // let location = "";
    // let category = "";
    // let date = "";

    const responseData = getSuggestion();

    return(
        <div className="getsuggestion-wrapper">
            <h1>GetSuggestion! </h1>
            
        </div>
    )
}
