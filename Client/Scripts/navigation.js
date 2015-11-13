var navigation = (function () {
    function login(context) {
        // in case someone just decides to type in the link #/login
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

                    usersController.login(user).then(function () {
                        context.redirect('#/home')
                    });
                });
            });
    }

    function logout(context) {
        usersController.logout();
        context.redirect('#/home');
    }

    function register(context) {
        // in case someone just decides to type in the link #/register
        if (usersController.userLoggedIn()) {
            context.redirect('#/home');
            return;
        }

        templates.get('register')
            .then(function (template) {
                context.$element().html(template());
            })
            .then(function () {
                $('#btn-register-submit').on('click', function () {
                    var user = {
                        'firstName': $('#tb-reg-firstName').val(),
                        'lastName': $('#tb-reg-lastName').val(),
                        'email': $('#tb-reg-email').val(),
                        'password': $('#tb-reg-password').val(),
                        'confirmPassword': $('#tb-reg-confirmPassword').val()
                    };

                    usersController.register(user);
                    context.redirect('#/home');
                });
            });
    }

    return {
        login: login,
        logout: logout,
        register: register
    };
}());