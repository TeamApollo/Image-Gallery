var registerController = function () {
    function reg(context) {
        // in case someone just decides to type in the link
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
        reg: reg
    };
}();