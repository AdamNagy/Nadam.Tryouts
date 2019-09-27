Window.Nadam = Window.Nadam || {};
Window.Nadam.store = {};

Window.Nadam.store.galleryes = [
	{
		id: 1,
		title: "Mixed landscape and portrait",
		imageSources: 
		[
			"./images/thumbs/1457081771_t.jpg",
			"./images/thumbs/570272_t.jpg",
			"./images/thumbs/9d7f0c19c77330eafebdd607558c4c57_t.jpg",
			"./images/thumbs/JEF_059119_1600x2560.jpg",
			"./images/thumbs/rock_hills_trees_rocks_105901_1920x1080_t.jpg"
		]
	}
];
			
Window.Nadam.store.templates = [
	{
		name: "galleryThumbnail",
		markup: `
			<div class="gallery">
				<div class="gallery-header row align-self-center">
					<h4>#title</h4>
				</div>
				<div class="gallery-body">
					#imageSources
				</div>
				<div class="gallery-footer">
					<a href="#gallery-link">Link to original source</a>
				</div>
			</div>
		`
	}
];

var GallerySelector = {

	GetById: function(_id) {

		let all = this.GetAll(); 
		for(let idx = 0; idx < all.length; ++idx) {
			if( all[idx]["id"] === _id )
				return all[idx];
		} 
	},

	GetAll: function() {

		return Window.Nadam.store.galleryes;
	}
}

Window.Nadam.selectors = {};
Window.Nadam.Selectors.gallerySelector = GallerySelector;