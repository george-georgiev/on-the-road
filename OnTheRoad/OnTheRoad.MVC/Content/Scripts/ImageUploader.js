(() => {
    var success = (data) => {
        console.log(data);
            var content = data.Content;
            $('#CoverImage').val(content);
            $('#image').attr('src', `data:image/jpeg;base64,${content}`);
    }

    var error = (data) => {
        toastr.error(data.responseJSON.DisplayMessage);
    }

    $('#image-submit').click((e) => {
        $('#file-upload').ajaxSubmit({ success, error });
    });
})();