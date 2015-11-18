var updateProfileController = (function () {
    function get() {
        templates
            .get('update-profile')
            .then(function (data) {
                $('#main-container').html(data);
                $('#update-btn').on('click', function () {
                    updateUser();
                    this.redirect('#/');
                });

                $(function () {
                    $("#birthdate").datepicker();
                });
            });
    }

    function updateUser() {
        var password = $('#password').val();
        var passConfirm = $('#pass-confirm').val();
        var firstName = $('#firstname').val();
        var lastName = $('#lastname').val();
        var dateofbirth = $('#birthdate').val();

        if (password) {
            if (passConfirm) {
                if (password === passConfirm) {
                    user = {
                        password: password,
                        confirmPassword: passConfirm
                    }
                }
            }
        }

        if (firstName) {
            user = {
                firstName: firstName
            }
        }

        if (lastName) {
            user = {
                lastName: lastName
            }
        }

        if (dateofbirth) {
            user = {
                dateofbirth: dateofbirth
            }
        }

        data.users.update(user);
    }

    return {
        get: get
    }
}());