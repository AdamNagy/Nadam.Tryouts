var Children = {

    isImage: function(element) {
        if (element == null || element.firstElementChild == null) {
            return false;
        }
        return element.firstElementChild.tagName.toLocaleLowerCase() === HtmlTags.image;
    },

    isDiv: function(element) {
        if (element == null || element.firstElementChild == null) {
            return false;
        }
        return element.firstElementChild.tagName.toLocaleLowerCase() === HtmlTags.div;
    },

    isThumbnail: function(element) {
        if (element == null || element.firstElementChild == null) {
            return false;
        }

        var firstChild = element.firstElementChild;
        var secondLevelChild = firstChild.firstElementChild;

        if (firstChild == null || secondLevelChild == null) {
            return false;
        }

        return firstChild.tagName.toLocaleLowerCase() === HtmlTags.div &&
            secondLevelChild.tagName.toLocaleLowerCase() === HtmlTags.image;
    }
}