import Speaker from "./Speaker";
import { useState } from "react";
import { data } from "../../SpeakerData";

function SpeakersList({ showSessions }) {

  const [speakersData, setSpeakersData] = useState(data);

  const onFavoriteToggle = (id) => {
    const speaker = speakersData.find((sp) => sp.id === id);
    // speaker.favorite = !speaker.favorite;
    console.log(`${speaker.first} ${speaker.last} is ${speaker.favorite}`);
    
    const updated = {...speaker, favorite: !speaker.favorite};
    const updatedList = speakersData.map((sp) => sp.id === id ? updated : sp);
    setSpeakersData(updatedList);  
  }

  return (
    <div className="container speakers-list">
      <div className="row">
        {speakersData.map(function (speaker) {
          return <Speaker key={speaker.id} speaker={speaker} showSessions={showSessions} 
          onFavoriteToggle={() => onFavoriteToggle(speaker.id)}/>;
        })}
      </div>
    </div>
  );
}

export default SpeakersList;
