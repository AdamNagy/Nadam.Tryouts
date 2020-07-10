class GalleryThumbnailContainer extends HTMLElement {
	
	model = {};
	data = {};
	isFirstLoad = true;

	constructor(_model) {
		super();
		this.model = _model;
	}

	// creates a bootstrap card element from a gallery model
	createCardElement(galleryThumb) {
		var cardElement = document.createElement("div")
			.WithClasses(["card mb-2"]);

		var cardHeaderElement = document.createElement("div")
			.WithClasses(["card-header", "text-center"]);

		var cardTitle = document.createElement("h3")
			.WithClasses(["h3"])
			.WithInnerText(galleryThumb.Title);

		cardHeaderElement.WithChild(cardTitle);

		var cardBodyElement = document.createElement("div")
			.WithClasses(["card-body", "d-flex", "flex-wrap", "justify-content-around"]);

		for(var imgMeta of galleryThumb.ImagesMetaData) {

			if(imgMeta.thumbnailImageSrc.endsWith(".gif")) {
				continue;
			}

			var imageWall = document.createElement("div")
				.WithClass("justify-content-center");

			var imageElement = new Image();
			imageElement.src = imgMeta.thumbnailImageSrc;
			imageElement.WithClasses(["img", "border", "border-dark", "mb-1"]);
			imageWall.append(imageElement);

			cardBodyElement.append(imageWall);
		}

		var cardFooter = document.createElement("div").WithClass("card-footer")
			.WithChild(
				document.createElement("button").WithInnerText(galleryThumb.Title)
					.WithOnClick(() => {
						window.app.router.activateRoute(`gallery?title=${galleryThumb.fileName}`);
					})
			)

		cardElement.append(cardHeaderElement);
		cardElement.append(cardBodyElement);
		cardElement.append(cardFooter);

		return cardElement;
	}

	// runs each time the element is added to the DOM
	connectedCallback() {
		if( this.isFirstLoad ) {
			for(var thumbnailModel of this.model) {
				this.append(this.createCardElement(thumbnailModel));
			}

			this.isFirstLoad = false;
		}
	}
}

customElements.define('ndm-gallery-thumbnail-container', GalleryThumbnailContainer);
