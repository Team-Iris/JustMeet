var homeController = (function () {
    function all() {
        userController.setUserPanel();

        var info = data.auth.userInfo();
        templates
            .get('home')
            .then(function (template) {
                $('#main-container').html(template(info));
            })

        data.users.get(3)
            .then(function (users) {
                users.forEach(function(user){
                    user.Years = moment(user.DateOfBirth).fromNow();
                    var lastIndex = user.Years.lastIndexOf(" ");
                    user.Years = user.Years.substring(0, lastIndex);
                })
                return users;
            })
                 .then(function (users) {
                     templates
                     .get('list-users')
                     .then(function (template) {
                         $('#logged-in-container .row').html(template(users));
                         console.log(users)
                     })
                 })
    }

    var homeController = {
        all: all
    };

    return homeController;
})();;