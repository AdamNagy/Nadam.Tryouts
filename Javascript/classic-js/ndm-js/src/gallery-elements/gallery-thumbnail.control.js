// this element not ready yet, it does not work from JS.
// needs a template

class GalleryThumbnailControl extends HTMLElement {
	
	config = {};
	data = {};
	isFirstLoad = true;
	queryStringObject = {};

	constructor(_config) {
		super();

		this.queryStringObject = {
			page: 1,
			pagesize: 10,
			title: ""
		};

	}

	loadPage(queryStrintObj) {		

		$.get(`http://localhost:8081/miv/manifest/thumbnails/?page=${queryStrintObj.page}&pagesize=${queryStrintObj.pagesize}`,
			(() => (response) => {

				var responseModel = JSON.parse(response);

				if( this.isFirstLoad ) {					
					this.$nid('pager').init(responseModel.pages, responseModel.currentPage);				
					this.isFirstLoad = false;
				}

				this.$nid("content-section").WithoutChildren();
				var thumbnailModels = [];
				for(var thumbnailFile of responseModel.thumbnailFiles) {
					var galleryThumbnailModel = JSON.parse(thumbnailFile.content);

					if(galleryThumbnailModel === undefined || galleryThumbnailModel === null)
						continue;

					galleryThumbnailModel.fileName = thumbnailFile.fileName;
					thumbnailModels.push(galleryThumbnailModel);
				}

				this.$nid("content-section").WithChild(new GalleryThumbnailContainer(thumbnailModels));

			})() );
	}

	// runs each time the element is added to the DOM
	connectedCallback() {
		if( this.isFirstLoad ) {
			var pageQueryParam = window.app.router.queryStringDict.find(item => item.key === "page");
			var pageValue = pageQueryParam ? pageQueryParam.value : "1";
			this.queryStringObject = Object.assign({}, this.queryStringObject, { page: pageValue });
			this.loadPage( this.queryStringObject );

			this.$nid('pager').WithEventListener("paging", (event) => {
				var queryParam = Object.assign({}, this.queryStringObject, { page: event.detail.page });
				this.loadPage(queryParam);
				this.$nid('pager').setActive(queryParam.page);
				window.app.router.setQueryParam("page", queryParam.page);
			});
		}		
	}
}

customElements.define('ndm-gallery-thumbnail-control', GalleryThumbnailControl);
