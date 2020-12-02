<?php

class StoredGallery {
	public $route;
	public $body;
}

function GetContainerContent($containerName, $force = false) {

	$date = date("d-m");
	$file = 'wedding-'.$date.'.xml';
	$url = 'https://nadam.blob.core.windows.net/'.$containerName.'?restype=container&comp=list';
	
	$xmlString = "";
	if ( !file_exists($file) || $force) {
		$ch = curl_init(); 
		//Set the URL that you want to GET by using the CURLOPT_URL option.
		curl_setopt($ch, CURLOPT_URL, $url);
		//Set CURLOPT_RETURNTRANSFER so that the content is returned as a variable.
		curl_setopt($ch, CURLOPT_RETURNTRANSFER, true);
		//Set CURLOPT_FOLLOWLOCATION to true to follow redirects.
		curl_setopt($ch, CURLOPT_FOLLOWLOCATION, true);
		//Execute the request.
		$xmlString = curl_exec($ch);
		//Close the cURL handle.
		curl_close($ch);
	
		file_put_contents($file, $xmlString);
	} else {
		$xmlString = file_get_contents($file);
	}

	return $xmlString;
}

function ParseXMLToGallery($xmlString) {

	$container = new SimpleXMLElement($xmlString);
	$idx = 0;
	$blob = $container -> Blobs -> Blob[$idx];
	
	$ignoreFolderNames = ["thumbnails", "viewimages", "originals"];
	$ignoreFileExtension = ["db", "json", "txt", "csv", "config", "gallery"];
	
	$galleries = array();
	
	while( isset($blob) ) {
		$splitted = explode('/', $blob -> Name);
		$fileName = $splitted[count($splitted) - 1];
		$fileExtension = explode('.', $fileName)[1];
		
		if( in_array($fileExtension, $ignoreFileExtension) == 1) {
			$idx = $idx + 1;
			$blob = $container->Blobs->Blob[$idx];
			continue;
		}
		
		$route = "";
		$fileNameSegmentIdx = 0;
		$currentSegment = $splitted[$fileNameSegmentIdx];
		while( in_array($currentSegment, $ignoreFolderNames) != 1 && $currentSegment != $fileName && $fileNameSegmentIdx < 3 ) {
			$route .= $currentSegment.'/';
			$fileNameSegmentIdx = $fileNameSegmentIdx + 1;
			$currentSegment = $splitted[$fileNameSegmentIdx];
		}
		$route = rtrim($route, "/");
	
		if( !array_key_exists($route, $galleries) ) {
			$galleries[$route] = array();
		}
	
		array_unshift($galleries[$route], $fileName);
	
		$idx = $idx + 1;
		$blob = $container->Blobs->Blob[$idx];
	}

	return $galleries;
}

function UpdateStoredGalleryDb($galleryDict) {

	foreach ($galleryDict as $key => $value) {
		$galleryModel = new StoredGallery();
		// echo $key;
		$galleryModel -> route = $key;
		$galleryModel -> body = $value;
	
		file_put_contents(str_replace("/", "--", $key).".stored-gallery.json", json_encode($galleryModel));
	}
}

$xmlString = GetContainerContent("wedding");
$galleryDict = ParseXMLToGallery($xmlString);
UpdateStoredGalleryDb($galleryDict);

print_r($galleryDict);
?>