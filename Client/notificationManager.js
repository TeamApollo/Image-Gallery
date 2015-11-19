var notificationManager = function () {

    var pubNub = PUBNUB.init({
        subscribe_key: 'sub-c-9fe7c32e-8c77-11e5-84ee-0619f8945a4f',
        publish_key: 'pub-c-aa7e75ee-ca5f-40ef-95c9-fe28cd6830f6',
        ssl: false
    });

    var channelName = 'notify_for_comment';

    var subscribe = function () {

        pubNub.subscribe({
            channel: channelName,
            message: function (message) {
                console.log(message);
            },
            error: function (error) {
                console.log(JSON.stringify(error));
            }
        });
    };

    var publish = function (message) {

        pubNub.publish({
            channel: channelName,
            message: message
        });
    };

    return {
        publish: publish,
        subscribe: subscribe
    }
}();