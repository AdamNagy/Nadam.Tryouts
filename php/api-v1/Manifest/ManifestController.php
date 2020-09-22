<?php
// /manifestframework/gallerythumb
require $_SERVER['DOCUMENT_ROOT']."/Restapi"."/Core/Restapi/ApiController.php";
require $_SERVER['DOCUMENT_ROOT']."/Restapi"."/Core/ManifestFramework/ManifestRepository.php";
require $_SERVER['DOCUMENT_ROOT']."/Restapi"."/Core/Restapi/ApiResponseModel.php";

class ManifestController extends ApiController {
	
	private $repository;
	private $pagesize = 10;

	function __construct($_action) {

		parent::__construct();
		$this -> action = $_action;
		$this -> repository = new ManifestRepository($_SERVER['DOCUMENT_ROOT']."/Restapi/App_Data");
	}

	public function GetGallery() {

		$responseModel = new ApiResponseModel();

		// query params need to contain "title"
		if( !array_key_exists("title", $this -> queryParams) ) {
			$responseModel -> isSuccess = false;
			$responseModel -> message = "No title provided";
			return $responseModel;
		}

		$_title = $this -> queryParams["title"];

		// first try with type gallery
		$fullFileTitle = $_title.".gallery.json";
		$manifest = $this -> repository -> ReadManifestFor($fullFileTitle);
		$responseModel -> type = "gallery";

		// then with type stored-gallery
		if( is_null($manifest) || $manifest == "" ) {
			$fullFileTitle = $_title.".stored-gallery.json";
			$manifest = $this -> repository -> ReadManifestFor($fullFileTitle);
			$responseModel -> type = "stored-gallery";
		}

		// if nothing it does not exist
		if( is_null($manifest) || $manifest == "" ) {
			$responseModel -> isSuccess = false;
			$responseModel -> message = "No such gallery with title: ".$_title;
			return $responseModel;
		}

		// if exist, return it
		$responseModel -> isSuccess = true;
		$responseModel -> content = $manifest;
		return $responseModel;
	}

	public function GetThumbnails() {

		$allFileNames;
		if( array_key_exists("title", $this -> queryParams) ) {
			$allFileNames = $this -> repository -> WhereTitleContains($this -> queryParams["title"]);
		} else {
			$allFileNames = $this -> repository -> SelectAll();
		}

		$page = $this -> queryParams["page"] ?? 1;
		$pageSize = $this -> queryParams["pagesize"] ?? $this -> pagesize;
		$numOfPages = ceil(count($allFileNames)) / $pageSize;

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

/*
        public $pages;
        public $currentPage;
        public $thumbnailFiles;
*/

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
}
?>