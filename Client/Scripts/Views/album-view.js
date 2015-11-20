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
                                commentsController.getAllCommentsByAlbum(album.Id)
                                    .then(function(comments) {
                                        album.Comments = comments;
                                    });
                                context.$element().html(template(album));
                                $(".imgLiquid").imgLiquid();
                                $('#btn-comment-body').on('click', function() {
                                    var commentBody = $('#comment-body').val();
                                    var comment = {
                                        Body: commentBody,
                                        AlbumId: album.Id,
                                        Author: album.Author
                                    };

                                    commentsController.createComment(comment)
                                });
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