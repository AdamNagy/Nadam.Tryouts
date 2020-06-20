<?php 
	include "./core/repository.php";
	$repository = new Repository("./App_Data")
?>

<!doctype html>
<html lang="en">
<head>
	<!-- Required meta tags -->
	<meta charset="utf-8">
	<meta name="viewport" content="width=device-width, initial-scale=1, shrink-to-fit=no">

	<!-- Bootstrap CSS -->
	<link rel="stylesheet" href="https://stackpath.bootstrapcdn.com/bootstrap/4.1.3/css/bootstrap.min.css" integrity="sha384-MCw98/SFnGE8fJT3GXwEOngsV7Zt27NXFoaoApmYm81iuXoPkFOJwJ8ERdknLPMO" crossorigin="anonymous">


	<title>PHP json db</title>

	<style>

	</style>
</head>
<body>

	<section class="container">
		<h1 class="text-center h1">PHP Json Dbndex</h1>
		<h2 class="h2">Here is some docs for it</h2>
		<ul>
		<?php 
			$files = $repository -> getFiles();
			for($i = 0; $i < count($repository -> getFiles()); $i++) {
				$file = $files[$i];
				echo "<li>$file";
			}		
		?>
		</ul>
	</section>



    <!-- Base JavaScript -->
    <!-- jQuery first, then Popper.js, then Bootstrap JS -->
    <script src="./lib/jquery/jquery-v3.4.0.js"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/popper.js/1.14.3/umd/popper.min.js" integrity="sha384-ZMP7rVo3mIykV+2+9J3UJ46jBk0WLaUAdn689aCwoqbBJiSnjAK/l8WvCWPIPm49" crossorigin="anonymous"></script>
    <script src="https://stackpath.bootstrapcdn.com/bootstrap/4.1.3/js/bootstrap.min.js" integrity="sha384-ChfqqxuZUCnJSK3+MXmPNIyE6ZbWh2IMqE241rYiqJxyMiZ6OW/JmZQ5stwEULTy" crossorigin="anonymous"></script>
  
	<script>
	</script>
</body>
</html>