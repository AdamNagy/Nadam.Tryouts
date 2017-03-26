"Use strict";
// <nadam-checkboxradio>
function NadamCheckboxRadio(cbgroup) {
    var $group = {};
    var $rbuttons = {};
    var allowMultiple = false;
    var selectionChangeLinsteners = new Array();
    this.Selected = "";


    var uncheckAll = function () {
        $rbuttons.each(function () {
            $(this).removeClass('nadam-radiobutton-selected');
        })
        this.Selected = "";
    }

    var fireSelectionChange = function () {
        for (var i = 0; i < selectionChangeLinsteners.length; ++i) {
            selectionChangeLinsteners[i](this.Selected);
        }
    }

    this.SelectionChange = function (func) {
        selectionChangeLinsteners.push(func);
    }

    var checkSlected = function ($selected) {
        $selected.addClass('nadam-radiobutton-selected');
        this.Selected = $selected.data('value');
        fireSelectionChange();
    }

    var ctor = function InitRadios(id) {
        $group = $('#' + id);
        $rbuttons = $group.find('.nadam-radiobutton');

        $rbuttons.each(function () {
            $(this).click(function () {
                uncheckAll();
                checkSlected($(this));
            });
        });
    }(cbgroup);
}
// </nadam-checkboxradio>
/********************************************************************************************************************************/
// <nadam-dropdown>
function NadamDropdownWidget(widgetId, items) {
    // <private_variables>
    var $widget = $('#' + widgetId);
    var $dropdownMenu = $('<div id="' + widgetId + '-dropdown-menu" class="nadam-dropdown-menu"></div>');
    var $dropdownSelection = $('<div class="dropdow-selection"></div>');
    var $dropdownSelectionImg = $('<img id="' + widgetId + '-selection-img" />');

    var $focusLayer = $('<div id="' + widgetId + '-focus-layer" class="focus-layer"></div>');
    var selectionChangedListeners = new Array();
    // <private_variables>

    // <private_functions>
    /// <summary>
    /// 
    /// </summary>
    var showOptions = function () {
        var x = $(this).offset().left + 20;
        var y = $(this).offset().top + $(this).height() + 20;

        $dropdownMenu.css('top', y + 50);
        $dropdownMenu.css('left', x);

        $dropdownMenu.show().animate({ 'top': y, 'opacity': 1 }, 250);
        $focusLayer.show();
    };


    var hideOptions = function () {
        $focusLayer.hide();
        $dropdownMenu.hide();
        $dropdownMenu.css('opacity', 0);
    }


    var changeSelection = function () {
        $dropdownSelectionImg.attr('src', $(this).attr('src'));
        $dropdownSelectionImg.data('selection', $(this).data('selection-id'));

        for (var j = 0; j < selectionChangedListeners.length; j++) {
            selectionChangedListeners[j]($(this).attr('src'), $(this).data('selection-id'));
        }
        hideOptions();
    }


    var generateOptions = function (items) {
        var $img = $('<img class="option-image" data-selection-id=""/>');

        if (items == null) {
            $widget.find($('.nadam-dropdown-data')).find("img").each(function () {
                var i = 0;
                $(this).data('selection-id', i);
                $(this).addClass('option-image');
                $(this).attr('id', widgetId + '-' + i);
                $(this).click(changeSelection);
                $dropdownMenu.append($(this));
                ++i;
            });
        } else {
            for (var i = 0; i < items.length; ++i) {
                var $imgOption = $img.clone();
                $imgOption.attr('src', items[i].Value);
                $imgOption.data('selection-id', items[i].Id);
                $imgOption.attr('id', widgetId + '-' + items[i].Id);
                $imgOption.click(changeSelection);
                $dropdownMenu.append($imgOption);
            }
        }
    };
    // </private_functions>

    // <global_variables>
    this.Selected = function () {
        var val = $('#' + widgetId + '-selection-img').data('selection');
        return val;
    }
    // <global_variables>

    // <events>
    this.SelectionChanged = function (func) {
        selectionChangedListeners.push(func);
    }
    // <events>

    // Ctor
    var constructor = function (items) {
        var $dropdownButton = $('<div class="nadam-dropdown-button" id="' + widgetId + '-images" data-selected="0">Select image</div>');
        var $dropdownButtonIcon = $('<span class="glyphicon glyphicon-chevron-down gi-2x"> </span>');

        $dropdownButton.append($dropdownButtonIcon);
        $dropdownSelection.append($dropdownSelectionImg);
        $widget.append($dropdownSelection);

        $widget.append($dropdownButton);
        $dropdownButton.click(showOptions);

        $('body').append($dropdownMenu);
        $('body').append($focusLayer);

        $('#' + widgetId + '-focus-layer').click(hideOptions);

        generateOptions(items);
    }(items);
}

function DropdownItem(id, value) {
    this.Id = id;
    this.Value = value;
}
// </nadam-dropdown>
/********************************************************************************************************************************/
// <nadam-focuslayer>
function CreateFocuslayer() {
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

    this.ShowFocuslayer = showFocuslayer;
    this.HideFocuslayer = hideFocuslayer;

    this.ShowLoadinglayer = showLoadinglayer;
    this.HideLoadinglayer = hideLoadinglayer;

    this.Click = function (func) {
        clickLinsteners.push(func);
    }

    var contrsuctor = function (base) {
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
    }(this);
}
// <nadam-focuslayer>
/********************************************************************************************************************************/
// <nadam-menus>
/* Sticky horisontal menu */
function ConvertToStickyMenu(id, offsetid) {
    if (offsetid == null)
        offsetid = $('header').height();

    $('#' + id).affix({
        offset: {
            top: offsetid
        }
    });
}

/* Hideable menu, with focus layer, full height */
function HiddenMenu(base, openClass) {
    var $menu = {};
    var $menuOpener = {};
    var onFocus = false;
    var openLinsteners = new Array();
    var closeLinsteners = new Array();

    var open = function () {
        onFocus = true;
        $menu.css('right', '0%');
        $menu.addClass(openClass);
    }

    var close = function () {
        onFocus = false;
        $menu.css('right', '-31%');
        $menu.removeClass(openClass);
    }

    this.Open = open;
    this.Close = close;

    var fireOpenEvent = function () {
        for (var i = 0; i < openLinsteners.length; ++i) {
            openLinsteners[i]();
        }
    }

    this.OnOpen = function (func) {
        openLinsteners.push(func);
    }

    var fireCloseEvent = function () {
        for (var i = 0; i < closeLinsteners.length; ++i) {
            closeLinsteners[i]();
        }
    }

    this.OnClose = function (func) {
        closeLinsteners.push(func);
    }

    var ctor = function (id) {
        $menu = $('#' + id);
        $menu.addClass('hidden-menu');
        var $menuOpener = $('<div class="container text-left"><span class="glyphicon glyphicon-menu-hamburger" id="' + id + '-toggle"></span></div>')
        $menu.prepend($menuOpener);

        $menuOpener.click(function () {
            if (!onFocus) {
                open();
                fireOpenEvent();
            }
            else {
                close();
                fireCloseEvent();
            }
        });

        //$menu.click(function () {
        //    if (!onFocus) {
        //        open();
        //        fireOpenEvent();
        //    }
        //    else {
        //        close();
        //        fireCloseEvent();
        //    }
        //});
    }(base)
}

/* Side menu on middle, half hidden */
function ConvertToSideMenu(id) {
    var $menu = $('#' + id);
    $menu.addClass('side-menu');
    var menuOnHover = false;
    var menuOnFocus = false;

    $('#' + id + ' input, #' + id + ' select').on('focus', function () {
        menuOnFocus = true;
        $menu.css('left', '0px');
        $menu.css('opacity', '1');
    });

    $('#' + id + ' input, #' + id + ' select').on('blur', function () {
        menuOnFocus = false;
        if (!menuOnHover) {
            $menu.css('left', '-250px');
            $menu.css('opacity', '0.5');
        }
    });

    $('#' + id).on('mouseenter', function () {
        menuOnHover = true;
        $menu.css('left', '0px');
        $menu.css('opacity', '1');
    });

    $('#' + id).on('mouseleave', function () {
        menuOnHover = false;
        if (!menuOnFocus) {
            $menu.css('left', '-250px');
            $menu.css('opacity', '0.5');
        }
    });
}
// </nadam-menus>
/********************************************************************************************************************************/
// <nadam-slider-app>
function SliderManager() {
    // private:
    var currPage = 0;
    var sumPages = 0;
    var pagesLoadStatus = {};
    var pageSelectionListeners = {};

    var slide = function (page) {
        if (page > sumPages) {
            page = 1;
        }
        else if (page < 1) {
            page = sumPages;
        }

        try {
            if (pagesLoadStatus[page] == false) {
                pageSelectionListeners[page]();
                pagesLoadStatus[page] = true;
            }
        }
        catch (err) {
            console.log("page " + page + " does not have any initializer function");
        }

        $('#slide-' + currPage).hide();
        $('#manu-button-' + currPage).removeClass("menu-button-current");

        currPage = page;
        $('#slide-' + currPage).show('fade', 500);
        $('#manu-button-' + currPage).addClass('menu-button-current');
    }

    // public:
    this.SlideLoadListener = function (pagenum, func) {
        pageSelectionListeners[pagenum] = func;
    }

    this.ToPage = function (tpPage) {
        slide(tpPage);
    }

    this.NextPage = function () {
        slide(currPage + 1);
    }

    this.PrevPage = function () {
        slide(currPage - 1);
    }

    this.start = function () {
        slide(1);
    }

    // Constructor
    var ctor = function () {
        var numOfPages = $('.slide').length;
        sumPages = numOfPages;
        for (var i = 1; i <= sumPages; ++i) {
            pagesLoadStatus[i] = false;
        }
    }();
}
// </nadam-slider-app>
/********************************************************************************************************************************/
// <nadam-spiter>
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
// <nadam-spiter>