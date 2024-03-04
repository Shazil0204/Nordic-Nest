// Function to update button text based on user input
function updateButtonText() {
  // Retrieve user inputs
  const totalAmount = parseFloat(document.getElementById("totalValue").value);
  const startingDate = new Date(document.getElementById("starting-date").value);
  const deadlineInput = document.getElementById("deadline").value;
  const deadline = deadlineInput ? new Date(deadlineInput) : null;
  const amountToInsert = parseFloat(
    document.getElementById("valueToInsert").value
  );
  const frequency = document.getElementById("frequency").value;

  // Check if starting date is before the deadline
  if (deadline && startingDate >= deadline) {
    // Update button text and disable button
    document.getElementById("calculateBtn").textContent =
      "Dates are causing problems";
    document.getElementById("calculateBtn").style.cursor = "not-allowed";
    document.getElementById("calculateBtn").disabled = true;
    return; // Exit function
  } else {
    // Re-enable button and set default text
    document.getElementById("calculateBtn").textContent = "Submit";
    document.getElementById("calculateBtn").style.cursor = "pointer";
    document.getElementById("calculateBtn").disabled = false;
  }

  // Update button text based on user input
  const calculateBtn = document.getElementById("calculateBtn");
  if (totalAmount && deadline && amountToInsert && frequency) {
    calculateBtn.textContent = "Double check";
  } else if (totalAmount && deadline && frequency) {
    calculateBtn.textContent = `Calculate Value to Insert ${frequency}`;
  } else if (totalAmount && amountToInsert && frequency && !deadline) {
    calculateBtn.textContent = "Calculate deadline";
  } else if (totalAmount && !deadline && amountToInsert && frequency) {
    calculateBtn.textContent = "Calculate Deadline";
  } else if (deadline && amountToInsert && frequency && !totalAmount) {
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
