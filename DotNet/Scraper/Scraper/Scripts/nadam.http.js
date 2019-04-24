var Http = {
	Get: function(src) {
		return new Promise(function(resolve, reject) {
			const oReq = new XMLHttpRequest();
			oReq.onload = function(e) {
				resolve(oReq.response);
			}

			oReq.open("GET", src);
			oReq.send();
		});
	}
};