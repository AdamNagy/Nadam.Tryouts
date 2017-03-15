function NadamDropdownWidget(widgetId, items) {
    "use strict";
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