(() => {
    var nextPage = $('#next-page');
    var prevPage = $('#prev-page');

    nextPage.click((e) => {
        return getTrips(e);
    });

    prevPage.click((e) => {
        return getTrips(e);
    });

    function getTrips(e) {
        $.get(
            $(e.target).attr('href'),
            null,
            (data) => {
                var parser = new DOMParser()
                var doc = parser.parseFromString(data, "text/html");
                var trips = doc.getElementById('trips');

                $(e.target).closest('.content-container').html(trips);

                $('#next-page').click((e) => {
                    return getTrips(e);
                });

                $('#prev-page').click((e) => {
                    return getTrips(e);
                });
            });

        e.preventDefault();
        return false;
    }
})();