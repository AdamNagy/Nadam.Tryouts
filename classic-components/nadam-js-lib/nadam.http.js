function Sleep(ms) {
	return new Promise(resolve => setTimeout(resolve, ms));
}

window.Nadam = window.Nadam || {}; 
Nadam.Http = {
	Get: function(url) {
		return new Promise(function(resolve, reject) {
			const oReq = new XMLHttpRequest();
			oReq.onload = function(e) {
				resolve(oReq.response);
			}

			oReq.open("GET", url);
			oReq.send();
		});
	},

	QueuedGet: async function (urls, numerOfThreds, handler) {
		return new Promise(async (resolve, reject) => {		
		
			if( numerOfThreds === undefined )
				numerOfThreds = 3;

			var promiseQueue = new Array(numerOfThreds);
			var idx = 0, inTheQueue = 0;

			for( ; idx < numerOfThreds; ) {
				var queueItem = {
					promise: Http.Get(urls[idx]),
					squenceNum: idx
				}
				++idx;

				queueItem.promise.then((function(promiseIdx, queue) {
					return function(result) {

						for(var i = 0; i < queue.length; ++i) {
							if(queue[i] !== undefined && queue[i].squenceNum === promiseIdx) {
								queue.splice(queue[i], 1);
								--inTheQueue;
							}
						}

						handler(result);						
					}
				})(queueItem.squenceNum, promiseQueue));

				promiseQueue.push(queueItem);
				++inTheQueue;
			}
			
			var attempts = 0;
			while( idx < urls.length) {				

				attempts++;
				if( inTheQueue < numerOfThreds ) {
						
					--attempts;
						
					var queueItem = {
						promise: Http.Get(urls[idx]),
						squenceNum: idx
					}
					++idx;
					
					queueItem.promise.then((function(promiseIdx, queue) {
						return function(result) {
							
							for(var i = 0; i < queue.length; ++i) {
								if(queue[i] !== undefined && queue[i].squenceNum === promiseIdx) {
									queue.splice(queue[i], 1);
									--inTheQueue;
								}
							}
							
							handler(result);							
						}
					})(queueItem.promiseIdx, promiseQueue));
						
					
					promiseQueue.push(queueItem);
					++inTheQueue;
				}

				if(attempts > 3) {
					
					break;
				}

				await Sleep(400);
			}	
				
			if( idx === urls.length )
				resolve();
			else
				reject();
		});		 
	}
};