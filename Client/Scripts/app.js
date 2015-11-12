(function () {
    var sammyApp = Sammy('#content', function () {

        this.get('#/', function () {
            this.redirect('#/home');
        });

        this.get('#/home', homeController.all);

        this.get('#/register', registerController.reg);

        this.get('#/login', loginController.login);

        this.get('#/logout', logoutController.logout);

        //this.get('#/users', viewUsersController.all);

        //this.get('#/cookies', cookieController.all);
        //this.get('#/cookies/share', cookieController.share);
        //this.get('#/my-cookie', cookieController.draw);

        //this.get('#/categories', categoriesController.all);
    });

    $(function () {
        sammyApp.run('#/home');
    })
}());