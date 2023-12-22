document.addEventListener('DOMContentLoaded', function () {
    var password = document.getElementById('password');
    var confirmPassword = document.getElementById('confirm_password');

    function validatePassword() {
        if (password.value !== confirmPassword.value) {
            confirmPassword.setCustomValidity("Passwords don't match");

        } else {
            confirmPassword.setCustomValidity('');
        }
    }

    password.onchange = validatePassword;
    confirmPassword.onkeyup = validatePassword;
});