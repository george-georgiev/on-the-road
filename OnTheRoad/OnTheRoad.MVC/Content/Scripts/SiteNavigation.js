(() => {
    $('#search-box').keypress(function (e) {
        if (e.keyCode == 13)
            $('#search-btn').click();
    });

    $('#search-btn').click(() => {
        var searchBox = $('#search-box');
        var searchPattern = searchBox.val();
        console.log(searchPattern);
        if (searchPattern && $.trim(searchPattern).length > 0) {
            window.location.href = '/trips/' + searchPattern;
        }
        else {
            searchBox.val('');
        }
    })
})();