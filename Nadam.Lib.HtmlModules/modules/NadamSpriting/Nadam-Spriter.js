"User strict";

function SpriteImage(imgSrc, iconWidth, iconHeight, outputDicId) {
    var xOffset = 0,
        yOffset = 0;

    var $imgTemp = $('<div>');
    var $imgFrameTemp = $('<div>');
    var urlt = 'url("' + imgSrc + '")';
    $imgTemp.css('background', urlt);

    var id = 1;
    var $outputDiv = $('#' + outputDicId);

    for (var i = 1; i < 4; i++) {
        for (var j = 1; j < 4; j++) {
            var url = 'url("' + imgSrc + '")' + xOffset + ' ' + yOffset;
            console.log(url);
            var $img = $imgTemp.clone();
            var $imgFrame = $imgFrameTemp.clone();
            $imgFrame.addClass('frame');

            $img.attr('id', id);
            $img.css({
                'background-position-x': xOffset,
                'background-position-y': yOffset,
                'height': iconHeight,
                'width': iconWidth
            });

            $imgFrame.append($img);
            $outputDiv.append($imgFrame);

            xOffset += iconWidth;
            ++id;
        }
        xOffset = 0,
        yOffset += iconHeight;
    }

    console.log("done");
}