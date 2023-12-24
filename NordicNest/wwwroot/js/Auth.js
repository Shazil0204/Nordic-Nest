// Get references to the forms and links
const loginForm = document.getElementById("loginForm");
const registerForm = document.getElementById("registerForm");
const showRegisterLink = document.getElementById("showRegister");
const showLoginLink = document.getElementById("showLogin");

// Function to show the login form and hide the register form
function showLoginForm() {
    loginForm.classList.remove("hidden");
    registerForm.classList.add("hidden");
}

// Function to show the register form and hide the login form
function showRegisterForm() {
    registerForm.classList.remove("hidden");
    loginForm.classList.add("hidden");
}

// Add click event listeners to the links
showRegisterLink.addEventListener("click", function (event) {
    event.preventDefault();
    showRegisterForm();
});

showLoginLink.addEventListener("click", function (event) {
    event.preventDefault();
    showLoginForm();
});

// Initially show the login form and hide the register form
showLoginForm();
