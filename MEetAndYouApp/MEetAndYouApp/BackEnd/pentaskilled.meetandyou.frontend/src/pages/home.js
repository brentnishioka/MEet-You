import React, { useState } from 'react'
import { useNavigate } from "react-router-dom";
import Select from 'react-select';
import { useForm } from 'react-hook-form';

export default function Home() {
    const [city, setCity] = useState("");
    const [date, setDate] = useState("");
    const [state, setState] = useState("");
    const navigate = useNavigate();
    const { handleSubmit } = useForm();
    const [selectedOptions, setSelectedOptions] = useState([]);

    const options = [
        { label: "Art", value: 1 },
        { label: "Attractions", value: 2 },
        { label: "Fitness Activities", value: 3 },
        { label: "Food and Drink", value: 4 },
        { label: "Libraries", value: 5 },
        { label: "Live music", value: 6 },
        { label: "Movies", value: 7 },
        { label: "Museum", value: 8 },
        { label: "Networking", value: 9 },
        { label: "Nightlife", value: 10 },
        { label: "Park", value: 11 },
    ];

    /*const toCategories = () => {
        navigate("/categories", {
            state: {
                city: city,
                date: date,
                state: state,
            }
        })
    };*/

    const handleChange = (options) => {
        setSelectedOptions(options);
    };

    const onSubmit = (formData) => {
        sessionStorage.setItem("city", city)
        sessionStorage.setItem("date", date)
        sessionStorage.setItem("state", state)
        navigate("/events", {
            state: {
                categories: selectedOptions
            }
        })
    }

    return (
        <div className="homepage-wrapper">
            <h1>Enter to start planning</h1>
            <form>
                <label>
                    <p>Enter City</p>
                    <input type="text" placeholder="City" onChange={e => setCity(e.target.value)} />
                </label>

                <label>
                    <p>Enter State</p>
                    <input type="text" placeholder="State" onChange={e => setState(e.target.value)} />
                </label>

                <label>
                    <p>Enter Date</p>
                    <input type="date" onChange={e => setDate(e.target.value)} />
                </label>

                {/*<div>
                    <button type="button" onClick={() => { toCategories() }}>Next</button>
                </div>*/}
            </form>

            <form onSubmit={handleSubmit(onSubmit)}>
                
                <p> Choose categories </p>
                
                <Select
                    isMulti={true}
                    options={options}
                    closeMenuOnSelect={true}
                    onChange={handleChange} />

                <button type="submit">Save</button>
            </form>
        </div>
    )
}