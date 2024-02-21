document.addEventListener("DOMContentLoaded", function () {
  const chartContainer = document.getElementById("MaxAmount");

  if (!chartContainer) {
    console.error("Could not find chart container element.");
    return;
  }

  if (!(chartContainer instanceof HTMLCanvasElement)) {
    console.error("The chart container is not a canvas element.");
    return;
  }

  const ctx = chartContainer.getContext("2d");

  if (!ctx) {
    console.error("Failed to get 2D context for chart container.");
    return;
  }

  const monthlyAmounts = [150, 130, 140, 160]; // Sample monthly amounts
  const maxAmount = 200; // Maximum amount for the month
  const deadlineAmount = 160; // Deadline amount

  const data = {
    labels: ["1", "2", "3", "4"], // Assuming the month has 4 days for simplicity
    datasets: [
      {
        label: "Daily Amount",
        data: monthlyAmounts,
        fill: false,
        borderColor: "rgba(0, 255, 255, 1)",
        tension: 0.4,
        pointRadius: 6,
      },
      {
        label: "Max Monthly Amount",
        data: Array(4).fill(maxAmount), // 4 days in the month
        fill: false,
        borderColor: "rgba(255, 99, 132, 1)",
        borderDash: [1, 5],
      },
    ],
  };

  const options = {
    scales: {
      y: {
        beginAtZero: true,
        ticks: {
          callback: function (value, index, values) {
            return value; // Display $ sign with y-axis values
          },
        },
      },
    },
    plugins: {
      annotation: {
        annotations: {
          deadlineLine: {
            type: "line",
            mode: "horizontal",
            scaleID: "y",
            value: deadlineAmount,
            borderColor: "red",
            borderWidth: 2,
            label: {
              content: "Deadline",
              enabled: true,
              position: "right",
            },
          },
        },
      },
    },
    plugins: {
      tooltip: {
        callbacks: {
          label: function (context) {
            return "You have used: " + context.raw;
          },
        },
      },
    },
  };

  const myChart = new Chart(ctx, {
    type: "line",
    data: data,
    options: options,
  });
});
