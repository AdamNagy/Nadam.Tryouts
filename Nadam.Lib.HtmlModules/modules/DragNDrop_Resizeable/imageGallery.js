$(function () {

    // There's the gallery and the trash
    var $gallery = $("#gallery");

    // Let the images be draggable
    $("li", $gallery).draggable({
        cancel: "a.ui-icon", // clicking an icon won't initiate dragging
        revert: "invalid", // when not dropped, the item will revert back to its initial position
        containment: "document",
        helper: "clone",
        cursor: "move"
    });

    // Let the sequence areas be droppable, accepting the gallery items
    $(".sequence-droparea").each(function () {
        $(this).droppable({
            accept: "#gallery > li",
            classes: {
                "ui-droppable-active": "seqeuence-dropactive"
            },
            drop: function (event, ui) {
                addToSequence(ui.draggable, $(this));
            }
        });
    });

    // Let the gallery be droppable as well, accepting items from the trash
    $gallery.droppable({
        accept: ".sequence-droparea li",
        classes: {
            "ui-droppable-active": "custom-state-active"
        },
        drop: function (event, ui) {
            removeFromSequence(ui.draggable);
        }
    });

    // Adding image to sequence
    function addToSequence($item, $sequence) {
        $item.fadeOut(300, function () {
            var $list = $("ul", $sequence).length ?
                $("ul", $sequence) :
                $("<ul class='gallery ui-helper-reset'/>").appendTo($sequence);

            $item.find("a.ui-icon-trash").remove();
            //$item.appendTo($list).fadeIn(function () {});
            $item.appendTo($list).fadeIn(300);
        });
    }

    // Remove image from sequence
    function removeFromSequence($item) {
        $item.fadeOut(function () {
            $item
                .appendTo($gallery)
                .fadeIn(200);
        });
    }

    $("#normal").colResizable({
        liveDrag: true,
        gripInnerHtml: "<div class='grip'></div>",
        draggingClass: "dragging",
        resizeMode: 'fit',
        minWidth: 400
    });
});
