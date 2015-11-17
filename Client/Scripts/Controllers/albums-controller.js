var albumsController = (function () {
    var MyLocalHostWithPort = 'http://localhost:55833/',
        ACCESSTOKEN = 'x-auth-token';

    function createAlbum(album) {
        if (!(validator.validateName(1, 100, album.name, "Album Name") &&
            validator.validateName(1, 1000, album.description, "Album Description"))) {
            return;
        }

        var url = MyLocalHostWithPort + 'api/albums',
            options = {
                data: {
                    "Name": album.name,
                    "Description": album.description,
                    "Private": album.isPrivate
                },
                headers: {
                    'Authorization': 'Bearer ' + localStorage.getItem(ACCESSTOKEN),
                    'Content-Type': 'application/json'
                }
            };


        var promise = requester.post(url, options)
            .then(function (result) {
                console.log(result);
                console.log(result.result);

                toastr.success('Successfully created album with name ' + album.name + '!');
            })
            .catch(function (err) {
                //var errorDescription = JSON.parse(err.responseText).error_description;
                //toastr.error(errorDescription);
            });

        return promise;
    }

    return {
        createAlbum: createAlbum
    }
}());