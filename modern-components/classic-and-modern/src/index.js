// import $ from "jquery";
import 'jquery-ui/ui/widgets/draggable';
import 'jquery-ui/ui/widgets/resizable';

(function() {

	$('h1').css('color', 'red');

	$( "#draggable" ).draggable();
	$( "#resizable" ).resizable();
})()