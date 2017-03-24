(() => {
    var editBtn = $('#edit-btn');
    var userDisplay = $('#user-display');
    var userEdit = $('#user-edit');
    var saveBtn = $('#save-btn');

    editBtn.click(() => {
        editBtn.addClass('display-none');
        userDisplay.addClass('display-none');

        userEdit.removeClass('display-none');
    });

    var success = (data) => {
        $('#profile-img').attr('src', $('#image').attr('src'));
        var fullName = $('#FirstName').val() + ' ' + $('#LastName').val();
        $('#full-name').html(fullName);
        $('#city-name').html($('#CityName').val());
        $('#phone-number').html($('#PhoneNumber').val());
        $('#info').html($('#Info').val());

        editBtn.removeClass('display-none');
        userDisplay.removeClass('display-none');

        userEdit.addClass('display-none');

        toastr.success(data.DisplayMessage);
    }

    var error = (data) => {
        toastr.error(data.responseJSON.DisplayMessage);
    }

    saveBtn.click((e) => {
        $('#user-update').ajaxSubmit({ success, error });
    });

    var btnFollow = $('#btn-follow');
    var btnUnfollow = $('#btn-unfollow');
    btnFollow.click((e) => {
        console.log($('#username-span').html());
        $.post(
            '/user/profile/follow',
            { username: $('#username-span').html() },
            (data) => {
                btnFollow.addClass('display-none');
                btnUnfollow.removeClass('display-none');

                toastr.success(data.DisplayMessage);
            })
        .fail((data) => {
            console.log(data);
            toastr.error(data.responseJSON.DisplayMessage);
        });
    });

    btnUnfollow.click((e) => {
        $.post(
            '/user/profile/unfollow',
            { username: $('#username-span').html() },
            (data) => {
                btnFollow.removeClass('display-none');
                btnUnfollow.addClass('display-none');

                toastr.success(data.Data.DisplayMessage);
            })
        .fail((data) => {
            toastr.error(data.responseJSON.Data.DisplayMessage);
        });
    });

    $('.btn-unfollow').click((e) => {
        $.post(
            '/user/profile/unfollow',
            { username: $(e.target).attr('username') },
            (data) => {
                $(e.target).closest('div.fav-user-wrapper').remove();

                toastr.success(data.Data.DisplayMessage);
            })
        .fail((data) => {
            toastr.error(data.responseJSON.Data.DisplayMessage);
        });
    });
})();