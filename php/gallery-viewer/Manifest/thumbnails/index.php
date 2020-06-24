<?php
require "../ManifestController.php";
$controller = new ManifestController("thumbnails");

echo json_encode($controller -> GetThumbnails());
?>