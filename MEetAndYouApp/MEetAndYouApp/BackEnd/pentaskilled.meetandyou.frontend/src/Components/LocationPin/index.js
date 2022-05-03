import React, { useState } from "react";
import { FaLocationArrow } from "react-icons/fa"

const LocationPin = ({ rating, onRating }) => {
    // const [rating, setRating] = useState(null);
    const [hover, setHover] = useState(null);
    return (
        <div>
            {[...Array(5)].map((locationArrow, i) => {
                const userRating = i + 1;

                return (
                    <label>
                        <input
                            type="radio"
                            name="rating"
                            style={{ display: "none" }}
                            value={userRating}
                            onClick={() => onRating(userRating)}
                        />
                        <FaLocationArrow
                            color={userRating <= (hover || rating) ? "#ff0033" : "#e4e5e9"}
                            size={30}
                            onMouseEnter={() => setHover(userRating)}
                            onMouseLeave={() => setHover(null)}
                        />
                    </label>
                );
            })}
        </div>
    );
};

export default LocationPin;