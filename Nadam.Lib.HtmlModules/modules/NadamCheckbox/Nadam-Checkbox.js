"Use strict";
function NadamCheckboxRadio(cbgroup) {
    // private:
    var $group = {};
    var $rbuttons = {};
    var allowMultiple = false;
    var selectionChangeLinsteners = new Array();    

    var uncheckAll = function() {
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

    var checkSlected = function ($selected) {
        $selected.addClass('nadam-radiobutton-selected');
        this.Selected = $selected.data('value');
        fireSelectionChange();
    }

    // public:
    /// <summary>
    /// Gets the selected item
    /// </summary>
    this.Selected = "";

    /// <summary>
    /// Event that is fired when selection changes
    /// </summary>
    this.SelectionChange = function(func){
        selectionChangeLinsteners.push(func);
    }

    // constructor
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