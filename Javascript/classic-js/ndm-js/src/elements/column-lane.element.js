class ColumnLaneElement extends HTMLElement {

	optionsMenuTemplate = `
		<nav class="navbar navbar-expand-lg navbar-dark sticky-top bg-dark" id="mainNav">
			<div class="container">
				
				<button class="navbar-toggler navbar-toggler-right"
					type="button"
					data-toggle="collapse"
					data-target="#navbarResponsive"
					aria-controls="navbarResponsive"
					aria-expanded="false"
					aria-label="Toggle Navigation">
						<i class="fa fa-bars"></i>
				</button>

				<div class="collapse navbar-collapse row justify-content-center" id="navbarResponsive">
					<div>
						<ul class="navbar-nav text-uppercase ml-auto">
							<li class="nav-item nav-link js-scroll-trigger text-center" nid="btn-toggle-column-margin">Oszlop margó</li>
							<li class="nav-item nav-link js-scroll-trigger text-center" nid="btn-toggle-item-margin">Kép margó</li>

							<li class="nav-item dropdown">
								<a class="nav-link dropdown-toggle"
									id="navbarDropdown"
									role="button"
									data-toggle="dropdown"
									aria-haspopup="true"
									aria-expanded="false">
										Oszlopok száma
								</a>

								<div nid="column-options-container" class="dropdown-menu" aria-labelledby="navbarDropdown">
									<a class="dropdown-item" role="button">1</a>
									<a class="dropdown-item" role="button">2</a>
									<a class="dropdown-item" role="button">3</a>
									<a class="dropdown-item" role="button">4</a>
									
									<a class="dropdown-item" role="button">5</a>
									<a class="dropdown-item" role="button">6</a>
									<a class="dropdown-item" role="button">7</a>
									<a class="dropdown-item" role="button">8</a>
								</div>
							</li>
						</ul>
					</div>
				</div>
			</div>
		</nav>
		`;

	config = {};
	items = [];
	observer;
	isFirstInit = true;

	portraitImageWidth = 0;
	landscapeImageWidth = 0;
	numOfPortraits = 0;
	numOfLandscapes = 0;

	constructor(config, items) {
		super();

		this.items = items || [];
		this.config = config || {};

		this.WithStyle("display", "block");

		var domParser = new DOMParser();
		var optionsMenu = domParser.parseFromString(this.optionsMenuTemplate, "text/html")
			.querySelector("nav");

		optionsMenu.$nid("btn-toggle-column-margin")
			.WithOnClick(() => {
				config.columnGap = config.columnGap === 10 ? 0 : 10;
				this.Init(config);
			});

		optionsMenu.$nid("btn-toggle-item-margin")
			.WithOnClick(() => {
				config.rowGap = config.rowGap === 10 ? 0 : 10;
				this.Init(config);
			});
		
		var columnNumber = 0;
		for(var columnNumberOption of optionsMenu.$nid("column-options-container").$("a.dropdown-item") ) {
			columnNumberOption.WithOnClick( ((_columnNumber) => () => this.Init(Object.assign(config, { numOfColumns: _columnNumber } )))(++columnNumber) );
		}

		this.WithChildren([
			optionsMenu,
			document.createElement("div")
				.WithAttribute("nid", "gallery-row")
				.WithClass("mx-auto")
		]);

		
		// this.Init(config, true);
	}

	Init(config, isFirstInit = false) {

		if( isFirstInit ) {
			// lazy loading observation
			this.observer = new IntersectionObserver((entries, self) => {
				entries.forEach((entry) => {
					if (!entry.isIntersecting)
						return true;
	
					this.LoadImage(entry.target);
					self.unobserve(entry.target);
					
				});
			});
		}

		this.$nid("gallery-row").WithoutChildren();
		for (let i = 0; i < config.numOfColumns; ++i) {
			this.$nid("gallery-row").WithChild(
				document.createElement("div").WithClass("column")
					.WithAttribute("nid", `column-${i}`)
					.WithStyle("padding", "0px")
					.WithStyle("max-width", `${100 / config.numOfColumns}%`),
			);
		}

		// put images into the columns
		for (let i = 0; i < this.items.length; ++i) {
			this.$nid("gallery-row").$nid(`column-${i % config.numOfColumns}`).WithChild(
				document.createElement("div") //.WithAttribute("href", this.items[i].getAttribute("data-real-src"))
				.WithChild(
					this.items[i].WithStyle("opacity", `${isFirstInit ? 0 : 1}`)
						.WithStyle("transition", "opacity .4s")
						.WithStyle("margin-bottom", `${config.rowGap}px`)
						.WithStyle("min-height", "70px")
						.WithOnLoad((event) => this.ClassifyImage(event))
						.WithEventListener("error", () => {
							this.items[i].WithStyle("display", "none")
						}),
				)
			);

			if( isFirstInit ) {
				this.observer.observe(this.items[i]);
			}
		}

		// additional styles as per the configuration
		if ( config.columnGap > 0 ) {
			let containerWidth = config.containerWidth;

			for (let i = 1; i < config.numOfColumns; ++i) {
				this.$nid("gallery-row").$nid(`column-${i}`)
					.WithStyle("margin-left", `${config.columnGap}px`);

				containerWidth += config.columnGap;
			}
			// this.WithStyle("width", `${containerWidth}px`);
		}
	}

	LoadImage(image) {
		image.WithAttribute("src", image.getAttribute("data-src"));
	}

	ClassifyImage(event) {

		const image = event.target;
		image.WithAttribute("original-width", image.Width())
			.WithAttribute("original-height", image.Height());

		// landscape
		if ( image.Width() > image.Height() ) {
			image.WithClass("landscape-image");
			++this.numOfLandscapes;
			if (this.portraitImageWidth > 0)
				image.WithStyle("width", `${this.portraitImageWidth}px`);

		// portrait
		} else if ( image.Width() < image.Height() ) {
			image.WithClass("portrait-image");
			++this.numOfPortraits;
			if (this.portraitImageWidth === 0)
				this.portraitImageWidth = parseInt(image.Width(), 0);

		} else
			image.WithClass("rectangle-image");
			if (this.portraitImageWidth > 0)
				image.WithStyle("width", `${this.portraitImageWidth}px`);

		image.WithStyle("opacity", "1");
	}

	connectedCallback() {
		if(this.isFirstInit) {
			this.Init(this.config, true);
			this.isFirstInit = false;
		}
	}
}

customElements.define("ndm-column-lane", ColumnLaneElement);

const DefaultConfig = {
	containerWidth: 1200,
	itemFirst: false,
	numOfColumns: 4,
	columnGap: 0,
	rowGap: 0,
	includeControlPanel: true,
};
