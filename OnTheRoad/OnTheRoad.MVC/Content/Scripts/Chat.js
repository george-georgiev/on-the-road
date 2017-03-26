$(() => {
    var chat = $.connection.chatHub;
    var messages = $('#messages-container');
    if (messages[0]) {
        messages[0].scrollTop = messages[0].scrollHeight;
    }

    var targetUsername = messages.attr('target-user');
    var message = $('#message');

    chat.client.addMessage = (text, fromUsername) => {
        var newMessage = document.createElement('div');
        newMessage.className = 'message';
        if (fromUsername !== targetUsername) {
            newMessage.className += ' author';
        }

        newMessage.textContent = text;
        messages.append(newMessage);

        messages[0].scrollTop = messages[0].scrollHeight;
    };

    $.connection.hub.start().done(() => {
        $('#send-message').click(() => {
            chat.server.sendMessage(targetUsername, message.val());
            message.val('').focus();
        });
    });

    $('.conversation').click((e) => {
        var username = $(e.target).closest('.conversation').attr('username');
        console.log(username);
        $.get(
            `/user/messages/${username}`,
            null,
            (data) => {
                console.log(data);
                $('#chat-container').html(data);
            });
    })
});