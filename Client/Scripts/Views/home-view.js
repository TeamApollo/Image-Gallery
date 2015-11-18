var homeView = function () {
    function all(context) {
        templates.get('home')
            .then(function (template) {
                var albums = {};
                albumsController.getAll()
                    .then(function(collection) {
                        albums.collection = collection;
                        context.$element().html(template(albums));
                    })
                    .catch(function() {
                        context.$element().html(template(albums));
                    });

            }).then(function () {

            });
    }

    return {
        all: all
    };
}();