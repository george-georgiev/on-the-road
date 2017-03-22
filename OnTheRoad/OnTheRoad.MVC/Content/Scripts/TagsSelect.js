(() => {
    var addedTags = [];

    var tag = $('#tags');
    tag.autocomplete({
        source: '/tags/search'
    });

    tag.keypress(function (e) {
        if (e.keyCode == 13) {
            $('#tag-submit').click();
            e.preventDefault();
            return false;
        }
    });

    $('#tag-submit').click(() => {
        var tagName = tag.val();
        var selectedTags = $('#selected-tags');
        if (tagName && $.trim(tagName).length > 0 && addedTags.indexOf(tagName) < 0) {
            selectedTags.append(`<div class='tags-select-item'>${tagName}</div>`);
            selectedTags.append(`<input type='hidden' name='TagNames' value='${tagName}' />`);
            addedTags.push(tagName)
        }

        tag.val('');
    })
})();