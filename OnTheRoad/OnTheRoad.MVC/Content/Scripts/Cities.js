(() => {
    var cities = $('#cities');
    cities.change(() => {
        var selectedId = cities.val();
        $('#CityId').val(selectedId);

        var selectedName = $('#cities option:selected').text();
        $('#CityName').val(selectedName);
    })
})();