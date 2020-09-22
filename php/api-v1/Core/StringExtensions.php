<?php
	class StringExtensions {
		public static function StartsWith($haystack, $needle) {
            return substr_compare($haystack, $needle, 0, strlen($needle)) === 0;
		}
		
        public static  function EndsWith($haystack, $needle) {
            return substr_compare($haystack, $needle, -strlen($needle)) === 0;
        }

		public static function Contains($text, $search) {
			if (strpos($text, $search) !== false) {
				return true;
			}
			return false;

		}
	}
?>