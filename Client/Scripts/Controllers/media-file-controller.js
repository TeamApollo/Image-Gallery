var mediaFileController = (function () {
    var defaultRoute = 'http://localhost:55833/', // http://imagegallery2015.azurewebsites.net/ <- replace with this for production
        ACCESSTOKEN = 'x-auth-token',
        USERNAME = 'x-user-name';

    function getMediaForAlbum(id) {
        var url = defaultRoute + 'api/images', //?albumId=5',
            options = {
                data: 'albumId=' + id
            };

        if (usersController.userLoggedIn()) {
            options.headers = {
                'Authorization': 'Bearer ' + localStorage.getItem(ACCESSTOKEN)
            };
        }

        var promise = new Promise(function (res, rej) {
            requester.get(url, options)
                .then(function (result) {
                    res(result);
                })
                .catch(function (err) {
                    toastr.error("Couldn't find media files for this album!");
                    rej(err);
                });
        });

        return promise;
    }

    function uploadFile(albumId) {
        var url = defaultRoute + 'api/file-upload',
            data = new FormData(),
            files = $("#up-alb-media").get(0).files; // input field

        if (files.length > 0) {
            data.append("UploadedImage", files[0]);
        }

        var options = {
            data: data,
            headers: {
                "X-Album-Id": albumId,
                'Authorization': 'Bearer ' + localStorage.getItem(ACCESSTOKEN)
            }
        };

        var promise = new Promise(function (res, rej) {
            requester.post(url, options)
                .then(function (result) {
                    toastr.success('Uploaded successfully!');
                    res();
                })
                .catch(function (err) {
                    toastr.error("Couldn't upload!");
                    rej();
                });
        });

        return promise;
    }

    return {
        getByAlbum: getMediaForAlbum,
        uploadFile: uploadFile
    }
}());