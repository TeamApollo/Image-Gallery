var homeView = function () {
    function all(context) {
        templates.get('home')
            .then(function (template) {
                context.$element().html(template());
            }).then(function () {

            });
    }

    return {
        all: all
    };
}();