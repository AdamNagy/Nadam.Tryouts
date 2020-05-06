declare class QueueItem {
    promise: Promise<any>;
    squenceNum: number;
    url: string;
}
declare class QueueState {
    queue: Array<QueueItem>;
    urls: Array<string>;
    failedUrls: Array<string>;
    nextItemIdx: number;
    inTheQueue: number;
    numOfSuccess: number;
}
export declare class Http {
    static Get(url: string): Promise<string>;
    queueState: QueueState;
    queueItemReduxer(squenceNum: number, handler: any): any;
    QueuedGet(urls: Array<string>, numerOfThreds: number, handler: any): Promise<string>;
}
export {};
