var homeController = function () {
    function all(context) {
        templates.get('home')
            .then(function (template) {
                context.$element().html(template());
            }).then(function () {
                if (usersController.userLoggedIn()) {
                    $('#div-reg').hide();
                    $('#btn-logout').show();
                } else {
                    $('#div-reg').show();
                    $('#btn-logout').hide();
                }
            });
    }

    return {
        all: all
    };
}();