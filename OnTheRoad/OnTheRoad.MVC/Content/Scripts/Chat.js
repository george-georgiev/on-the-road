$(() => {
    var chat = $.connection.chatHub;
    var messages = $('#messages-container');
    if (messages[0]) {
        messages[0].scrollTop = messages[0].scrollHeight;
    }

    chat.client.addMessage = (text, fromUsername) => {
        var targetUsername = $('#messages-container').attr('target-user');
        var newMessage = document.createElement('div');
        newMessage.className = 'message';
        if (fromUsername !== targetUsername) {
            newMessage.className += ' author';
        }

        newMessage.textContent = text;
        $('#messages-container').append(newMessage);

        $('#messages-container')[0].scrollTop = $('#messages-container')[0].scrollHeight;
    };

    $.connection.hub.start().done(() => {
        loadMessageSendEvent();
    });

    var previous;
    $('.conversation').click((e) => {
        var target = $(e.target).closest('.conversation');
        target.addClass('selected');
        if (previous) {
            previous.removeClass('selected');
        }

        previous = target;
        var username = target.attr('username');

        $.get(
            `/user/messages/${username}`,
            null,
            (data) => {
                var parser = new DOMParser()
                var doc = parser.parseFromString(data, "text/html");
                var chat = doc.getElementById('chat');

                $('#chat-container').html(chat);

                $('#messages-container')[0].scrollTop = $('#messages-container')[0].scrollHeight;

                loadMessageSendEvent();
            });
    });

    function loadMessageSendEvent() {
        var message = $('#message');
        var targetUsername = $('#messages-container').attr('target-user');

        $('#send-message').click(() => {
            console.log('here');
            chat.server.sendMessage(targetUsername, message.val());
            message.val('').focus();
        });
    }
});