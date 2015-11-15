var userController = (function () {
    function register() {
        templates
            .get('register')
            .then(function (data) {
                $('#main-container').html(data);
                $('#register-btn').on('click', function () {
                    registerUser();
                });

                $(function () {
                    $("#birthdate").datepicker();
                });
            });
    }

    function registerUser() {
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

        data.register(user);
    }

    function login() {
        templates
            .get('login')
            .then(function (data) {
                $('#main-container').html(data);
                $('#login-btn').on('click', function () {
                    loginUser();
                })
            })
    }

    function loginUser() {
        var username = $('#username').val();
        var password = $('#password').val();

        data.login(username, password);
    }

    var userController = {
        register: register,
        login: login
    };

    return userController;
})();;