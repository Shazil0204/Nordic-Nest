document.addEventListener("DOMContentLoaded", function () {
  const ctx = document.getElementById("SavingInsert").getContext("2d");
  const myChart = new Chart(ctx, {
    type: "bar",
    data: {
      labels: [
        "January",
        "February",
        "March",
        "April",
        "May",
        "June",
        "July",
        "August",
        "September",
        "October",
        "November",
        "December",
      ],
      datasets: [
        {
          label: "Saving",
          data: [
            1200, 1300, 1100, 900, 1000, 950, 1050, 1150, 1250, 1350, 1400,
            1300,
          ],
          backgroundColor: "rgba(54, 162, 235, 0.5)",
          borderColor: "rgba(54, 162, 235, 1)",
          borderWidth: 1,
        },
      ],
    },
    options: {
      scales: {
        y: {
          beginAtZero: true,
          title: {
            display: true,
            text: "Amount",
          },
        },
      },
      plugins: {
        tooltip: {
          callbacks: {
            label: function (context) {
              return "You have inserted: " + context.raw + " in this month";
            },
          },
        },
      },
    },
  });
});
