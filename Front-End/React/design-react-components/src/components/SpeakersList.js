import Speaker from "./Speaker";
import useRequestDelay, { REQUEST_STATUS } from "../hooks/useRequestDelay";
import ReactPlaceholder from "react-placeholder";
import {data} from "../../SpeakerData";
import { SpeakerFilterContext } from "../contexts/SpeakerFilterContext";
import { useContext } from "react";

function SpeakersList() {

  const {
    data: speakersData, 
    requestStatus, error,
    updateRecord
  } = useRequestDelay(2000, data);

  const { searchQuery, eventYear } = useContext(SpeakerFilterContext);

  if( requestStatus === REQUEST_STATUS.FAILURE )
    return (<div>{error}</div>)

  return (
    <div className="container speakers-list">
      <ReactPlaceholder type="media" rows={15} className="speakerslist-placeholder" ready={requestStatus === REQUEST_STATUS.SUCCESS}>
        <div className="row">
          {speakersData
          .filter((sp) => sp.first.toLowerCase().includes(searchQuery) || sp.last.toLowerCase().includes(searchQuery))
          .filter((sp) => sp.sessions.find((se) => se.eventYear === eventYear))
          .map((speaker) => {
            return (
              <Speaker 
                key={speaker.id} 
                speaker={speaker}
                onFavoriteToggle={(doneFunction) => updateRecord({...speaker, favorite: !speaker.favorite}, doneFunction) }
              />);
          })}
        </div>
      </ReactPlaceholder>
    </div>
  );
}

export default SpeakersList;
