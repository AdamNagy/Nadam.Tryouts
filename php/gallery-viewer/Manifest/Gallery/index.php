<?php

require "../ManifestController.php";

$controller = new ManifestController("gallery");
echo json_encode($controller -> GetGallery());
?>