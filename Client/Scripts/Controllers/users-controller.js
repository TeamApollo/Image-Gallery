var usersController = (function () {
    var MyLocalHostWithPort = 'http://localhost:55833/',
        ACCESSTOKEN = 'x-auth-token';

    //function all(context) {
    //    var users,
    //        authKey = localStorage.getItem(ACCESSTOKEN);
    //    requester.get(url, false, authKey)
    //        .then(function (requestedUsers) {
    //            users = requestedUsers.result;
    //            return templates.get('users');
    //        })
    //        .then(function (template) {
    //            context.$element().html(template(users));
    //        })
    //        .catch(function (err) {
    //            toastr.error(err.responseText);
    //        });
    //}

    function userLogin(user) {
        var url = MyLocalHostWithPort + 'token',
            data = "username=" + user.email + "&password=" + user.password + "&grant_type=password";
        contentType = 'application/x-www-form-urlencoded; charset=utf-8';

        var promise = requester.post(url, data, false, contentType)
            .then(function (result) {
                var accessToken = result.access_token;
                localStorage.setItem(ACCESSTOKEN, accessToken);
                toastr.success(user.email + ', you logged in successfully!');
            })
            .catch(function (err) {
                toastr.error(err.responseText);
            });

        return promise;
    }

    function userRegister(user) {
        if (!(validator.validateName(user.firstName, "First Name") &&
            validator.validateName(user.lastName, "Last Name") &&
            validator.validateEmail(user.email, "Email") &&
            validator.validatePassword(user.password, "Password") &&
            validator.validatePassword(user.confirmPassword, "Confirmation Password"))) {
            return
        }

        var url = MyLocalHostWithPort + 'api/account/register',
            contentType = 'application/json',
            data = {
                'firstName': user.firstName,
                'lastName': user.lastName,
                'email': user.email,
                'password': user.password,
                'confirmPassword': user.confirmPassword
            };

        requester.post(url, data, false, contentType)
            .then(function (result) {
                toastr.success(user.email + ', you registered successfully!');
            })
            .catch(function (err) {
                toastr.error(err.responseText);
            });
    }

    function userLoggedIn() {
        var username = localStorage.getItem(ACCESSTOKEN);
        return !!username;
    }

    function userLogout() {
        localStorage.removeItem(ACCESSTOKEN);
    }

    return {
        login: userLogin,
        userLoggedIn: userLoggedIn,
        register: userRegister,
        logout: userLogout
    };
})();