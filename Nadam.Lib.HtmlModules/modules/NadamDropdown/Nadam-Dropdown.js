"use strict";
function NadamDropdownWidget(widgetId, items) {
    // private:
    var $widget = {};
    var $dropdownMenu = {};
    var $dropdownSelection = {};
    var $dropdownSelectionImg = {};

    var selectionChangedListeners = new Array();

    var showOptions = function () {
        var x = $(this).offset().left + 20;
        var y = $(this).offset().top + $(this).height() + 20;

        $dropdownMenu.css('top', y + 50);
        $dropdownMenu.css('left', x);

        $dropdownMenu.show().animate({ 'top': y, 'opacity': 1 }, 250);
    };

    var hideOptions = function () {
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

    // public:
    /// <summary>
    /// Get the currently selected item
    /// </summary>
    this.Selected = function () {
        var val = $('#' + widgetId + '-selection-img').data('selection');
        return val;
    }

    /// <summary>
    /// Event that is fired awhen the selected item changes
    /// </summary>
    this.SelectionChanged = function (func) {
        selectionChangedListeners.push(func);
    }

    // constructor:
    var constructor = function (items, widgetId) {
        $widget = $('#' + widgetId);
        $dropdownMenu = $('<div id="' + widgetId + '-dropdown-menu" class="nadam-dropdown-menu"></div>');
        $dropdownSelection = $('<div class="dropdow-selection"></div>');
        $dropdownSelectionImg = $('<img id="' + widgetId + '-selection-img" />');

        var $dropdownButton = $('<div class="nadam-dropdown-button" id="' + widgetId + '-images" data-selected="0">Select image</div>');
        var $dropdownButtonIcon = $('<span class="glyphicon glyphicon-chevron-down gi-2x"> </span>');

        $dropdownButton.append($dropdownButtonIcon);
        $dropdownSelection.append($dropdownSelectionImg);
        $widget.append($dropdownSelection);

        $widget.append($dropdownButton);
        $dropdownButton.click(showOptions);

        $('body').append($dropdownMenu);
        $('#' + widgetId + '-focus-layer').click(hideOptions);

        generateOptions(items);
    }(items, widgetId);
}

function DropdownItem(id, value) {
    this.Id = id;
    this.Value = value;
}