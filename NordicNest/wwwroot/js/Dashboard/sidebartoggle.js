document.addEventListener("DOMContentLoaded", function () {
  const sidebar = document.getElementById("sidebar");
  const context1 = document.getElementById("context-div-1");
  const context2 = document.getElementById("context-div-2");
  const toggleSidebarBtn = document.getElementById("toggleSidebar");
  const closeSidebarBtn = document.getElementById("closeSidebar"); // New button for closing the sidebar

  toggleSidebarBtn.addEventListener("click", toggleSidebar);
  closeSidebarBtn.addEventListener("click", toggleSidebar); // Add event listener to the closeSidebar button

  function toggleSidebar() {
    const isOpen = sidebar.classList.contains("-translate-x-full");
    if (isOpen) {
      sidebar.classList.remove("-translate-x-full");
      sidebar.classList.add(
        "translate-x-0",
        "drop-shadow-[0_15px_15px_rgba(0,0,0,1)]"
      );
      toggleSidebarBtn.style.display = "none";
      context1.style.filter = "blur(5px)"; // Apply blur effect to body
      context2.style.filter = "blur(5px)"; // Apply blur effect to body
    } else {
      sidebar.classList.remove(
        "translate-x-0",
        "drop-shadow-[0_15px_15px_rgba(255,255,255,1)]"
      );
      sidebar.classList.add("-translate-x-full");
      context1.style.filter = "none"; // Remove blur effect from body
      context2.style.filter = "none"; // Remove blur effect from body
      toggleSidebarBtn.style.display = "block";
      sidebar.classList.remove(
        "translate-x-0",
        "drop-shadow-[0_15px_15px_rgba(0,0,0,1)]"
      );
    }
  }
});
