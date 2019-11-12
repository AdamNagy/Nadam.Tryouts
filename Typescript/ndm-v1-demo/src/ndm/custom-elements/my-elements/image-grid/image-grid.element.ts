import { PrototypeHTMLElement } from "../../../htmlelement/prototype-html-element";
import { GalleryModel } from "./gallery.model";
import "./image-grid.element.mobile.scss";
import "./image-grid.element.scss";

class ImageMeta {
	public Thumbnail: string;
	public ViewImage: string;
}

// tslint:disable
export class ImageGrid extends PrototypeHTMLElement {

	// create config object: rootMargin and threshold
	// are two properties exposed by the interface
	public config = {
		rootMargin: "0px 0px 50px 0px",
		threshold: 0,
	};

	// register the config object with an instance
	// of intersectionObserver
	public observer = new IntersectionObserver((entries, self) => {
		// iterate over each entry
		entries.forEach((entry) => {
		// process just the images that are intersecting.
		// isIntersecting is a property exposed by the interface
		if (entry.isIntersecting) {
			// custom function that copies the path to the img
			// from data-src to src
			this.PreloadImage(entry.target as HTMLImageElement);
			// the image is now in place, stop watching
			self.unobserve(entry.target);
		}
		});
	}, this.config);

	private templateId = "template-image-grid";
	private itemTemplateId = "template-image-grid-item";
	private model: GalleryModel;
	private imageViewer: HTMLElement;
	private blurLayer: HTMLElement;
	private imageMetas: ImageMeta[] = [];
	private imageContainer: PrototypeHTMLElement;

	constructor(model: GalleryModel) {
		super();

		// this.model = model;

		const template = document.getElementById(this.templateId);
		const node = document.importNode((template as any).content, true);

		this.blurLayer = node.querySelector("div.blur-layer");
		this.imageViewer = node.querySelector("div.image-viewer");
		this.imageContainer = HTMLElement = node.querySelector("div.gallery-grid");
		this.append(node);

		// const itemTemplate = document.getElementById(this.itemTemplateId);
		// let imgIdx = -1;
		// for (const imageFileName of this.model.Images) {
		// 	const imageGridItem = document.importNode((itemTemplate as any).content, true);

		// 	const imageTitle = imageFileName.split(".")[0];
		// 	const imageElement: HTMLImageElement = imageGridItem.querySelector("img");

		// 	const imageMeta = this.GetViewImageSrcFor(this.model.Path, imageTitle);			
		// 	imageElement.setAttribute("data-src", imageMeta.Thumbnail);
		// 	imageElement.setAttribute("data-viewimage-src", imageMeta.ViewImage);
		// 	imageElement.setAttribute("data-idx", `${++imgIdx}`);
		// 	imageElement.addEventListener("click", () => this.SlideShow(imageElement));

		// 	this.imageMetas.push(imageMeta);

		// 	imageContainer.appendChild(imageGridItem);
		// 	this.observer.observe(imageElement);
		// }
	}

	public WithModel(gallery: GalleryModel): ImageGrid {
		
		this.model = gallery;

		const itemTemplate = document.getElementById(this.itemTemplateId);
		let imgIdx = -1;
		for (const imageFileName of this.model.Images) {
			const imageGridItem = document.importNode((itemTemplate as any).content, true);

			const imageTitle = imageFileName.split(".")[0];
			const imageElement: HTMLImageElement = imageGridItem.querySelector("img");

			const imageMeta = this.GetViewImageSrcFor(this.model.Path, imageTitle);			
			imageElement.setAttribute("data-src", imageMeta.Thumbnail);
			imageElement.setAttribute("data-viewimage-src", imageMeta.ViewImage);
			imageElement.setAttribute("data-idx", `${++imgIdx}`);
			imageElement.addEventListener("click", () => this.SlideShow(imageElement));

			this.imageMetas.push(imageMeta);

			this.imageContainer.appendChild(imageGridItem);
			this.observer.observe(imageElement);
		}
		return this;
	}

	private SlideShow(image: HTMLImageElement) {
		let currentImageIndex = Number(image.getAttribute("data-idx"));
		this.imageViewer.querySelector("img").setAttribute("src", image.getAttribute("data-viewimage-src"));

		$(this.imageViewer).show("fast");
		$(this.blurLayer).show("fast");
		document.body.style.overflowX = "hidden";

		// background click
		this.blurLayer.addEventListener("click", () => {
			$(this.imageViewer).hide("fast");
			$(this.blurLayer).hide("fast");
			document.body.style.overflowX = "scroll";
		});

		// escape
		$(document).keyup((e) => {
			if (e.key === "Escape") {
				$(this.imageViewer).hide("fast");
				$(this.blurLayer).hide("fast");
				document.body.style.overflowX = "scroll";
			}
		});

		// left arrow
		$(document).keyup((e) => {
			if (e.keyCode === 39) {
				const nextImage = this.imageMetas[++currentImageIndex];
				this.imageViewer.querySelector("img").setAttribute("src", nextImage.ViewImage);
			}
		});

		// right arrow
		$(document).keyup((e) => {
			if (e.keyCode === 37) {
				const nextImage = this.imageMetas[--currentImageIndex];
				this.imageViewer.querySelector("img").setAttribute("src", nextImage.ViewImage);
			}
		});

		// click on image
		this.imageViewer.addEventListener("click", () => {
			const nextImage = this.imageMetas[++currentImageIndex];
			this.imageViewer.querySelector("img").setAttribute("src", nextImage.ViewImage);
		});
	}

	private GetViewImageSrcFor(path: string, fileTitle: string): ImageMeta {
		if ( this.model.Path === "engagement_1" || this.model.Path === "engagement_2") {
			return  {
				Thumbnail: `https://nadam.blob.core.windows.net/wedding/${this.model.Path}/thumbnails/${fileTitle}_t.jpg`,
				ViewImage: `https://nadam.blob.core.windows.net/wedding/${this.model.Path}/viewimages/${fileTitle}_vi.jpg`
			}
		} else {
			return  {
				Thumbnail: `https://nadam.blob.core.windows.net/wedding/${this.model.Path}/thumbnails/${fileTitle}_t.jpg`,
				ViewImage: `https://nadam.blob.core.windows.net/wedding/${this.model.Path}/viewimages/${fileTitle}.JPG`
			}
		}
	}

	private PreloadImage(image: HTMLImageElement) {
		image.setAttribute("src", image.getAttribute("data-src"));
	}
}

customElements.define("nadam-image-grid", ImageGrid);
