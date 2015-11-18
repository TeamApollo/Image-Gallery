var albumsController = (function () {
    var defaultRoute = 'http://localhost:55833/', // http://imagegallery2015.azurewebsites.net/ <- replace with this for production
        ACCESSTOKEN = 'x-auth-token';

    function createAlbum(album) {
        if (!(validator.validateName(1, 100, album.name, "Album Name") &&
            validator.validateName(1, 1000, album.description, "Album Description"))) {
            return;
        }

        var url = defaultRoute + 'api/albums',
            options = {
                data: JSON.stringify({
                    "Name": album.name,
                    "Description": album.description,
                    "Private": album.isPrivate
                }),
                headers: {
                    'Authorization': 'Bearer ' + localStorage.getItem(ACCESSTOKEN),
                    'Content-Type': 'application/json'
                }
            };

        debugger;

        var promise = requester.post(url, options)
            .then(function (id) {
                toastr.success('Successfully created album ' + album.name + '!');
            })
            .catch(function (err) {
                var errorDescription = JSON.parse(err.responseText).error_description;
                toastr.error(errorDescription);
            });

        return promise;
    }

    function getAllAlbums() {
        var url = defaultRoute + 'api/albums',
            options = {};

        if (usersController.userLoggedIn()) {
            options.headers = {
                'Authorization': 'Bearer ' + localStorage.getItem(ACCESSTOKEN)
            }
        }

        var promise = new Promise(function (resolve, reject) {
            requester.get(url, options)
                .then(function (albums) {
                    resolve(albums);
                })
                .catch(function () {
                    toastr.error("No connection with the server!");
                    reject();
                })
        });

        return promise;
    }

    return {
        createAlbum: createAlbum,
        getAll: getAllAlbums
    }
}());