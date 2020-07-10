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
		var spotlightGallery = galleryModel.ImagesMetaData.Select((meta) => { 
			return {
				src: meta.realImageSrc,
				description: "no description",
				title: meta.realImageSrc.split('/').Last()
			} });
		var spotlightIdx = 0;

		for(var imageMeta of galleryModel.ImagesMetaData) {
			var imageElement = document.createElement("img")
				.WithAttribute("data-src", imageMeta.thumbnailImageSrc)
				.WithAttribute("data-real-src", imageMeta.realImageSrc)
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

		this.$nid("label-title").WithInnerText(galleryModel.Title);
	}

	async getModel(title) {

		var modelPromis = new Promise((resolve, reject) => {

			var galleryModel = this.store[title];
			
			if(galleryModel) {
				resolve(galleryModel);
			} else {
				$.get(`http://localhost:8081/miv/manifest/gallery/?title=${title}`,
					(() => (response) => {
						
						console.log("going to api");
						var responseModel = JSON.parse(response);
		
						this.store[title] = JSON.parse(responseModel.galleryFile.content);
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
}

customElements.define('ndm-gallery-control', GalleryControl);
