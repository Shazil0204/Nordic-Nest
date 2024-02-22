document.addEventListener("DOMContentLoaded", function () {
  const ctx = document.getElementById("Weekchart").getContext("2d");
  const myChart = new Chart(ctx, {
    type: "line",
    data: {
      labels: [
        "Monday",
        "Tuesday",
        "Wednesday",
        "Thursday",
        "Friday",
        "Saturday",
        "Sunday",
      ],
      datasets: [
        {
          label: "Weekly Amount Usage",
          data: [65, 59, 80, 81, 56, 55, 40],
          borderColor: "rgba(255, 99, 132, 1)",
          borderWidth: 1,
          tension: 0.4,
        },
      ],
    },
    options: {
      scales: {
        y: {
          beginAtZero: true,
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
    },
  });
});
