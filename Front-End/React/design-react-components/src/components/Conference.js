import {useState} from "react";

import SpeakersList from "./SpeakersList";
import SpeakersToolbar from "./SpeakersToolbar";

const Conference = ({ theme, setTheme }) => {

    const [showSessions, setShowSessions] = useState(true);
    return(
        <>
            <SpeakersToolbar 
                theme={theme}
                setTheme={setTheme}
                showSessions={showSessions}
                setShowSessions={setShowSessions} />
            <SpeakersList showSessions={showSessions}/>
        </>
    )
}

export default Conference;