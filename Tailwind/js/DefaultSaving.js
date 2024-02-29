// Get the elements
const savingProgressBarFill = document.getElementById("savingProgressBarFill");

// Set the initial values
const currentsavingAmount = 512;
const goalsavingAmount = 5000;

// Calculate the progress percentage
const savingProgressPercentage = (currentsavingAmount / goalsavingAmount) * 100;

// Interpolate color based on progress
let savingFillColor = interpolatesavingColor(savingProgressPercentage);

// Update the saving progress bar fill color
savingProgressBarFill.style.backgroundColor = savingFillColor;

// Update the saving progress bar width
savingProgressBarFill.style.width = `${savingProgressPercentage}%`;

// Function to interpolate color based on saving progress
function interpolatesavingColor(progress) {
  // Define color stops
  const savingColorStops = [
    { percent: 0, color: [255, 0, 0] }, // Red at 0%
    { percent: 50, color: [255, 255, 0] }, // Yellow at 50%
    { percent: 100, color: [0, 255, 0] }, // Green at 100%
  ];

  // Find the two color stops that the progress falls between
  let savingStartColor, savingEndColor;
  for (let i = 0; i < savingColorStops.length - 1; i++) {
    if (progress <= savingColorStops[i + 1].percent) {
      savingStartColor = savingColorStops[i];
      savingEndColor = savingColorStops[i + 1];
      break;
    }
  }

  // Calculate the interpolated color
  const percentInRange =
    (progress - savingStartColor.percent) /
    (savingEndColor.percent - savingStartColor.percent);
  const savingInterpolatedColor = [
    Math.round(
      savingStartColor.color[0] +
        percentInRange * (savingEndColor.color[0] - savingStartColor.color[0])
    ),
    Math.round(
      savingStartColor.color[1] +
        percentInRange * (savingEndColor.color[1] - savingStartColor.color[1])
    ),
    Math.round(
      savingStartColor.color[2] +
        percentInRange * (savingEndColor.color[2] - savingStartColor.color[2])
    ),
  ];

  // Return the interpolated color as a CSS rgb string
  return `rgb(${savingInterpolatedColor.join(",")})`;
}
