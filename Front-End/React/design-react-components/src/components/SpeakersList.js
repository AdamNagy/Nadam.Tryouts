import Speaker from "./Speaker";
import useRequestDelay, { REQUEST_STATUS } from "../hooks/useRequestDelay";
import ReactPlaceholder from "react-placeholder";
import {data} from "../../SpeakerData";

function SpeakersList({ showSessions }) {

  const {
    data: speakersData, 
    requestStatus, error,
    updateRecord
  } = useRequestDelay(2000, data);

  if( requestStatus === REQUEST_STATUS.FAILURE )
    return (<div>{error}</div>)

  return (
    <div className="container speakers-list">
      <ReactPlaceholder type="media" rows={15} className="speakerslist-placeholder" ready={requestStatus === REQUEST_STATUS.SUCCESS}>
        <div className="row">
          {speakersData.map((speaker) => {
            return <Speaker key={speaker.id} speaker={speaker} showSessions={showSessions} 
              onFavoriteToggle={(doneFunction) => updateRecord({...speaker, favorite: !speaker.favorite}, doneFunction) } />;
          })}
        </div>
      </ReactPlaceholder>
    </div>
  );
}

export default SpeakersList;
