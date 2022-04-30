import React, { useState, useEffect} from 'react'
import { useNavigate } from "react-router-dom";

export default function GetSuggestion()
{
    const [location, setLocation] = useState("");
    const [category, setCategory] = useState("");
    const [result, setResult] = useState([]);
    const [date, setDate] = useState("");


    // const suggestion = () => {
    //     //console.log("it's working")
    //     fetch("https://localhost:9000/GetEvent?category=food%20and%20drink&location=Long%20Beach&date=May%204")
    //     .then(response => console.log(response.json()))
    //     .catch(error => console.error('Unable to get items.', error));
    //     console.log(result)
    // }

    useEffect( () => {
        fetch("https://localhost:9000/GetEvent?category=food%20and%20drink&location=Long%20Beach&date=May%204")
        .then(response => setResult(response.json()))
        .catch(error => console.error('Unable to get items.', error));
        console.log("THE RESULTS ARE", result)
    }, [])

    // let location = "";
    // let category = "";
    // let date = "";




    return(
        <div className="getsuggestion-wrapper">
        <h1>GetSuggestion!</h1>
            {/* <h1>{result}</h1> */}

        {/* <body>     
            <script src="src/apicall/site.js" asp-append-version="true"></script>
            <script type="text/javascript">
                console.log(getSuggestion());
            </script>
        </body> */}
    </div>
    )
}
