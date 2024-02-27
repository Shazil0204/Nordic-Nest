// Get the elements
const progressBar = document.getElementById("progressBar");
const progressBarFill = document.getElementById("progressBarFill");
const progressText = document.getElementById("progressText");

// Set the initial values
const currentAmount =512;
const goalAmount = 5000;

// Calculate the progress percentage
const progressPercentage = (currentAmount / goalAmount) * 100;

// Interpolate color based on progress
let fillColor = interpolateColor(progressPercentage);

// Update the progress bar fill color
progressBarFill.style.backgroundColor = fillColor;

// Update the progress bar width
progressBarFill.style.width = `${progressPercentage}%`;

// Function to interpolate color based on progress
function interpolateColor(progress) {
  // Define color stops
  const colorStops = [
    { percent: 0, color: [255, 0, 0] }, // Red at 0%
    { percent: 50, color: [255, 255, 0] }, // Yellow at 50%
    { percent: 100, color: [0, 255, 0] }, // Green at 100%
  ];

  // Find the two color stops that the progress falls between
  let startColor, endColor;
  for (let i = 0; i < colorStops.length - 1; i++) {
    if (progress <= colorStops[i + 1].percent) {
      startColor = colorStops[i];
      endColor = colorStops[i + 1];
      break;
    }
  }

  // Calculate the interpolated color
  const percentInRange =
    (progress - startColor.percent) /
    (endColor.percent - startColor.percent);
  const interpolatedColor = [
    Math.round(
      startColor.color[0] +
        percentInRange * (endColor.color[0] - startColor.color[0])
    ),
    Math.round(
      startColor.color[1] +
        percentInRange * (endColor.color[1] - startColor.color[1])
    ),
    Math.round(
      startColor.color[2] +
        percentInRange * (endColor.color[2] - startColor.color[2])
    ),
  ];

  // Return the interpolated color as a CSS rgb string
  return `rgb(${interpolatedColor.join(",")})`;
}