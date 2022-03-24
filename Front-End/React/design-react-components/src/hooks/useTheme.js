import { useState } from "react";

export function useTheme(startingTheme = "light") {
    const [theme, setTheme] = useState(startingTheme);

    function validateTheme(val) {
        if( val === "dark" ) {
            setTheme("dark");
        } else {
            setTheme("light");
        }
    }

    return {
        theme,
        setTheme: validateTheme
    }
}