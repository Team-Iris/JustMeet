var listUsersController = (function () {

    function list(context) {
        data.users.get(3)
            .then(function(users) {
                templates
                .get('list-users')
                .then(function (template) {
                    $('#logged-in-container .row').html(template(users));
                })
            })
        }

    return {
        list: list
    }
}());