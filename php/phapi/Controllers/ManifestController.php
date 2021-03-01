<?php

class ManifestController extends Controller {
	
	private $repository;
	private $defaltPagesize = 10;

	function __construct($_queryParams, $_pathParams) {

		parent::__construct($_queryParams, $_pathParams);
		$this -> repository = new ManifestRepository("./App_Data");

	}

	public function GetGallery() {

		$responseModel = new GalleryResponse();
		$_title = "";

		// query params need to contain "title"
		if( array_key_exists("title", $this -> queryParams)) {
			$_title = $this -> queryParams["title"];
		} else if( isset($this -> pathParams[0])) {
			$_title = $this -> pathParams[0];
		} else {
			$responseModel -> isSuccess = false;
			$responseModel -> message = "ManifestController.GetGallery -> No title provided in query string 'title' key nor in the path";
			return $responseModel;
		}


		// first try with type gallery
		$fullFileTitle = $_title; //.".gallery.json";
		$manifest = $this -> repository -> ReadManifestFor($fullFileTitle);

		// then with type stored-gallery
		if( is_null($manifest) || $manifest == "" ) {
			$fullFileTitle = $_title.".stored-gallery.json";
			$manifest = $this -> repository -> ReadManifestFor($fullFileTitle);
		}

		// if nothing it does not exist
		if( is_null($manifest) || $manifest == "" ) {
			$responseModel -> isSuccess = false;
			$responseModel -> message = "No such gallery with title: ".$_title;
			return $responseModel;
		}

		// if exist, return it
		$responseModel -> isSuccess = true;
		$responseModel -> galleryFile = $manifest;
		return $responseModel;
	}

	public function GetThumbnails() {

		$allFileNames;
		$page = 1;
		if( array_key_exists("title", $this -> queryParams) ) {
			$allFileNames = $this -> repository -> WhereTitleContains($this -> queryParams["title"]);			
		} else if( isset($this -> pathParams[0]) ) {
			if( is_numeric($this -> pathParams[0]) ) {
				$page = $this -> pathParams[0];
				$allFileNames = $this -> repository -> SelectAll();
			} else {
				$allFileNames = $this -> repository -> WhereTitleContains($this -> pathParams[0]);
			}
		} else {
			$allFileNames = $this -> repository -> SelectAll();
		}

		$page = array_key_exists("page", $this -> queryParams) ? (int)$this -> queryParams["page"] : $page;
		$pageSize = array_key_exists("pagesize", $this -> queryParams) ? $this -> queryParams["pagesize"] : $this -> defaltPagesize;

		$numOfPages = ceil(count($allFileNames) / $pageSize);

		$filteredByPage = $this -> FilterByPage($allFileNames, $page, $pageSize);

		$responseModel = new ThumbnailsResponse();
		$responseModel -> isSuccess = true;
		$responseModel -> currentPage = $page;
		$responseModel -> pages = $numOfPages;

		if(count($filteredByPage) > 0) {
			$responseModel -> thumbnailFiles = $this -> repository -> ReadThumbnailsFor($filteredByPage);
		} else {
			$responseModel -> thumbnailFiles = "";
		}

		return $responseModel;
	}

	private function FilterByPage($allFileNames, $page, $pageSize) {
		$numOfPages = ceil(count($allFileNames)) / $pageSize;

		--$page;
		$startIndex = $page * ($pageSize) > count($allFileNames) ?
			(--$page) * ($pageSize) :
			$page * ($pageSize);

		return array_slice($allFileNames, $startIndex, $pageSize);
	}

	private function GetQueryOrPathParam($key, $index) {
		if( array_key_exists($key, $this -> queryParams) ) {
			return $this -> queryParams[$key];			
		} else if( isset($this -> pathParams[$index]) ) {
			return $this -> pathParams[$index];
		} 

		return "";		
	}
}
?>