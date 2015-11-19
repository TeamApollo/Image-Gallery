var tagsController = (function () {
    var defaultRoute = 'http://imagegallery2015.azurewebsites.net/',
        ACCESSTOKEN = 'x-auth-token';

    function createTag(tag) {
        if (!(validator.validateName(1, 50, tag.description, "Tag Description"))) {
            return;
        }

        var url = defaultRoute + 'api/tags',
            options = {
                data: JSON.stringify({
                    "Description": tag.description
                }),
                headers: {
                    'Authorization': 'Bearer ' + localStorage.getItem(ACCESSTOKEN),
                    'Content-Type': 'application/json'
                }
            };

        debugger;

        var promise = requester.post(url, options)
            .then(function (id) {
                toastr.success('Successfully created tag ' + tag.description + '!');
            })
            .catch(function (err) {
                var errorDescription = JSON.parse(err.responseText).error_description;
                toastr.error(errorDescription);
            });

        return promise;
    }

    function getAllTags() {
        var url = defaultRoute + 'api/tags',
            options = {};

        if (usersController.userLoggedIn()) {
            options.headers = {
                'Authorization': 'Bearer ' + localStorage.getItem(ACCESSTOKEN)
            }
        }

        var promise = new Promise(function (resolve, reject) {
            requester.get(url, options)
                .then(function (tags) {
                    resolve(tags);
                })
                .catch(function () {
                    toastr.error("No connection with the server!");
                    reject();
                })
        });

        return promise;
    }

    return {
        createTag: createTag,
        getAll: getAllTags
    }
}());