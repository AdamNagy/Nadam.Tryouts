<?php

require_once('routes.php');

function __autoload($class_name) {

	if( file_exists('./classes/'.$class_name.'.php')) {
		require_once './classes/'.$class_name.'.php';

	} else if( file_exists('./Controllers/'.$class_name.'.php') ) {
		require_once './Controllers/'.$class_name.'.php';
	}
}

$controller = "";
$action = "";

if( isset($_POST['query'])) {
	
	echo "using POST\n";
	
	$query = json_decode($_POST["query"]);
	$controller_propName = "controller";
	$action_propName = "action";
	$props_propName = "params";

	$controller = $query -> $controller_propName;
	$action = $query -> $action_propName;

} else {
	echo "using GET\n";
	$urlSegments = explode('/', $_GET['url']);

	if( isset($urlSegments[2]) ) {
		$controller = $urlSegments[1];
		$action = $urlSegments[2];
	} else {
		$controller = $urlSegments[1];
		$action = "index";
	}
}

echo "controller \n";
echo $controller;

echo "\n\naction \n";
echo $action;

// echo "\n\nprops \n";
// echo var_dump($query -> $props_propName);

// echo "\n\nquery as string\n";
// echo $_POST["query"];
?>