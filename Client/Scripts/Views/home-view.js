var homeView = function () {
    var USERNAME = 'x-user-name';

    function all(context) {
        templates.get('home')
            .then(function (template) {
                var albums = {};
                albumsController.getAll()
                    .then(function (collection) {
                        var currentUsername = localStorage.getItem(USERNAME);
                        collection.forEach(function (album) {
                            album.CreatedOn = moment(album.CreatedOn).format('LLLL');
                            //if(album.Author == currentUsername) {
                            //    album.CanAddMedia = 'true'
                            //}
                            // TODO: get all images for every album
                        });
                        albums.collection = collection;
                        context.$element().html(template(albums));
                        //initCarousel();
                    })
                    .catch(function () {
                        toastr.error("No connection with the server!");
                        context.$element().html(template(albums));
                    });

            }).then(function () {
                //var $container = $('.container');
                //$container.on('click', '.album-link', function () {
                //    console.log('album clicked');
                //});
            });
    }

    function initCarousel() {
        $('.jcarousel').jcarousel();

        $('.jcarousel-control-prev')
            .on('jcarouselcontrol:active', function() {
                $(this).removeClass('inactive');
            })
            .on('jcarouselcontrol:inactive', function() {
                $(this).addClass('inactive');
            })
            .jcarouselControl({
                target: '-=1'
            });

        $('.jcarousel-control-next')
            .on('jcarouselcontrol:active', function() {
                $(this).removeClass('inactive');
            })
            .on('jcarouselcontrol:inactive', function() {
                $(this).addClass('inactive');
            })
            .jcarouselControl({
                target: '+=1'
            });

        $('.jcarousel-pagination')
            .on('jcarouselpagination:active', 'a', function() {
                $(this).addClass('active');
            })
            .on('jcarouselpagination:inactive', 'a', function() {
                $(this).removeClass('active');
            })
            .jcarouselPagination();
    }

    return {
        all: all
    };
}();