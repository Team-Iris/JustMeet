var homeController = (function () {
    function all(context) {
        $('#main-container').html('');
        userController.setUserPanel();
    }

    var homeController = {
        all: all
    };

    return homeController;
})();;