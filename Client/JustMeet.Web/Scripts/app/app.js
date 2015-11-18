$(function () {
    userController.setUserPanel();

    var app = Sammy('#main-container', function () {
        this.get('#/', function () {
            this.redirect('#/home');
        });

        this.get('#/home', homeController.all);

        this.get('#/login', userController.login);

        this.get('#/logout', userController.logout);

        this.get('#/register', userController.register);

        this.get('#/my-profile', myProfileController.all);
    });

    $(function () {
        app.run('#/');
    });
});