var data = (function () {
    function register(email, password, passwordConfirm) {
        var options = {
            data: {
                Email: email,
                Password: password,
                ConfirmPassword: passwordConfirm
            }
        };

        jsonRequester
            .post('http://localhost:53232/api/Account/Register', options)
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
        register: register
    };

    return data;
})();