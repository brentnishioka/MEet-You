import React, {useState, useEffect} from "react";


function ProfileItinerary({}){
    const [data, setData] = useState([]);


    
    const getData = async () => {
        try {
            var id = 5
            const res = await fetch('https://localhost:9000/GetUPDData?id=' + id);
            const response = await res.json();
            setData(response.data);
            console.log(response);
        }
        catch (error) {
            console.log('error');
        }
    }

    useEffect(() => {
        getData();
    }, []);


    let counter = -1;
    const rows = data.map(item => (
        <tr>
            <td align='center'>{counter = counter + 1}</td>
            <td align='center'>{data.getData}</td>
            <td align='center'>{data.getData}</td>
        </tr>
    ));


    return (
        <div>
            <h1>Itineraries</h1>
            <table
                style={{ "borderCollapse": "collapse", "padding": "5px", "width": "100%", "border": "1px solid black" }}
                className="table table-hover">
                <thead style={{ "borderCollapse": "collapse", "padding": "5px", "width": "100%", "border": "1px solid black" }}>
                    <tr style={{ "borderCollapse": "collapse", "padding": "5px", "width": "100%", "border": "1px solid black" }}>
                    </tr>
                </thead>
                <tbody>
                    {rows}
                </tbody>
            </table>
        </div>
    )
}

export default ProfileItinerary;