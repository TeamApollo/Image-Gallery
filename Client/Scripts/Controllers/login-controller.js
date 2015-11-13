var loginController = function () {
    function login(context) {
        // in case someone just decides to type in the link
        if (usersController.userLoggedIn()) {
            context.redirect('#/home');
            return;
        }

        templates.get('login')
            .then(function (template) {
                context.$element().html(template());
            }).then(function () {
                $('#btn-login-submit').on('click', function () {
                    var user = {
                        email: $('#tb-log-email').val(),
                        password: $('#tb-log-password').val()
                    };

                    usersController.login(user);
                    // needed for logout button to appear but first the query must be completed
                    setTimeout(function() { context.redirect('#/home') }, 100);
                });
            });
    }

    return {
        login: login
    };
}();