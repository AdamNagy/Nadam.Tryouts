<?php
	
	function filter($item) {
		$filter["prop2"] > 10;
	}

	$file = fopen("./json-db/test.json", "r") or die("Unable to open file!");
	$fileContent = fread($file, filesize("./json-db/test.json"));
	fclose($file);

	$json = json_decode($fileContent);
	
	// can go to entity class
	$name = "name";
	$age = "age";
	$staffs = "staffs";

	$businessModel;
	$businessModel[$name] = $json -> $name;
	$businessModel[$staffs] = array_filter($json -> $staffs, filter);

	echo "name: ";
	echo $businessModel[$name];

	echo " | age: ";
	echo $json -> $age;

	echo " | staffs: ";
	print_r($businessModel[$staffs]);
?>