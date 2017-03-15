"use strict";
function CreateFocuslayer() {
    var $focusLayer = $('<div id="focuslayer"></div>');

    var contrsuctor = function () {
        //alert("contrsuctor CreateFocuslayer");
        $focusLayer.click(function () {
            $(this).animate({ 'opacity': 0 }, 400, function () {
                $focusLayer.hide();
            });
            $('.blurable').each(function () {
                $(this).removeClass('blure-3px');
            });
        });

        $('body').append($focusLayer);
    }();

    this.ShowFocuslayer = function () {
        $focusLayer.show();
        $focusLayer.animate({ 'opacity': 1 }, 400);

        $('.blurable').each(function () {
            $(this).addClass('blure-3px');
        });
    }
}
