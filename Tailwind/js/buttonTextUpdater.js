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
    return; // Exit function
  } else {
    // Re-enable button and set default text
    document.getElementById("calculateBtn").textContent = "Submit";
  }

  // Update button text based on user input
  const calculateBtn = document.getElementById("calculateBtn");
  if (totalAmount && deadline && amountToInsert && frequency) {
    calculateBtn.textContent = "Double check";
    countDates();
  } else if (totalAmount && deadline && frequency) {
    calculateBtn.textContent = `Calculate Value to Insert ${frequency}`;
    countDates();
  } else if (totalAmount && amountToInsert && frequency && !deadline) {
    calculateBtn.textContent = "Calculate Deadline";
  } else if (deadline && amountToInsert && frequency && !totalAmount) {
    calculateBtn.textContent = "Calculate Total Amount";
    countDates();
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

function countDates() {
  const startDate = new Date(document.getElementById("starting-date").value);
  const endDate = new Date(document.getElementById("deadline").value);
  const option = document.getElementById("frequency").value;

  let count = 0;
  let currentDate = new Date(startDate);

  switch (option) {
    case "daily":
      count = Math.ceil((endDate - startDate) / (1000 * 60 * 60 * 24));
      console.log(
        `There are ${count} days between ${startDate.toDateString()} and ${endDate.toDateString()}.`
      );
      break;
    case "weekly":
      count = Math.ceil((endDate - startDate) / (1000 * 60 * 60 * 24 * 7));
      console.log(
        `There are ${count} weeks between ${startDate.toDateString()} and ${endDate.toDateString()}.`
      );
      break;
    case "bi-monthly":
      let count01 = 0;
      let count16 = 0;
      while (currentDate <= endDate) {
        if (currentDate.getDate() === 1) {
          count01++;
        } else if (currentDate.getDate() === 16) {
          count16++;
        }
        currentDate.setMonth(currentDate.getMonth() + 1); // Move to the next month
        currentDate.setDate(1); // Set the day to the 1st to avoid issues with months of different lengths
      }
      console.log(
        `There are ${count01} occurrences of 01 and ${count16} occurrences of 16 between ${startDate.toDateString()} and ${endDate.toDateString()}.`
      );
      break;
    case "monthly":
      let countMonthly = 0;
      let currentMonthDate = new Date(startDate);
      while (currentMonthDate <= endDate) {
        if (currentMonthDate.getDate() === 1) {
          countMonthly++;
        }
        currentMonthDate.setMonth(currentMonthDate.getMonth() + 1); // Move to the next month
        currentMonthDate.setDate(1); // Set the day to the 1st to avoid issues with months of different lengths
      }
      console.log(
        `There are ${countMonthly} occurrences of the 01 date between ${startDate.toDateString()} and ${endDate.toDateString()}.`
      );
      break;
    default:
      console.log("Invalid option selected.");
  }
}
