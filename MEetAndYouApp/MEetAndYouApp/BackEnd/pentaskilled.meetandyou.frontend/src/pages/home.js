import React, { useState } from 'react'
import { useNavigate } from "react-router-dom";

export default function Home() {
    const [city, setCity] = useState("");
    const [date, setDate] = useState("");
    const [state, setState] = useState("");
    const navigate = useNavigate();

    const toCategories = () => {
        navigate("/categories", {
            state: {
                city: city,
                date: date,
                state: state,
            }
        })
    };

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

                <div>
                    <button type="button" onClick={() => { toCategories() }}>Next</button>
                </div>
            </form>
        </div>
    )
}