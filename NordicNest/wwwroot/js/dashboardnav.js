var showNavID = document.getElementById('ShowNavBtn');
var navbar = document.getElementById('DashboardNavbar');

function HideNav() {
    navbar.classList.add('hidden');
    showNavID.style.display = 'block';
}

function ShowNav() {
    navbar.classList.remove('hidden');
    showNavID.style.display = 'none';
}