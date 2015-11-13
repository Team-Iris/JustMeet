var userController = (function () {
    function Register() {
        templates
            .get('register')
            .then(function (data) {
                $('#main-container').html(data);
                $('#register-btn').on('click', function () {
                    RegisterUser();
                });
            });


    }

    function RegisterUser() {
        var username = $('#username').val();
        var password = $('#password').val();
        var passConfirm = $('#pass-confirm').val();
        
        data.register(username, password, passConfirm);
    }

    function Login() {

    }

    var userController = {
        register: Register,
        login: Login
    };

    return userController;
})();;