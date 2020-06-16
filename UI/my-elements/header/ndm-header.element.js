
class HeaderElementConfig {
	Parallax = false;
	Carouse = false;
	CarouseConfig = {};
}

// default
class SlickCarouselConfig {
	dots = false;
	arrows = false;
	infinite = true;
	speed = 300;
	slidesToShow = 4; 
	slidesToScroll = 3;
	autoplay = true;
	autoplaySpeed = 3000;
	responsive = [
		{
			breakpoint: 1024,
			settings: {
				slidesToShow: 3,
				slidesToScroll: 3,
				infinite: true,
				dots: true
			}
		},
		{
			breakpoint: 600,
			settings: {
				slidesToShow: 2,
				slidesToScroll: 2
			}
		},
		{
			breakpoint: 480,
			settings: {
				slidesToShow: 1,
				slidesToScroll: 1
			}
		}
	]
}

class HeaderElement extends HTMLParagraphElement {
	
	constructor() {	  
		super();
  

	}
}

customElements.define('ndm-header', HeaderElement);