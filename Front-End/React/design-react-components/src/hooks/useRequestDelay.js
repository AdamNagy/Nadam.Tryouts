import { useState, useEffect } from "react";

export const REQUEST_STATUS = {
    LOADING: "loading",
    SUCCESS: "success",
    FAILURE: "failure"
}

function useRequestDelay(delayTime = 1000, initialData = []) {
    const [data, setData] = useState(initialData);
    const [requestStatus, setRequestStatus] = useState(REQUEST_STATUS.LOADING);
    const [error, setError] = useState("");

    const delay = (ms) => new Promise((resolve, reject) => setTimeout(resolve, ms));

    useEffect(() => {
        async function delayFunc() {
            try {
                await delay(delayTime);
                setRequestStatus(REQUEST_STATUS.SUCCESS);
                setData(data);
            } catch (e) {
                setRequestStatus(REQUEST_STATUS.FAILURE);
                setError("Shit happens");
            }
        }

        delayFunc();
    }, []);

    function updateRecord(recordUpdate, doneCallback) {
        const newRecords = data.map((rec) => {
            return rec.id === recordUpdate.id ? recordUpdate : rec;
        });

        async function delayFunction() {
            try {
                setData(newRecords);
                await delay(400);
                if( doneCallback ) {
                    doneCallback();
                }

            } catch(e) {
                console.log(e);
            }
        }

        delayFunction();
    }

    return {
        data, 
        requestStatus,
        error,
        updateRecord
    }
}

export default useRequestDelay;