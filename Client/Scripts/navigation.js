var navigation = (function () {
    function login(context) {
        // in case someone just decides to type in the link #/login
        if (usersController.userLoggedIn()) {
            context.redirect('#/home');
            return;
        }

        loginView.show(context);
    }

    function logout(context) {
        usersController.logout();

        $('#div-reg').show();
        $('#div-loggedin').hide();

        context.redirect('#/home');
    }

    function register(context) {
        // in case someone just decides to type in the link #/register
        if (usersController.userLoggedIn()) {
            context.redirect('#/home');
            return;
        }

        registerView.show(context);
    }

    function createAlbum(context) {
        if (!usersController.userLoggedIn()) {
            toastr.error('You must be LoggedIn to do that!');
            context.redirect('#/home');
            return;
        }

        albumCreateView.show(context);
    }

    return {
        login: login,
        logout: logout,
        register: register,
        createAlbum: createAlbum
    };
}());