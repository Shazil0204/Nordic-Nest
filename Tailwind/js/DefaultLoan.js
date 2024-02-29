// Get the elements
const loanProgressBarFill = document.getElementById("loanProgressBarFill");

// Set the initial values
const currentLoanAmount = 512;
const goalLoanAmount = 5000;

// Calculate the progress percentage
const loanProgressPercentage = (currentLoanAmount / goalLoanAmount) * 100;

// Interpolate color based on progress
let loanFillColor = interpolateLoanColor(loanProgressPercentage);

// Update the loan progress bar fill color
loanProgressBarFill.style.backgroundColor = loanFillColor;

// Update the loan progress bar width
loanProgressBarFill.style.width = `${loanProgressPercentage}%`;

// Function to interpolate color based on loan progress
function interpolateLoanColor(progress) {
  // Define color stops
  const loanColorStops = [
    { percent: 0, color: [255, 0, 0] }, // Red at 0%
    { percent: 50, color: [255, 255, 0] }, // Yellow at 50%
    { percent: 100, color: [0, 255, 0] }, // Green at 100%
  ];

  // Find the two color stops that the progress falls between
  let loanStartColor, loanEndColor;
  for (let i = 0; i < loanColorStops.length - 1; i++) {
    if (progress <= loanColorStops[i + 1].percent) {
      loanStartColor = loanColorStops[i];
      loanEndColor = loanColorStops[i + 1];
      break;
    }
  }

  // Calculate the interpolated color
  const percentInRange =
    (progress - loanStartColor.percent) /
    (loanEndColor.percent - loanStartColor.percent);
  const loanInterpolatedColor = [
    Math.round(
      loanStartColor.color[0] +
        percentInRange * (loanEndColor.color[0] - loanStartColor.color[0])
    ),
    Math.round(
      loanStartColor.color[1] +
        percentInRange * (loanEndColor.color[1] - loanStartColor.color[1])
    ),
    Math.round(
      loanStartColor.color[2] +
        percentInRange * (loanEndColor.color[2] - loanStartColor.color[2])
    ),
  ];

  // Return the interpolated color as a CSS rgb string
  return `rgb(${loanInterpolatedColor.join(",")})`;
}
