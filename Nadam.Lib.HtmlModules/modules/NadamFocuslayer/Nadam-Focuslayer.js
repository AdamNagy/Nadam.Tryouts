"use strict";
// public:
/// <summary>
/// Focus layer (and Loading layer) is html div element act as a layer that can be displayed and hide, so
/// controls can be make more visible, or one honourable element can be placed into focus (and everithing els unfocus)
/// </summary>
function CreateFocuslayer() {
    // private:
    var $focuslayer = {};
    var $loadinglayer = {};
    var $blurBgLayer = {};

    var clickLinsteners = new Array();

    var showFocuslayer = function () {
        $focuslayer.show('fade', 400);
        $('.blurable').each(function () {
            $(this).addClass('blur');
        });
        $blurBgLayer.show('fade', 400);
    };
        
    var hideFocuslayer = function () {
        $focuslayer.hide('fade', 400);
        $('.blurable').each(function () {
            $(this).removeClass('blur');
        });
        $blurBgLayer.hide('fade', 400);        
    }

    var showLoadinglayer = function () {
        $loadinglayer.show('fade', 400);
        $('.blurable').each(function () {
            $(this).addClass('blur');
        });
        $blurBgLayer.show('fade', 400);
    }

    var hideLoadinglayer = function () {
        $loadinglayer.hide('fade', 400);
        $('.blurable').each(function () {
            $(this).removeClass('blur');
        });
        $blurBgLayer.hide('fade', 400);
    }

    var fireClickEvent = function () {
        for (var i = 0; i < clickLinsteners.length; ++i) {
            clickLinsteners[i]();
        }
    }

    // public:
    /// <summary>
    /// Make the Focus layer visible
    /// </summary>
    this.ShowFocuslayer = showFocuslayer;

    /// <summary>
    /// Make the Focus layer invisible
    /// </summary>
    this.HideFocuslayer = hideFocuslayer;

    /// <summary>
    /// Make the Loading layer visible
    /// </summary>
    this.ShowLoadinglayer = showLoadinglayer;

    /// <summary>
    /// Make the Loading layer invisible
    /// </summary>
    this.HideLoadinglayer = hideLoadinglayer;

    /// <summary>
    /// Click event implementaion (for Focus layer only)
    /// </summary>
    this.Click = function (func) {
        clickLinsteners.push(func);
    }

    /// <summary>
    /// Contrsuctor
    /// </summary>
    var contrsuctor = function () {
        $focuslayer = $('<div class="layerable" id="focus-layer"></div>');
        var $loadingLayerRow = $('<div class="loading-layer"><img class="gif" src="gifs/HV8RABd.gif" /></div>');
        $loadinglayer = $('<div class="layerable" id="loading-layer"></div>');
        $blurBgLayer = $('<div class="blured-bg-layer blur" id="blured-bc"></div>');
        $loadinglayer.append($loadingLayerRow);

        $focuslayer.click(function () {
            hideFocuslayer();
            fireClickEvent();
        });
        $loadinglayer.click(hideLoadinglayer);

        $('body').append($focuslayer);
        $('body').append($loadinglayer);
        //$('body').append($blurBgLayer);
    }();
}
