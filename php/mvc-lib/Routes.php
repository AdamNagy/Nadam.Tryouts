<?php 

	$url = explode('/', $_GET['url']);
	print_r($url);

	Route::set('about-us', function() {
	
		$controller = new AboutUs();

		if( isset(url[2]) ) {
			$controller->{$url[1]}($url[2]);
		} else if (isset(url[1])) {
			$controller->{$url[1]};
		}
	});

	Route::set('contact-us', function() {
		echo 'contact us';
	});

?>