$(function () {
    var app = Sammy('#main-container', function () {
        this.get('/#/', function () {
            this.redirect('/#/home');
        });

        this.get('/#/home', function () {

        });

        this.get('/#/login', function () {
        });

        this.get('/#/logout', function () {
        });

        this.get('/#/register', userController.register);
    });

    $(function () {
        app.run('/#/home');
    });
});