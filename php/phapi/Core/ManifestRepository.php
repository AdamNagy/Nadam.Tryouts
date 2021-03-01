<?php

class ManifestRepository {

	public $root = "";
	public $postfix = "";
	public $fileNames = [];

	function __construct($_root, $_postfix = ".json") {

		$this -> root = $_root;
		$this -> postfix = $_postfix;

		$allFileNames = scandir($this -> root);
		$this -> fileNames = array_values(array_filter($allFileNames, function($item) { return StringExtensions::EndsWith($item, $this -> postfix); }));
	}

	public function SelectAll() {
		return $this -> fileNames;
	}

	public function WhereTitleContains($searchTerm) {
		$filtered = array();
		for($i = 0; $i < count($this -> fileNames); $i++) {
			if( StringExtensions::Contains(strtolower($this -> fileNames[$i]), strtolower($searchTerm)) ) {
				array_push($filtered, $this -> fileNames[$i]);
			}
		}	
		return $filtered;
	}

	public function ReadManifestFor($fileTitle) {
		$_root = $this -> root;

		if(file_exists("$_root/$fileTitle")) {
			$file = fopen("$_root/$fileTitle", "r");
		} else {
			return "";
		}
		
		$fileContent = fread($file, filesize("$_root/$fileTitle"));
		fclose($file);

		$galleryFile = new GalleryFileModel();
		$galleryFile -> fileName = $fileTitle;
		$galleryFile -> type = explode(".", $fileTitle)[1];
		$galleryFile -> content = $fileContent;

		return $galleryFile;
	}

	public function ReadThumbnailsFor($fileTitles) {
		$filtered = array();
		for($i = 0; $i < count($fileTitles); $i++) {

			$galleryFile = new GalleryFileModel();
			$galleryFile -> fileName = $fileTitles[$i];
			$galleryFile -> type = explode(".", $fileTitles[$i])[1];

			switch($galleryFile -> type) {
				case "gallery": 
					$galleryFile -> content = $this -> ReadThumbnailForGallery($this-> root, $fileTitles[$i], 5);
					break;
				case "stored-gallery":
					$galleryFile -> content = $this -> ReadThumbnailForStoredGallery($this-> root, $fileTitles[$i], 5);
					break;
			}
			

			array_push($filtered, $galleryFile);
		}

		return $filtered;
	}

	private function ReadThumbnailForGallery($folder, $fileTitle, $numOfImages) {

		$file = fopen("$folder/$fileTitle", "r") or die("Unable to open file!");

		$readMore = true;
		$fileContent = "";
		while($readMore || $segment == false) {
			$segment = fread($file, 50);
			$fileContent = $fileContent.$segment;
			$readMore = substr_count($fileContent, "[") < 1;	
		}

		$readMore = true;
		while($readMore || $segment == false) {
			$segment = fread($file, 50);
			$fileContent = $fileContent.$segment;
			$readMore = substr_count($fileContent, "}") < ($numOfImages);	
		}

		fclose($file);

		$lastCommaIdx = strripos($fileContent, "}");
		$diff = strlen($fileContent) - $lastCommaIdx;
		$jsonString = substr($fileContent, 0, (-1) * $diff);

		//$galleryFile -> content = $jsonString."}]}";
		return $jsonString."}]}";
		
	}

	private function ReadThumbnailForStoredGallery($folder, $fileTitle, $numOfImages) {
		$file = fopen("$folder/$fileTitle", "r") or die("Unable to open file!");

		$readMore = true;
		$fileContent = "";
		while($readMore || $segment == false) {
			$segment = fread($file, 50);
			$fileContent = $fileContent.$segment;
			$readMore = substr_count($fileContent, "[") < 1;	
		}

		$readMore = true;
		while($readMore || $segment == false) {
			$segment = fread($file, 50);
			$fileContent = $fileContent.$segment;
			$readMore = substr_count($fileContent, ",") < ($numOfImages);	
		}

		fclose($file);

		$lastCommaIdx = strripos($fileContent, ",");
		$diff = strlen($fileContent) - $lastCommaIdx;
		$jsonString = substr($fileContent, 0, (-1) * $diff);

		return $jsonString."]}";
	}
}

?>