<?php
	function ThumbnailTemplate($imgSrc)
	{
		return "
			<div class='thumbnail'>
				<img src='./thumbnails/$imgSrc'>
			</img></div>";
	}
?>