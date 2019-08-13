<?php
	function HandShake($echo) {
		return $echo;
	}

	function Get($fileName) {
		$thumbsFolderName = "./json-db";
	}

	$queryString =  $_SERVER['QUERY_STRING'];
	$route = $_SERVER['REQUEST_URI'];

	Echo '{queryString:';
	Echo $queryString;
	Echo ', route:';
	Echo $route;
	Echo '}';
?>