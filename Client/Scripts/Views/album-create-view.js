var albumCreateView = (function () {
    function show(context) {
        templates.get('album-create')
            .then(function (template) {
                context.$element().html(template());
            }).then(function () {
                $('#btn-alb-create').on('click', function () {
                    var album = {
                        name: $('#tb-alb-name').val(),
                        description: $('#tb-alb-desc').val(),
                        isPrivate: $('#cb-alb-private').val()
                    };

                    albumsController.createAlbum(album).then(function () {
                        context.redirect('#/home');
                    });
                });
            });
    }

    return {
        show: show
    }
}());