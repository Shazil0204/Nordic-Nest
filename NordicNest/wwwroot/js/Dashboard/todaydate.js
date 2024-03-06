// Get the current date
const today = new Date();
// Format the date as YYYY-MM-DD
const formattedDate = today.toISOString().slice(0, 10);
// Set the default value of the starting date input field
document.getElementById("starting-date").value = formattedDate;
