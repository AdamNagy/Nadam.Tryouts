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
		include 'thumbnailTemplate.php';
		
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
				Echo ThumbnailTemplate($thumb);
			}
		}
	?>

	<button onclick="func1()">Click me</button>
	<button onclick="func2()">Get JSON</button>

	<script>
	function func1() {
		var xhttp = new XMLHttpRequest();
		xhttp.onreadystatechange = function() {
			if (this.readyState == 4 && this.status == 200) {
				document.nadam = this.responseText;
			}
		};
		xhttp.open("GET", "./file?prop1:wer", true);
		xhttp.send();
	}

	function func2() {
		var xhttp = new XMLHttpRequest();
		xhttp.onreadystatechange = function() {
			if (this.readyState == 4 && this.status == 200) {
				document.body.append(this.responseText);
				document.json = JSON.parse(this.responseText);
			}
		};
		xhttp.open("GET", "./json", true);
		xhttp.send();
	}
	</script>
</body>
</html>