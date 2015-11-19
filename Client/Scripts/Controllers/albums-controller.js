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
                    reject();
                })
        });

        return promise;
    }

    function getAlbumById(id) {
        var url = defaultRoute + 'api/albums/' + id,
            options = {
                headers: {
                    'Authorization': 'Bearer ' + localStorage.getItem(ACCESSTOKEN)
                }
            };

        var promise = new Promise(function (resolve, reject) {
            requester.get(url, options)
                .then(function (album) {
                    // TODO: must get the album pictures, put it on album.pictures
                    resolve(album);
                })
                .catch(function () {
                    reject();
                })
        });

        return promise;
    }

    function getAlbumsByPage(pageNumber, itemsPerPage) {
        var url = defaultRoute + 'api/albums/all',
            options = {
                data: 'page=' + pageNumber + '&pageSize=' + itemsPerPage
            };

        var promise = new Promise(function (resolve, reject) {
            requester.get(url, options)
                .then(function (albums) {
                    resolve(albums);
                })
                .catch(function () {
                    reject();
                })
        });

        return promise;
    }

    function getAllAlbumsByUser(ownerName) {
        var url = defaultRoute + 'api/albums/user',
            options = {
                data: 'owner=' + ownerName,
            };

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
                    reject();
                })
        });

        return promise;
    }

    function deleteAlbumById(id) {
        var url = defaultRoute + 'api/albums/' + id,
            options = {
                headers: {
                    'Authorization': 'Bearer ' + localStorage.getItem(ACCESSTOKEN)
                }
            };

        var promise = new Promise(function (resolve, reject) {
            requester.del(url, options)
                .then(function () {
                    toastr.success('Successfully deleted album!');
                    resolve();
                })
                .catch(function (err) {
                    console.log(err);
                    toastr.error('There was an error deleting!');
                    reject(err);
                })
        });

        return promise;
    }

    return {
        createAlbum: createAlbum,
        getAll: getAllAlbums,
        getAlbumById: getAlbumById,
        getAlbumsByPage: getAlbumsByPage,
        getAllAlbumsByUser: getAllAlbumsByUser,
        deleteAlbumById: deleteAlbumById
    }
}());