import React, { useEffect, useState } from "react";
import { FaLocationArrow } from "react-icons/fa"

const DisplayLocationPin = ({ eventRating }) => {
    console.log(eventRating)
    return (
        <div>
            <h5>Current Rating:</h5>
            {[...Array(5)].map((locationArrow, i) => {
                const userRating = i + 1;

                return (
                    <label>
                        <FaLocationArrow
                            color={userRating <= (eventRating) ? "#ff0033" : "#e4e5e9"}
                            size={30}
                        />
                    </label>
                );
            })}
        </div>
    );
}

export default DisplayLocationPin;