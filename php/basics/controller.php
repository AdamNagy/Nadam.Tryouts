<?php
	include 'mvc.class.php';

	function Get($fileName) {
		$thumbsFolderName = "./json-db";
	}
	
	$queryStringKeyValues = Mvc::ParseQueryString($_SERVER['QUERY_STRING']);
	Echo "fileName: ";
	Echo $queryStringKeyValues["fileName"];

	$controller = Mvc::GetActionName($_SERVER['REQUEST_URI'], "controller");

	Echo " | REQUEST_URI: ";
	Echo $_SERVER['REQUEST_URI'];

	Echo " | controller: ";
	Echo $controller;
?>