// this element not ready yet, it does not work from JS.
// needs a template

class GalleryControl extends HTMLElement {
	
	store = [];
	modal = {};
	isFirstLoad = true;

	constructor() {
		super();
	}

	async loadPage() {

		var title = window.app.router.queryStringDict.find(item => item.key === "title").value;

		if( !title ) {
			return;
		}

		var galleryModel = await this.getModel(title);

		var imageElements = [];
		var spotlightGallery = galleryModel.images.Select((meta) => { 
			return {
				src: meta.realImageSource,
				description: "no description",
				title: meta.realImageSource.split('/').Last()
			} });
		var spotlightIdx = 0;

		for(var imageMeta of galleryModel.images) {
			var imageElement = document.createElement("img")
				.WithAttribute("data-src", imageMeta.thumbnailImageSource)
				.WithAttribute("data-real-src", imageMeta.realImageSource)
				.WithOnClick( ((idx) => () => {
					Spotlight.show(spotlightGallery, {
						index: idx,
						theme: "white",
						autohide: false,
						control: ["autofit", "zoom"]
					})
				})(++spotlightIdx))
			imageElements.push(imageElement);
		}

		this.$nid("content-section")
			.WithoutChildren()
			.WithChild(new ColumnLaneElement({
				containerWidth: 1200,
				itemFirst: false,
				numOfColumns: 4,
				columnGap: 0,
				rowGap: 0,
				includeControlPanel: true,
			}, imageElements));

		this.$nid("label-title").WithInnerText(galleryModel.title);
	}

	async getModel(title) {

		var modelPromis = new Promise((resolve, reject) => {

			var galleryModel = this.store[title];
			
			if(galleryModel) {
				resolve(galleryModel);
			} else {
				$.get(`${window.app.apiConfig.galleryApi}?title=${title}`,
					(() => (response) => {
						
						console.log("going to api");
						var responseModel = JSON.parse(response);
		
						var galleryModel = JSON.parse(responseModel.galleryFile.content);
						if( responseModel.galleryFile.type === "stored-gallery" ) {
							this.store[title] = this.mapStoredGalleryModel(galleryModel);
						} else {
							this.store[title] = galleryModel;
						}

						resolve(this.store[title]);	
					})()
				);
			}			
		});

		return modelPromis;
	}

	// runs each time the element is added to the DOM
	connectedCallback() {
		// if( this.isFirstLoad ) {
			this.loadPage();
			this.$nid("btn-back").WithOnClick(() => {
				window.app.router.activateRoute("home");
			}).WithStyle("zIndex", "1021");
	
			this.model = new ModalElement();

			this.isFirstLoad = false;
		// }
	}

	mapStoredGalleryModel(storedGalleryModel) {
		var mappedModel = {
			title: storedGalleryModel.Title,
			route: storedGalleryModel.route
		};

		mappedModel.images = storedGalleryModel.images.Select((item) => {
			return {
				thumbnailImageSource: `${window.app.contentRoot}${storedGalleryModel.route}/thumbnails/${item}`,
				realImageSource:  `${window.app.contentRoot}${storedGalleryModel.route}/${item}`,
			}
		});

		return mappedModel;
	}
}

customElements.define('ndm-gallery-control', GalleryControl);
