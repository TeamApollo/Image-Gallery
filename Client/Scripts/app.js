(function () {
    var sammyApp = Sammy('#content', function () {

        this.get('#/', function () {
            this.redirect('#/home');
        });

        this.get('#/login', navigation.login);

        this.get('#/logout', navigation.logout);

        this.get('#/register', navigation.register);

        this.get('#/home', homeView.all);

        this.get('#/create-album', albumCreateView.show);

        this.get('#/album/:id', albumView.show);

        this.get('#/manage-albums', albumsManageView.show);
    });

    $(function () {
        sammyApp.run('#/home');

        if (usersController.userLoggedIn()) {
            $('#div-reg').hide();
            $('#div-loggedin').show();
        } else {
            $('#div-reg').show();
            $('#div-loggedin').hide();
        }
    })
}());