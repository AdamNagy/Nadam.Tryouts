"Use strict"

/* Sticky horisontal menu */
function ConvertToStickyMenu(id, offsetid) {
    if (offsetid == null)
        offsetid = 'header';

    $('#' + id).affix({
        offset: {
            top: $(offsetid).height()
        }
    });
}

/* Hideable menu, with focus layer, full height */
function HiddenMenu(base, openClass) {
    // private:
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

    var fireOpenEvent = function () {
        for (var i = 0; i < openLinsteners.length; ++i) {
            openLinsteners[i]();
        }
    }

    var fireCloseEvent = function () {
        for (var i = 0; i < closeLinsteners.length; ++i) {
            closeLinsteners[i]();
        }
    }

    // public:
    /// <summary>
    /// Opens the menu (flow in, so it will be visible)
    /// </summary>
    this.Open = open;
    /// <summary>
    /// Close the menu (flow out, so it will be invisible)
    /// </summary>
    this.Close = close;

    /// <summary>
    /// Event that is fired when menu is opend
    /// </summary>
    this.OnOpen = function (func) {
        openLinsteners.push(func);
    }

    /// <summary>
    /// Event that is fired when menu is closed
    /// </summary>
    this.OnClose = function (func) {
        closeLinsteners.push(func);
    }

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="id">id of the html div element that will be converted to hidden menu</param>
    var ctor = function (id) {
        $menu = $('#' + id);
        $menu.addClass('hidden-menu');
        var $menuOpener = $('<div class="container text-left"><span class="glyphicon glyphicon-plus" id="' + id + '-toggle"></span></div>')
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