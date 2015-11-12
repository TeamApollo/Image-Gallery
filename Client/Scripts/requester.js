var requester = function () {
    'use strict';

    function httpRequest(method, url, data, headers, contentType) {
        var promise = new Promise(function (resolve, reject) {
            var objectRequest = {
                url: url,
                method: method,
                crossDomain: true,
                success: function (res) {
                    resolve(res);
                },
                error: function (err) {
                    reject(err);
                }
            };

            if (data) {
                objectRequest.data = JSON.stringify(data);
            }

            if (contentType) {
                objectRequest.contentType = contentType
            }

            //if (authKey) {
            //    objectRequest.headers = {'x-auth-key': authKey};
            //}

            if (headers) {
                objectRequest.headers = headers;
            }

            $.ajax(objectRequest);
        });

        return promise;
    }

    function get(url, data, authKey) {
        return httpRequest('GET', url, data, authKey);
    }

    function post(url, data, authKey) {
        return httpRequest('POST', url, data, authKey);
    }

    function put(url, data, authKey) {
        return httpRequest('PUT', url, data, authKey);
    }

    return {
        get: get,
        post: post,
        put: put
    };
}();