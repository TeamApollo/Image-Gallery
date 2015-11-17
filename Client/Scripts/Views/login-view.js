var loginView = (function () {
    function show(context) {
        templates.get('login')
            .then(function (template) {
                context.$element().html(template());
            }).then(function () {
                $('#btn-login-submit').on('click', function () {
                    var user = {
                        email: $('#tb-log-email').val(),
                        password: $('#tb-log-password').val()
                    };

                    usersController.login(user)
                        .then(function () {
                            $('#div-reg').hide();
                            $('#div-loggedin').show();
                            context.redirect('#/home');
                        }, function () {
                        });
                })
            });
    }

    return {
        show: show
    }
}());