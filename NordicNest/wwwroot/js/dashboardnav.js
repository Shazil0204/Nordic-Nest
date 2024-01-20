const navbarToggle = document.getElementById('navbarToggle');
const closeNavbar = document.getElementById('closeNavbar');
const navbar = document.getElementById('navbar');

navbarToggle.addEventListener('click', () => {
    navbar.classList.toggle('-translate-x-full');
    navbarToggle.style.display = 'none';
});

closeNavbar.addEventListener('click', () => {
    navbar.classList.toggle('-translate-x-full');
    navbarToggle.style.display = 'block';
});