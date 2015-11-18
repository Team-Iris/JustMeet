var data = (function () {
    var CONSTANTS = {
        USERNAME_LOCAL_STORAGE_KEY: 'username',
        AUTH_KEY_LOCAL_STORAGE_KEY: 'user-auth-key'
    };

    function userIsLoggedIn() {
        var hasUsername = localStorage.getItem(CONSTANTS.USERNAME_LOCAL_STORAGE_KEY) != undefined;
        var hasAuthKey = localStorage.getItem(CONSTANTS.AUTH_KEY_LOCAL_STORAGE_KEY) != undefined;

        return hasUsername && hasAuthKey;
    }

    function getUserInfo() {
        return {
            username: localStorage.getItem(CONSTANTS.USERNAME_LOCAL_STORAGE_KEY),
            authkey: localStorage.getItem(CONSTANTS.AUTH_KEY_LOCAL_STORAGE_KEY),
            isLoggedIn: userIsLoggedIn()
        };
    }

    function register(info, context) {
        var options = {
            data: info
        };

        jsonRequester
            .post('http://localhost:53232/api/Account/Register', options)
            .then(function (response) {
                toastr.success('Register was successful.');
                context.redirect('/#/');
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

    function login(username, password, context) {
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
                toastr.success('Login was successful.');
                localStorage.setItem(CONSTANTS.USERNAME_LOCAL_STORAGE_KEY, response['userName']);
                localStorage.setItem(CONSTANTS.AUTH_KEY_LOCAL_STORAGE_KEY, response['access_token']);
                context.redirect('#/');
            },
            function (err) {
                var error = $.parseJSON(err.responseText)['error_description'];
                toastr.error(error);
            });
    };

    function myProfile() {
        var options = {
            headers: {
                Authorization: 'Bearer ' + localStorage[CONSTANTS.AUTH_KEY_LOCAL_STORAGE_KEY]
            }
        }

        return jsonRequester.get('http://localhost:53232/api/users/profile', options)
        .then(function (response) {
            return response
        })
    };

    function get(number) {
        var options = {
            headers: {
                Authorization: 'Bearer ' + localStorage[CONSTANTS.AUTH_KEY_LOCAL_STORAGE_KEY]
            }
        }

        return jsonRequester.get('http://localhost:53232/api/users/random?numberOfUsers=3', options)
        .then(function (response) {
            return response
        })
    }

    function update(user) {
        var options = {
            headers: {
                Authorization: 'Bearer ' + localStorage[CONSTANTS.AUTH_KEY_LOCAL_STORAGE_KEY]
            },
            data: {
                user
            }
        }

        return jsonRequester.post('http://localhost:53232/api/users/update', options)
            .then(function (response) {
                return response
            })
    }

    return ({
        auth: {
            register: register,
            login: login,
            hasUser: userIsLoggedIn,
            userInfo: getUserInfo
        },
        profile: {
            myProfile: myProfile
        },
        users: {
            get: get,
            update: update
        }
    });
})();