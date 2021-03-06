﻿(() => {
    var subscription = $("#subscription");
    var tripId = subscription.attr('trip-id');
    subscription.change(() => {
        var statusValue = subscription.val();

        $.post(
            '/trips/subscribe',
            { tripId, statusValue },
            (data) => {
                toastr.success(data.DisplayMessage);
            })
            .fail(function (data) {
                toastr.error(data.responseJSON.DisplayMessage);
            });
    })
})()