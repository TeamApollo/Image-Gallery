var logoutController = function () {
    function logout(context) {
        usersController.logout();
        context.redirect('#/home');
    }

    return {
        logout: logout
    };
}();