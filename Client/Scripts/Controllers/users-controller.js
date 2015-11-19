var usersController = (function () {
    var defaultRoute = 'http://imagegallery2015.azurewebsites.net/',
        ACCESSTOKEN = 'x-auth-token',
        USERNAME = 'x-user-name';

    function userLogin(user) {
        var url = defaultRoute + 'token',
            options = {
                data: "username=" + user.email + "&password=" + user.password + "&grant_type=password"
            };

        var promise = new Promise(function (res, rej) {
            requester.post(url, options)
                .then(function (result) {
                    var accessToken = result.access_token;
                    localStorage.setItem(ACCESSTOKEN, accessToken);
                    localStorage.setItem(USERNAME, user.email);
                    toastr.success(user.email + ', you logged in successfully!');
                    res();
                })
                .catch(function (err) {
                    var errorDescription = JSON.parse(err.responseText).error_description;
                    toastr.error(errorDescription);
                    rej();
                });
        });

        return promise;
    }

    function userRegister(user) {
        if (!(validator.validateName(3, 30, user.firstName, "First Name") &&
            validator.validateName(3, 30, user.lastName, "Last Name") &&
            validator.validateEmail(user.email, "Email") &&
            validator.validatePassword(6, 100, user.password, "Password") &&
            validator.validatePassword(6, 100, user.confirmPassword, "Confirmation Password"))) {
            return
        }

        var url = defaultRoute + 'api/account/register',
            options = {
                data: {
                    'firstName': user.firstName,
                    'lastName': user.lastName,
                    'email': user.email,
                    'password': user.password,
                    'confirmPassword': user.confirmPassword
                }
            };

        var promise = new Promise(function (res, rej) {
            requester.post(url, options)
                .then(function () {
                    toastr.success(user.email + ', you registered successfully!');
                    res();
                })
                .catch(function (err) {
                    var errorResponseText = JSON.parse(err.responseText);
                    var errorDescription = errorResponseText.error_description;
                    if(!errorDescription) {
                        for (var attr in errorResponseText) {
                            if (errorResponseText[attr] !== undefined && attr !== 'Message') {
                                for (var prop in errorResponseText[attr]) {
                                    errorResponseText[attr][prop].forEach(function (error) {
                                        toastr.error(error);
                                    });
                                }
                            }
                        }
                    } else {
                        toastr.error(errorDescription);
                    }
                    rej();
                });
        });

        return promise;
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
})
();