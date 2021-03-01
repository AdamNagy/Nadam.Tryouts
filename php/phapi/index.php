<?php
	function __autoload($class_name) {

		if( file_exists('./Core/'.$class_name.'.php'))  {
			require_once './Core/'.$class_name.'.php';

		} else if( file_exists('./Controllers/'.$class_name.'.php') ) {
			require_once './Controllers/'.$class_name.'.php';

		} else if( file_exists('./Models/'.$class_name.'.php') ) {
			require_once './Models/'.$class_name.'.php';
		}
	}

	$routes = ["manifest"];
	$routeTree = array(
		"manifest"=> array(
			// 'thumbnails' word does not work, like it would be a foglalt kulcsszĂł in PHP
			"thumbnails" => "GetThumbnails",
			"gallery" => "GetGallery",
			// 'thumba' just works
			"index" => "GetThumbnails"
			)
		);
		
	$urlSegments = explode('/', $_SERVER['REQUEST_URI']);

	$controllerName = "";
	$actionName = "index";
	
	// iterate throught the $urlSegments, when passed "api" then the first is the controller
	// second is the action and so
	// the last is which contains a '?' or does not end with '/'
	$idx = 0;
	for(; $idx < count($urlSegments); $idx++) {
		if( $urlSegments[$idx] == "api" ) {
			break;
		}
	}

	// echo " idx: ".$idx;
	
	if (isset($urlSegments[++$idx])) {
		// echo " controller: ".$urlSegments[$idx]." ";
		$controllerName = $urlSegments[$idx];
	}
	
	if (isset($urlSegments[++$idx]) ) {
		$slug = explode('?', $urlSegments[$idx]);
		$actionName = $slug[0];
		// echo " action name: ".$actionName." ";
	}

	$pathSegments = array();
	if( ++$idx < count($urlSegments) ) {
		for(; $idx < count($urlSegments); $idx++) {
			$x = explode('?', $urlSegments[$idx]);
			$pathSegments[] = $x[0];
		}
	}

	//echo "path segments: ";
	//print_r($pathSegments);

	$controller;

	switch($controllerName) {
		case "manifest":
			$controller = new ManifestController($_SERVER['QUERY_STRING'], $pathSegments);
			break;
		default: die("no controller");
	}
		
	$responseModel = new ThumbnailsResponse();
	$action = $routeTree[$controllerName][$actionName];

	$responseModel = $controller->{$action}();
	
	header("Content-Type: application/json");
	// header("Access-Control-Allow-Origin: *");
	// header("Access-Control-Allow-Methods: *");
	echo json_encode($responseModel);
?>