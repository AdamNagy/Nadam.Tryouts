// import $ from "jquery";	// OPTION 1 to include jquery, other is in webpack.config
import 'jquery-ui/ui/widgets/draggable';
import 'jquery-ui/ui/widgets/resizable';


(function() {

	$('h1').css('color', 'red');

	$( "#draggable" ).draggable();
	$( "#resizable" ).resizable();
})()