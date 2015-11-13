(function () {
    var sammyApp = Sammy('#content', function () {

        this.get('#/', function () {
            this.redirect('#/home');
        });

        this.get('#/login', navigation.login);

        this.get('#/logout', navigation.logout);

        this.get('#/register', navigation.register);

        this.get('#/home', homeView.all);
    });

    $(function () {
        sammyApp.run('#/home');
    })
}());