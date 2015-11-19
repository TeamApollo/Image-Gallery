var albumsManageView = (function () {
    var USERNAME = 'x-user-name';

    function show(context) {
        if (!usersController.userLoggedIn()) { // in case someone just decides to type #/manage-albums
            context.redirect('#/home');
            return;
        }

        templates.get('albums-manage')
            .then(function (template) {
                var albums = {};
                var currentUsername = localStorage.getItem(USERNAME);
                albumsController.getAllAlbumsByUser(currentUsername)
                    .then(function (collection) {
                        collection.forEach(function (album) {
                            album.CreatedOn = moment(album.CreatedOn).format('LLLL');
                        });
                        albums.collection = collection;
                        context.$element().html(template(albums));
                        $('.well').on('click', '.btn-alb-upload-media', function () {
                            var clickedAlbumLink = $(this).siblings('a').attr('href');
                            var clickedAlbumId = clickedAlbumLink.substr(clickedAlbumLink.lastIndexOf('/') + 1, clickedAlbumLink.length);
                            mediaFileController.uploadFile(clickedAlbumId); // promise
                        });
                    })
                    .catch(function () {
                        toastr.error("No albums for this user, or no connection with the server!");
                        context.redirect('#/home');
                    });
            })
    }

    return {
        show: show
    }
}());