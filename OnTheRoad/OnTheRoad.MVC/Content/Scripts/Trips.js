(() => {
    var subscription = $("#subscription");
    var tripId = subscription.attr('trip-id');
    subscription.change(() => {
        var statusValue = subscription.val();

        $.post(
            '/trips/subscribe',
            { tripId, statusValue },
            (data) => {
                if (data.Status === 0) {
                    toastr.success(data.DisplayMessage);
                } else if (data.Status === 1) {
                    toastr.error(data.DisplayMessage);
                }
            });
    })
})()