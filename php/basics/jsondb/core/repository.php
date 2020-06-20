<?php
class Repository {

	private $root = "";
	private $postfix = "";
	private $files = array();

	function __construct($tempRoot, $tempPostfix = ""){

		$this -> files = scandir($tempRoot);
	}

	public function getFiles() {
		return $this -> files;
	}
}
?>