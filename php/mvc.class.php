<?php
	class Mvc {
		public function ParseQueryString($_queryString) {

			$queryStrings = explode("&", $_queryString);

			$keyValues = array();
			foreach($queryStrings as $qs) {
				$fragment = explode("=", $qs);
				$keyValues[$fragment[0]] = $fragment[1];
			}

			return $keyValues;
		}

		public function GetActionName($_route, $_controllerName) {

			$routeSegments = explode("/", $_route);
			$idx = 0;
			foreach($routeSegments as $segment) {
				if( $segment == $_controllerName ) {
					break;
				}
				++$idx;
			}
			++$idx;
			return explode("?", $routeSegments[$idx])[0];
		}
	}
?>