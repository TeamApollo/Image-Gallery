var mediaFileController = (function () {
    var defaultRoute = 'http://imagegallery2015.azurewebsites.net/',
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
        var url = defaultRoute + 'api/fileupload',
            data = new FormData(),
            headers = {
                "X-Album-Id": albumId,
                'Authorization': 'Bearer ' + localStorage.getItem(ACCESSTOKEN)
            },
            files = $("#up-alb-media").get(0).files; // input field

        if (files.length > 0) {
            data.append("UploadedImage", files[0]);
        }

        var promise = new Promise(function (res, rej) {
            $.ajax({
                type: "POST",
                url: "http://localhost:55833/api/fileupload",
                contentType: false,
                processData: false,
                data: data,
                headers: headers,
                success: function(result) {
                    toastr.success("Media file uploaded successfully!");
                    res(result);
                },
                error: function(error) {
                    console.log(error);
                    toastr.error("Error uploading media file!");
                    rej(error);
                }
            });
        });

        return promise;
    }

    return {
        getByAlbum: getMediaForAlbum,
        uploadFile: uploadFile
    }
}());