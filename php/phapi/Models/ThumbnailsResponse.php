<?php
	class ThumbnailsResponse {
		public $isSuccess;
        public $message;

        public $pages;
        public $currentPage;
        public $thumbnailFiles;
	}

	class GalleryResponse {
		public $isSuccess;
		public $message;
		// stringifyed 'GalleryFileModel' object
		public $galleryFile;
	}
?>