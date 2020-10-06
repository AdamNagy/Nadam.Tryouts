<?php
class ApiController {

	public $queryParams;
	public $action;

	function __construct(){

		$this -> queryParams = $this -> ParseQueryString($_SERVER['QUERY_STRING']);
	}

	private function ParseQueryString($_queryString) {

		$queryStrings = explode("&", $_queryString);

		$keyValues = array();

		if(is_null($_queryString) && count($queryStrings) > 0) {
			return $keyValues;
		}

		foreach($queryStrings as $qs) {
			$fragment = explode("=", $qs);

			
			if(count($fragment) < 2) {
				return $keyValues;
			}

			$keyValues[$fragment[0]] = $fragment[1];
		}

		return $keyValues;
	}
}
?>