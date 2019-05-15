import { setTimeout } from "timers";

function Sleep(ms) {
    return new Promise(resolve => setTimeout(resolve, ms));
}


export class Http {

    static async Get(url) {
        return new Promise(function (resolve, reject) {

            if (url === undefined)
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
                } else if (request.readyState === 4 && request.status === 0) {
                    reject({ readyState: request.readyState, status: request.status });
                } else if (request.readyState === 4 && request.status !== 200) {
                    reject({ readyState: request.readyState, status: request.status });
                }
            };

            try {
                request.open("GET", url);
                request.send();
            }
            catch{
                reject({ readyState: request.readyState, status: request.status });
            }
        });
    }

    constructor() {

        this.queueState = this.initState();
    }

    queueItemReduxer(squenceNum, handler) {
        return (result) => {
            for (var i = 0; i < this.queueState.queue.length; ++i) {
                if (this.queueState.queue[i] !== undefined && this.queueState.queue[i].squenceNum === squenceNum) {
                    this.queueState.queue.splice(this.queueState.queue[i], 1);
                    --this.queueState.inTheQueue;
                }
            }

            if (handler !== undefined) {
                this.queueState.numOfSuccess = this.queueState.numOfSuccess + 1;
                handler(result);
            }
        };
        
    }    

    initState() {

        return {
            queue: new Array(),
            urls: new Array(),
            failedUrls: new Array(),
            nextItemIdx: 0,
            inTheQueue: 0,
            numOfSuccess: 0
        };
    }

    async QueuedGet(urls, numerOfThreds, handler) {

        this.queueState = this.initState();

        return new Promise( async (resolve, reject) => {

                if (numerOfThreds === undefined)
                    numerOfThreds = 3;

                this.queueState.urls = urls;

                for (; this.queueState.nextItemIdx < numerOfThreds;) {

                    let queueItem = {
                        promise: Http.Get(urls[this.queueState.nextItemIdx]),
                        squenceNum: this.queueState.nextItemIdx,
                        url: urls[this.queueState.nextItemIdx]
                    };

                    ++this.queueState.nextItemIdx;

                    queueItem.promise.then(
                        this.queueItemReduxer(queueItem.squenceNum, handler),
                        this.queueItemReduxer(queueItem.squenceNum));

                    this.queueState.queue.push(queueItem);
                    ++this.queueState.inTheQueue;
                }
                
                var attempts = 0;
                while (this.queueState.nextItemIdx < urls.length || this.queueState.inTheQueue > 0) {

                    ++attempts;
                    if (this.queueState.inTheQueue < numerOfThreds && this.queueState.nextItemIdx < urls.length) {

                        let queueItem = {
                            promise: Http.Get(urls[this.queueState.nextItemIdx]),
                            squenceNum: this.queueState.nextItemIdx,
                            url: urls[this.queueState.nextItemIdx]
                        };
                        ++this.queueState.nextItemIdx;

                        queueItem.promise.then(
                            this.queueItemReduxer(queueItem.squenceNum, handler),
                            this.queueItemReduxer(queueItem.squenceNum));

                        this.queueState.queue.push(queueItem);
                        ++this.queueState.inTheQueue;

                        --attempts;
                    }
                    else {
                        // setTimeout(() => { }, 600);
                        await Sleep(600);
                    }

                    if (attempts >= 10)
                        break;
                }

                if (this.queueState.numOfSuccess === this.queueState.urls.length)
                    resolve(this.queueState.numOfSuccess + " out of: " + urls.length + " was done");
                reject({ failed: this.queueState.failedUrls, message: this.queueState.numOfSuccess + " out of: " + urls.length + " was done" });
            }); 
     }
}