var commentsController = (function () {
    var defaultRoute = 'http://imagegallery2015.azurewebsites.net/',
        ACCESSTOKEN = 'x-auth-token';

    function createComment(comment) {
        if (!(validator.validateName(1, 4000, comment.Body, "Comment Body"))) {
            return;
        }

        var url = defaultRoute + 'api/comments',
            options = {
                data: {
                    "Body": comment.Body,
                    "AlbumId": comment.AlbumId,
                    "UserName": comment.User.Name
                }
            };

        var promise = requester.post(url, options)
            .then(function (id) {
                toastr.success('Comment Added!');
            })
            .catch(function (err) {
                debugger;
                var errorDescription = JSON.parse(err.responseText).error_description;
                toastr.error(errorDescription);
            });

        return promise;
    }

    function getAllCommentsByAlbum(id) {
        var url = defaultRoute + 'api/comments',
            options = {
                data: "albumId=" + id
            };

        var promise = new Promise(function (resolve, reject) {
            requester.get(url, options)
                .then(function (comments) {
                    resolve(comments);
                })
                .catch(function (error) {
                    if(error.statusText !== "Not Found") {
                        toastr.error("No connection with the server!");
                    }

                    reject(error);
                })
        });

        return promise;
    }

    return {
        createComment: createComment,
        getAllCommentsByAlbum: getAllCommentsByAlbum
    }
}());