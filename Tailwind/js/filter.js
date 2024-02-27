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
