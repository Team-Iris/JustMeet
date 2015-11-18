var myProfileController = (function () {
    var res;

    function all() {
        data.profile.myProfile()
            .then(function (response) {
                res = response;
                return templates.get('my-profile')
            })
			.then(function (template) {
			    $('#main-container').html(template(res));;
			})
        .then(function () {
            if (res.IsMale) {
                $('.gender-icon.male').addClass('active');
                $('#avatar').attr('src', 'https://upload.wikimedia.org/wikipedia/en/9/90/Bale_as_Batman.jpg');
            } else {
                $('.gender-icon.female').addClass('active')
                $('#avatar').attr('src', 'https://s-media-cache-ak0.pinimg.com/736x/d2/f6/01/d2f601d46629376ef656e7174d0a9149.jpg');
            }
        })
    }

    return {
        all: all
    }
}());