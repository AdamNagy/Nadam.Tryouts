import { FaStar } from "react-icons/fa"; 
import { useState } from "react";

const createArray = (length) => [...Array(length)];

export function StarRating({totalStars = 5, selectStar}) {
    const [selectedStarts, setSelectedStarts] = useState(0);

    return <>{createArray(totalStars).map((n, i) => 
        <Star key={i} 
            selected={selectedStarts > i} 
            onSelect={() => setSelectedStarts(i+1)}
    />)}
    <p>{selectedStarts} of {totalStars}</p>
    </>
}

export function Star({selected = false, onSelect}) {
    return (
        <>
            <FaStar color={selected ? "red" : "grey"} onClick={onSelect}/>
        </>

    )
}