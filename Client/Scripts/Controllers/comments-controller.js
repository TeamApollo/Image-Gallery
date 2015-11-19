var albumsComments = (function () {
    var defaultRoute = 'http://localhost:55833/', // http://imagegallery2015.azurewebsites.net/ <- replace with this for production
        ACCESSTOKEN = 'x-auth-token';

    function createComment(comment) {
        if (!(validator.validateName(1, 4000, comment.body, "Comment Body"))) {
            return;
        }

        var url = defaultRoute + 'api/comments',
            options = {
                data: JSON.stringify({
                    "Body": comment.body
                }),
                headers: {
                    'Authorization': 'Bearer ' + localStorage.getItem(ACCESSTOKEN),
                    'Content-Type': 'application/json'
                }
            };

        debugger;

        var promise = requester.post(url, options)
            .then(function (id) {
                toastr.success('Successfully created comment ' + comment.body + '!');
            })
            .catch(function (err) {
                var errorDescription = JSON.parse(err.responseText).error_description;
                toastr.error(errorDescription);
            });

        return promise;
    }

    function getAllComments() {
        var url = defaultRoute + 'api/comments',
            options = {};

        if (usersController.userLoggedIn()) {
            options.headers = {
                'Authorization': 'Bearer ' + localStorage.getItem(ACCESSTOKEN)
            }
        }

        var promise = new Promise(function (resolve, reject) {
            requester.get(url, options)
                .then(function (comments) {
                    resolve(comments);
                })
                .catch(function () {
                    toastr.error("No connection with the server!");
                    reject();
                })
        });

        return promise;
    }

    return {
        createComment: createComment,
        getAll: getAllComments
    }
}());