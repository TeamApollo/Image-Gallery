var albumView = (function () {
    function show(context) {
        var albumId = this.params['id'];
        templates.get('album')
            .then(function (template) {
                albumsController.getAlbumById(albumId)
                    .then(function (album) {
                        album.CreatedOn = moment(album.CreatedOn).format('LLLL');
                        mediaFileController.getByAlbum(album.Id)
                            .then(function (files) {
                                album.MediaFiles = files;
                                context.$element().html(template(album));
                                $(".imgLiquid").imgLiquid();
                            });
                    })
                    .catch(function (e) {
                        toastr.error("No connection with the server!");
                        context.redirect('#/home');
                    });
            });
    }

    return {
        show: show
    }
}());