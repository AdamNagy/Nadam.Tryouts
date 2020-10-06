<?php
	class GalleryFileModel {
		public $type;
        public $fileName;
        // content of the file or thumbnails of files as string
        public $content;
	}

	class ThumbnailsResponse {
		public $isSuccess;
        public $message;

        public $pages;
        public $currentPage;
        public $thumbnailFiles;
	}

	class ApiResponseModel {
		public $isSuccess;
		public $message;
		// stringifyed 'GalleryFileModel' object
		public $galleryFile;
	}
?>