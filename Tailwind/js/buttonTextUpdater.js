// valueChecker.js

// Function to update button text based on user input
function updateButtonText() {
  // Retrieve user inputs
  const totalAmount = parseFloat(document.getElementById("totalValue").value);
  const startingDate = new Date(document.getElementById("starting-date").value);
  const deadline = new Date(document.getElementById("deadline").value);
  const amountToInsert = parseFloat(
    document.getElementById("valueToInsert").value
  );
  const frequency = document.getElementById("frequency").value;

  // Check if starting date is before the deadline
  if (startingDate >= deadline) {
    // Update button text and disable button
    document.getElementById("calculateBtn").textContent =
      "Dates are causing problems";
    document.getElementById("calculateBtn").style.cursor = "not-allowed";
    return; // Exit function
  } else {
    // Revert button text and enable button
    document.getElementById("calculateBtn").textContent = "Submit";
    document.getElementById("calculateBtn").style.cursor = "pointer";
  }

  // Update button text based on user input
  const calculateBtn = document.getElementById("calculateBtn");
  if (totalAmount && deadline && amountToInsert && frequency) {
    calculateBtn.textContent = "Calculate";
  } else if (totalAmount && deadline && frequency) {
    calculateBtn.textContent = `Calculate Value to Insert ${frequency}`;
  } else if (totalAmount && amountToInsert && frequency) {
    calculateBtn.textContent = "Calculate Deadline";
  } else if (deadline && amountToInsert && frequency) {
    calculateBtn.textContent = "Calculate Total Amount";
  } else {
    calculateBtn.textContent = "Few Data Missing";
  }
}

// Attach event listeners to input fields for immediate response
document
  .getElementById("totalValue")
  .addEventListener("input", updateButtonText);
document
  .getElementById("starting-date")
  .addEventListener("change", updateButtonText);
document
  .getElementById("deadline")
  .addEventListener("change", updateButtonText);
document
  .getElementById("valueToInsert")
  .addEventListener("input", updateButtonText);
document
  .getElementById("frequency")
  .addEventListener("change", updateButtonText);
