function Sleep(ms: number): Promise<any> {
    return new Promise(resolve => setTimeout(resolve, ms));
}

class QueueItem {
	promise: Promise<any>;
	squenceNum: number;
	url: string;}

class QueueState {

	queue: Array<QueueItem> = new Array();
	urls: Array<string> = new Array();
	failedUrls: Array<string> = new Array();
	nextItemIdx: number = 0;
	inTheQueue: number = 0;
	numOfSuccess: number = 0;
}

export class Http {

    static async Get(url: string): Promise<string> {
        return new Promise(function (resolve: any, reject: any): void {

            if (url === undefined) {
                reject("Url is null");
			}

            const request: XMLHttpRequest = new XMLHttpRequest();
            request.timeout = 10000;
            /**
             * xmlhttp.readyState enum:
             * 0	UNSENT	Client has been created. open() not called yet.
             * 1	OPENED	open() has been called.
             * 2	HEADERS_RECEIVED	send() has been called, and headers and status are available.
             * 3	LOADING	Downloading; responseText holds partial data.
             * 4	DONE	The operation is complete.
             */
            request.onreadystatechange = function (): void {
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
            } catch {
                reject({ readyState: request.readyState, status: request.status });
            }
        });
    }

	queueState: QueueState;

    queueItemReduxer(squenceNum: number, handler: any): any {
        return (result: any) => {
            for (let i: number = 0; i < this.queueState.queue.length; ++i) {
                if (this.queueState.queue[i] !== undefined && this.queueState.queue[i].squenceNum === squenceNum) {
					let startIdx: number = this.queueState.queue.indexOf(this.queueState.queue[i]);
                    this.queueState.queue.splice(startIdx, 1);
                    --this.queueState.inTheQueue;
                }
            }

            if (handler !== undefined) {
                this.queueState.numOfSuccess = this.queueState.numOfSuccess + 1;
                handler(result);
            }
        };
    }

    async QueuedGet(urls: Array<string>, numerOfThreds: number, handler: any): Promise<string> {

        this.queueState = new QueueState();

        return new Promise( async (resolve, reject) => {

                if (numerOfThreds === undefined) {
                    numerOfThreds = 3;
				}

                this.queueState.urls = urls;

                for (; this.queueState.nextItemIdx < numerOfThreds;) {

                    let queueItem: QueueItem = {
                        promise: Http.Get(urls[this.queueState.nextItemIdx]),
                        squenceNum: this.queueState.nextItemIdx,
                        url: urls[this.queueState.nextItemIdx]
                    };

                    ++this.queueState.nextItemIdx;

                    queueItem.promise.then(
                        this.queueItemReduxer(queueItem.squenceNum, handler),
                        this.queueItemReduxer(queueItem.squenceNum, null));

                    this.queueState.queue.push(queueItem);
                    ++this.queueState.inTheQueue;
                }

                var attempts: number = 0;
                while (this.queueState.nextItemIdx < urls.length || this.queueState.inTheQueue > 0) {

                    ++attempts;
                    if (this.queueState.inTheQueue < numerOfThreds && this.queueState.nextItemIdx < urls.length) {

                        let queueItem: QueueItem = {
                            promise: Http.Get(urls[this.queueState.nextItemIdx]),
                            squenceNum: this.queueState.nextItemIdx,
                            url: urls[this.queueState.nextItemIdx]
                        };
                        ++this.queueState.nextItemIdx;

                        queueItem.promise.then(
                            this.queueItemReduxer(queueItem.squenceNum, handler),
                            this.queueItemReduxer(queueItem.squenceNum, null));

                        this.queueState.queue.push(queueItem);
                        ++this.queueState.inTheQueue;

                        --attempts;
                    } else {
                        await Sleep(600);
                    }

                    if (attempts >= 10) {
                        break;
					}
                }

                if (this.queueState.numOfSuccess === this.queueState.urls.length) {
                    resolve(this.queueState.numOfSuccess + " out of: " + urls.length + " was done");
				}

                reject({
					failed: this.queueState.failedUrls,
					message: this.queueState.numOfSuccess + " out of: " + urls.length + " was done"
				});
            });
     }
}