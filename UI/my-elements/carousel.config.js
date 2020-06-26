// Single slides carousel config
var SingleSlideCarouselConfig = {
	dots : true,
	arrows : false,
	infinite : true,
	speed : 300,
	slidesToShow : 1, 
	slidesToScroll : 1,
	autoplay : false,
	autoplaySpeed : 2000,
	lazyLoad : "ondemand",
	responsive : [
		{
			breakpoint: 1200,
			settings: {
				slidesToShow: 1,
				slidesToScroll: 1,
			}
		},
		{
			breakpoint: 992,
			settings: {
				slidesToShow: 1,
				slidesToScroll: 1,
			}
		},
		{
			breakpoint: 768,
			settings: {
				slidesToShow: 1,
				slidesToScroll: 1,
				infinite: true,
				dots: true
			}
		},
		{
			breakpoint: 576,
			settings: {
				slidesToShow: 1,
				slidesToScroll: 1,
				infinite: true,
				dots: true
			}
		}
	]
}

// Multiple slides carousel config
var MultiSlideCarouselConfig = {
	dots : true,
	arrows : false,
	infinite : true,
	speed : 300,
	slidesToShow : 4, 
	slidesToScroll : 3,
	autoplay : false,
	autoplaySpeed : 3000,
	variableWidth : true,
	lazyLoad : "ondemand",
	responsive : [
		{
			breakpoint: 1200,
			settings: {
				slidesToShow: 4,
				slidesToScroll: 3,
			}
		},
		{
			breakpoint: 992,
			settings: {
				slidesToShow: 3,
				slidesToScroll: 2,
			}
		},
		{
			breakpoint: 768,
			settings: {
				slidesToShow: 3,
				slidesToScroll: 2,
				infinite: true,
				dots: true
			}
		},
		{
			breakpoint: 576,
			settings: {
				slidesToShow: 1,
				slidesToScroll: 1,
				infinite: true,
				dots: true
			}
		}
	]
}