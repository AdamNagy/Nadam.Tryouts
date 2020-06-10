<?php
	include 'mvc.class.php';
	$queryStringKeyValues = Mvc::ParseQueryString($_SERVER['QUERY_STRING']);
	$fileName = $queryStringKeyValues["name"];

	$file = fopen("./$fileName.js", "r") or die("Unable to open file!");
	$fileContent = fread($file, filesize("./$fileName.js"));
	fclose($file);
	
	header("Access-Control-Allow-Origin: *");
	header("content-type: application/javascript");
	echo $fileContent;
?>