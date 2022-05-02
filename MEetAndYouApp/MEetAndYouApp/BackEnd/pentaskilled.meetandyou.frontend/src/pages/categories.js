/*import React, { useState, useEffect } from 'react';
import Select from 'react-select';
import 'bootstrap/dist/css/bootstrap.min.css';

const eventCategories = [
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

const Categories = () => {

    const [category, setCategory] = useState("");
    const [categories, setCategories] = useState([]);

    //setCategories([...categories, selectedOption.label]);
    

    const handleSelect = (options) => {
        setCategory(options.label);    
    };

    const onSubmit = (formData, event) => {
        console.log("Form Data: ", formData)
        console.log("Selected Options: ", selectedOptions)
    }

    console.log("CATEGORY: ", category);
    console.log("CATEGORIES: ", categories); 

  
    return (
        <div className="container">
            <div className="row">
                <div className="col-md-4"></div>
                <div className="col-md-4">
                    <label>
                        1.
                    </label>
                    <Select
                        isMulti
                        options={eventCategories}
                        onChange={handleSelect}
                    />

                    <button onClick={addDropdown}>
                        Add another event category
                    </button>

                </div>
                <div className="col-md-4"></div>
            </div>
        </div>
    )
};

export default Categories*/

import React, { useState } from 'react';
import Select from 'react-select';
import { useForm } from 'react-hook-form';
import { useNavigate } from "react-router-dom";
import { useLocation } from 'react-router-dom';

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

const Categories = () => {
    const navigate = useNavigate();
    const location = useLocation();
    const {handleSubmit } = useForm();
    const [selectedOptions, setSelectedOptions] = useState([]);
    const test = [location.state.city, location.state.date, location.state.state];
    
    const handleChange = (options) => {
        setSelectedOptions(options);
    };

    const onSubmit = (formData) => {
        navigate("/events", {
            state: {
                location: test,
                category: selectedOptions,
            }
        })
    }

    return (
        <form onSubmit={handleSubmit(onSubmit)}>
            <Select
                isMulti={true}
                options={options}
                closeMenuOnSelect={true}
                onChange={handleChange} />

            <button type="submit">Save</button>
        </form>
    );
}

export default Categories