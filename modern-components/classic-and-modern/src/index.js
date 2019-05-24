// import $ from "jquery";	// OPTION 1 to include jquery, other is in webpack.config
import 'jquery-ui/ui/widgets/draggable';
import 'jquery-ui/ui/widgets/resizable';

// var $ = require('jquery');
// var jQueryBridget = require('jquery-bridget');
// var Masonry = require('masonry-layout');

jQueryBridget( 'masonry', Masonry, $ );


(function() {

	$('h1').css('color', 'red');

	$( "#draggable" ).draggable();
	$( "#resizable" ).resizable();

	$('.grid').masonry({
		columnWidth: 100
	  });
})()