<?php
	
	$file = fopen("./json-db/test.json", "r") or die("Unable to open file!");
	$fileContent = fread($file, filesize("./json-db/test.json"));
	fclose($file);

	$json = json_decode($fileContent);
	
	// can go to entity class
	$name = "name";
	$age = "age";
	$staffs = "staffs";

	echo "name: ";
	echo $json -> $name;

	echo " | age: ";
	echo $json -> $age;

	echo " | staffs: ";
	print_r($json -> $staffs);
?>