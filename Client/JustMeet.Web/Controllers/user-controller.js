var userController = (function () {
    var CONSTANTS = {
        USERNAME_LOCAL_STORAGE_KEY: 'username',
        AUTH_KEY_LOCAL_STORAGE_KEY: 'user-auth-key'
    };

    function setUserPanel() {
        var info = data.userInfo();
        templates
            .get('user-panel')
            .then(function (template) {
                $('#user-panel').html(template(info));
            })
    }

    function logout(context) {
        localStorage.removeItem(CONSTANTS.USERNAME_LOCAL_STORAGE_KEY);
        localStorage.removeItem(CONSTANTS.AUTH_KEY_LOCAL_STORAGE_KEY);
        context.redirect('#/');
    }

    function register(context) {
        templates
            .get('register')
            .then(function (data) {
                $('#main-container').html(data);
                $('#register-btn').on('click', function () {
                    registerUser(context);
                });

                $(function () {
                    $("#birthdate").datepicker();
                });
            });
    }

    function registerUser(context) {
        var username = $('#username').val();
        var password = $('#password').val();
        var passConfirm = $('#pass-confirm').val();
        var firstName = $('#firstname').val();
        var lastName = $('#lastname').val();
        var dateofbirth = $('#birthdate').val();
        var isMale = $('#is-male-radio').is(':checked');

        var user = {
            email: username,
            password: password,
            confirmPassword: passConfirm,
            firstName: firstName,
            lastName: lastName,
            dateofbirth: dateofbirth,
            isMale: isMale
        }

        data.register(user, context);
    }

    function login(context) {
        templates
            .get('login')
            .then(function (data) {
                $('#main-container').html(data);
                $('#login-btn').on('click', function () {
                    loginUser(context);
                })
            })
    }

    function loginUser(context) {
        var username = $('#username').val();
        var password = $('#password').val();

        data.login(username, password, context);
    }

    var userController = {
        register: register,
        login: login,
        setUserPanel: setUserPanel,
        logout: logout
    };

    return userController;
})();;