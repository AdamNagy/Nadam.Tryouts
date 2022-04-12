import { useReducer } from "react";

export function CheckBox() {
    const [checked, toggle] = useReducer((checked) => !checked, false);

    return (
        <>
            <input type="checkbox"
                value={checked}
                onChange={toggle}>
            </input>
            {checked ? "Yes" : "No"}
        </>
    )
}