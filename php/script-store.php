<?php

	

	$file = fopen("./some-script.js", "r") or die("Unable to open file!");
	$fileContent = fread($file, filesize("./some-script.js"));
	fclose($file);
	
	header("Access-Control-Allow-Origin: *");
	header("content-type: application/javascript");
	echo $fileContent;
?>