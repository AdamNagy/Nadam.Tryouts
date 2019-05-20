

function Sleep(ms) {
    return new Promise(resolve => setTimeout(resolve, ms));
}

window.Nadam = window.Nadam || {};
Nadam.Http = new function () {

    this.Get = function (url) {
        return new Promise(function (resolve, reject) {

            if (url == undefined)
                reject("Url is null");

            const request = new XMLHttpRequest();
            request.timeout = 10000;
            /**
             * xmlhttp.readyState enum:
             * 0	UNSENT	Client has been created. open() not called yet.
             * 1	OPENED	open() has been called.
             * 2	HEADERS_RECEIVED	send() has been called, and headers and status are available.
             * 3	LOADING	Downloading; responseText holds partial data.
             * 4	DONE	The operation is complete.
             */
            request.onreadystatechange = function () {
                if (request.readyState === 4 && request.status === 200) {
                    resolve(request.responseText);
                } else if (request.readyState === 4 && request.status == 0) {
                    reject({ readyState: request.readyState, status: request.status });
                } else if (request.readyState === 4 && request.status !== 200) {
                    reject({ readyState: request.readyState, status: request.status });
                }
            }

            try {
                request.open("GET", url);
                request.send();
            }
            catch{
                reject({ readyState: request.readyState, status: request.status });
            }
        });
    };

    var queueItemReduxer = function (squenceNum, handler) {
        return function (result) {

            for (var i = 0; i < queueState.queue.length; ++i) {
                if (queueState.queue[i] !== undefined && queueState.queue[i].squenceNum === squenceNum) {
                    queueState.queue.splice(queueState.queue[i], 1);
                    --queueState.inTheQueue;
                }
            }

            if (handler != undefined) {
                queueState.numOfSuccess = queueState.numOfSuccess + 1;
                handler(result);
            }
        }
    }

    var queueState = {}

    var initState = function () {

        return {
            queue: new Array(),
            urls: new Array(),
            failedUrls: new Array(),
            nextItemIdx: 0,
            inTheQueue: 0,
            numOfSuccess: 0
        }
    }

    this.QueuedGet = async function (urls, numerOfThreds, handler) {

        queueState = initState();

        return new Promise(async function (resolve, reject) {

            if (numerOfThreds === undefined)
                numerOfThreds = 3;

            queueState.urls = urls;

            for (; queueState.nextItemIdx < numerOfThreds;) {

                var queueItem = {
                    promise: Nadam.Http.Get(urls[queueState.nextItemIdx]),
                    squenceNum: queueState.nextItemIdx,
                    url: urls[queueState.nextItemIdx]
                }
                ++queueState.nextItemIdx;

                queueItem.promise.then(
                    queueItemReduxer(queueItem.squenceNum, handler),
                    queueItemReduxer(queueItem.squenceNum)
                );

                queueState.queue.push(queueItem);
                ++queueState.inTheQueue;
            }


            var attempts = 0;
            while (queueState.nextItemIdx < urls.length || queueState.inTheQueue > 0) {

                ++attempts;
                if (queueState.inTheQueue < numerOfThreds && queueState.nextItemIdx < urls.length) {

                    var queueItem = {
                        promise: Nadam.Http.Get(urls[queueState.nextItemIdx]),
                        squenceNum: queueState.nextItemIdx,
                        url: urls[queueState.nextItemIdx]
                    }
                    ++queueState.nextItemIdx;

                    queueItem.promise.then(
                        queueItemReduxer(queueItem.squenceNum, handler),
                        queueItemReduxer(queueItem.squenceNum));

                    queueState.queue.push(queueItem);
                    ++queueState.inTheQueue;

                    --attempts;
                }
                else
                    await Sleep(600);

                if (attempts >= 10)
                    break;
            }

            if (queueState.numOfSuccess === queueState.urls.length)
                resolve(queueState.numOfSuccess + " out of: " + urls.length + " was done");
            reject({ failed: queueState.failedUrls, message: queueState.numOfSuccess + " out of: " + urls.length + " was done" });
        });
    }
};