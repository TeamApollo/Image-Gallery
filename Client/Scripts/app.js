(function () {
    var sammyApp = Sammy('#content', function () {

        this.get('#/', function () {
            this.redirect('#/home');
        });

        this.get('#/home', homeController.all);

        this.get('#/register', registerController.reg);

        this.get('#/login', loginController.login);

        this.get('#/logout', logoutController.logout);
    });

    $(function () {
        sammyApp.run('#/home');
    })
}());