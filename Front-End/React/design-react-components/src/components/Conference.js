import {useState} from "react";

import SpeakersList from "./SpeakersList";
import SpeakersToolbar from "./SpeakersToolbar";

import { SpeakerFilterProvider } from "../contexts/SpeakerFilterContext";

const Conference = () => {
    
    return(
        <SpeakerFilterProvider startingShowSessions={false}>
            <SpeakersToolbar />
            <SpeakersList />
        </SpeakerFilterProvider>
    )
}

export default Conference;