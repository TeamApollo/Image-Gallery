var validation = function () {
    var isString = function validateString(str, msg) {
        if (typeof str !== 'string') {
            toastr.error(msg + ' must be string!');
            return false;
        }

        return true;
    };

    var length = function validateLength(str, msg) {
        if (str.length < 6 || str.length > 30) {
            toastr.error(msg + ' must have length between 6 and 30!');
            return false;
        }

        return true;
    };

    var regex = function validateChars(str, msg) {
        if (str.match("^[a-zA-Z0-9_. ]+$")) {
            return true;
        }

        toastr.error(msg + ' can contain only Latin letters, digits and the characters, "_" and "."');
        return false;
    };
    
    var imgUrl = function validateUrl(url, msg) {
        var substring = url.substr(0, 7);
        if (substring === 'http://') {
            return true;
        }

        toastr.error(msg + 'must be a valid url!');
        return false;
    };

    function validateUsername(str, msg) {
        return isString(str, msg) && length(str, msg) && regex(str, msg);
    }

    function validateFortuneCookieText(text, msg) {
        return isString(text, msg) && length(text, msg);
    }

    function validateFortuneCookieCategory(category, msg) {
        return isString(category, msg) && length(category, msg);
    }

    function validateFortuneCookieImgUrl(url, msg) {
        return isString(url, msg) && imgUrl(url, msg);
    }

    return {
        validateUsername: validateUsername,
        validateFortuneCookieText: validateFortuneCookieText,
        validateFortuneCookieCategory: validateFortuneCookieCategory,
        validateFortuneCookieImgUrl: validateFortuneCookieImgUrl
    };
}();