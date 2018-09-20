$(document).ready(function () {
    $('#btnSubmit').click(function (e) {
        if (grecaptcha && grecaptcha.getResponse().length > 0) {
            return true;
        }
        else {
            swal({
                title: 'Please check your captcha',
                type: 'error',
                confirmButtonText: 'Ok'
            });
            e.preventDefault();
            return false;
        }
    });
})