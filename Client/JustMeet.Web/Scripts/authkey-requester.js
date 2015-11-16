var authkeyRequester = (function () {
    function send(method, url, options) {
        options = options || {};

        var headers = options.headers || {},
          data = options.data || undefined;

        var promise = new Promise(function (resolve, reject) {
            $.ajax({
                url: url,
                method: method,
                contentType: 'application/json',
                headers: headers,
                data: data,
                success: function (res) {
                    resolve(res);
                },
                error: function (err) {
                    reject(err);
                }
            });
        });
        return promise;
    }

    function post(url, options) {
        return send('POST', url, options);
    }

    return {
        post: post,
    };
}());