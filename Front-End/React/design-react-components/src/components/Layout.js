import { useContext } from "react";
import {ThemeProvider, ThemeContext} from "../contexts/ThemeContext";

function Layout({startingTheme, children}) {
    return (
        <ThemeProvider startingTheme={startingTheme}>
            <LayoutNoTheme>{children}</LayoutNoTheme>
        </ThemeProvider>
    )
}

function LayoutNoTheme({children}) {

    const {theme} = useContext(ThemeContext);

    return (
        <div className={theme === "light" ? "container-fluid light" : "container-fluid dark"} >
            {children}
        </div>
    )
}

export default Layout;
