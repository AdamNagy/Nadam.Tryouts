<!doctype html>
<html>
<head>
	<style>
		.thumbnail {
			padding: 5px;
		}
	</style>
</head>
<body>
	<h1>Hello from PHP templating</h1>
	<p>This is outside php</p>
	<?php

		Echo "<p>This one is inside php</p>";
		function endsWith($base, $end)
		{
			$length = strlen($end);
			if ($length == 0) 
			{
				return true;
			}
		
			return (substr($base, -$length) === $end);
		}

		$thumbsFolderName = "./thumbnails";
		$thumbs = scandir($thumbsFolderName);

		foreach($thumbs as $thumb)
		{
			if( endsWith($thumb, ".jpg") )
			{
				Echo "
					<div class='thumbnail'>
						<img src='./thumbnails/$thumb'>
					</img>";
			}
		}
	?>

</body>
</html>