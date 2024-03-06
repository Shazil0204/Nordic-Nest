// Add event listener to button 1
document.getElementById("filter").addEventListener("click", function () {
  showOptions();
});

// Add event listener to button 2
document.getElementById("currentfilter").addEventListener("click", function () {
  showOptions();
});

function showOptions() {
  var filterMenu = document.getElementById("filtermenu");
  var dropdownicon = document.getElementById("dropdownicon");

  if (filterMenu.style.display === "none" || filterMenu.style.display === "") {
    filterMenu.style.display = "block";
    dropdownicon.classList.remove("rotate-180");
  } else {
    filterMenu.style.display = "none";
    dropdownicon.classList.add("rotate-180");
  }
}
