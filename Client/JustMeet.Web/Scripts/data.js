var data = (function () {
    function register(info) {
        var options = {
            data: info
        };

        jsonRequester
            .post('http://localhost:53232/api/Account/Register', options)
            .then(function (response) {
                toastr.success('Register was successful.');
            },
            function (err) {
                var modelState = $.parseJSON(err.responseText).ModelState;

                for (var key in modelState) {
                    var errors = modelState[key];

                    for (var index in errors) {
                        toastr.error(errors[index]);
                    }
                }
            });
    };

    function login(username, password) {
        var options = {
            data: {
                username: username,
                password: password,
                grant_type: 'password'
            }
        };

        authkeyRequester
            .post('http://localhost:53232/api/Account/Login', options)
            .then(function (response) {
                console.log(response);
            },
            function (err) {
                var modelState = $.parseJSON(err.responseText).ModelState;

                for (var key in modelState) {
                    var errors = modelState[key];

                    for (var index in errors) {
                        toastr.error(errors[index]);
                    }
                }
            });
    };

    var data = {
        register: register,
        login: login
    };

    return data;
})();